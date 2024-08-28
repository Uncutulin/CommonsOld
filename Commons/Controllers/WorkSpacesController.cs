using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Commons.Identity;
using Commons.Identity.Extensions;
using Commons.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Commons.Controllers
{
    public class WorkSpacesController : CommonsController
    {
        private readonly IUserService _userService;
        private readonly ICommonsDbContext _context;
        private readonly IClaimsService _claimsService;
        private readonly ISignInService _signInService;

        public WorkSpacesController(IUserService userService, ICommonsDbContext context, IClaimsService claimsService, ISignInService signInService)
        {
            _userService = userService;
            _context = context;
            _claimsService = claimsService;
            _signInService = signInService;
        }

        [HttpGet]
        public async Task<IActionResult> _Select()
        {
            List<IWorkSpace> workspaces = await _userService.GetRelatedWorkSpaces(User.Identity.Name);
            if (User.IsAdmin())
            {
                workspaces = _context.GetIWorkSpaces();
            }
            return PartialView("WorkSpaces/_Select", workspaces.OrderBy(x => x.Name).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> _Change(string id)
        {
            var username = User.Identity.Name;

            await _claimsService.ChangeSelectedInstitute(username, id);

            await _claimsService.Refresh(username);

            await _signInService.SignOutAsync();

            await _signInService.SignInAsync(username);

            // Back to home.
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}