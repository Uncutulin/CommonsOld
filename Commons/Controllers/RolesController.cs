using System;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Commons.Identity;
using Commons.Identity.Attributes;
using Commons.Identity.Extensions;
using Commons.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Commons.Controllers
{
    [Secured]
    public class SecurityRoles : CommonsController
    {
        private readonly IRolesService _roles;
        private readonly IUserService _user;
        private readonly IClaimsService _claims;
        public ICommonsDbContext Context { get; set; }

        public SecurityRoles(ICommonsDbContext context, IRolesService roles, IUserService user, IClaimsService claims) 
        {
            _roles = roles;
            _user = user;
            _claims = claims;
            Context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View("Roles/Index", await _roles.GetRoles(x => x.WorkSpaceId == User.GetWorkSpaceId()));
        }

        public async Task<IActionResult> RolesDataTable()
        {
            var query = from role in _roles.GetRoles(x => x.WorkSpaceId == User.GetWorkSpaceId()).Result.AsQueryable()
                select new CommonsRoleDto()
                {
                    Id = role.Id,
                    ShowName = role.ShowName,
                    Show = role.Show,
                    Description = role.Description
                };

            return DataTable(query);
        }

        [HttpGet]
        public IActionResult _Create()
        {
            return PartialView("Roles/_Create");
        }

        [HttpPost]
        public async Task<IActionResult> _Create(string showName, string description)
        {
            if (await Context.Roles.AnyAsync(x => x.ShowName == showName && x.WorkSpaceId == User.GetWorkSpaceId()))
            {
                AddPageAlerts(PageAlertType.Error, $"Ya existe un rol con el nombre {showName}.");
            }

            await _roles.Create(showName, description, User.GetWorkSpaceId());

            AddPageAlerts(PageAlertType.Success, $"Se ha creado el rol {showName}.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roles.Find(x => x.Id == id);

            return View("Roles/Edit", role);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roles.Find(x => x.Id == id);
            await _roles.Delete(role);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult _AddFunction(string id)
        {
            ViewBag.Functions = Context.AspNetFunctions.Select(x => new SelectListItem(x.Name, x.Id));
            return PartialView("Roles/_AddFunction", id);
        }

        [HttpPost]
        public async Task<IActionResult> _AddFunction(string roleId, string functionId)
        {
            var role = await Context.Roles.Include(x => x.RoleFunctions).FirstOrDefaultAsync(x => x.Id == roleId);
            var function = await Context.AspNetFunctions.FindAsync(functionId);

            if (role == null || function == null)
            {
                AddPageAlerts(PageAlertType.Error, "Error al agregar la función.");
                return RedirectToAction(nameof(Index));
            }

            role.AddFunction(function);

            await _roles.Update(role);

            return RedirectToAction("Edit", new {id = roleId});
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFunction(string roleId, string functionId)
        {
            var role = await Context.Roles.Include(x => x.RoleFunctions).FirstOrDefaultAsync(x => x.Id == roleId);
            var function = await Context.AspNetFunctions.FindAsync(functionId);

            if (role == null || function == null)
            {
                AddPageAlerts(PageAlertType.Error, "Error al eliminar la función.");
                return RedirectToAction(nameof(Index));
            }

            role.RemoveFunction(function);
            await Context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = roleId });
        }

        /// <summary>
        /// Returns partial view for role assignment.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> _Assign(string id)
        {
            if (await _claims.IsWorkSpaceAdmin(id, User.GetWorkSpaceId()))
            {
                return PartialView("Roles/_AssignAdmin", id);
            }

            var userRoles = await _user.GetRoles(id, User.GetWorkSpaceId());
            ViewBag.UserRoles = userRoles;

            var roles = await _roles.GetRoles(x => x.WorkSpaceId == User.GetWorkSpaceId());

            roles.RemoveAll(x => userRoles.Any(y => y.Id == x.Id));

            ViewBag.Roles = roles.Select(x => new SelectListItem(x.ShowName, x.Id));
            return PartialView("Roles/_Assign", id);
        }

        /// <summary>
        /// Assign role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> _Assign(string userId, string roleId)
        {
            var role = await _roles.Find(x => x.Id == roleId);
            await _user.AddToRoleAsync(userId, role);
            return RedirectToAction("_Assign", new {id = userId});
        }

        [HttpGet]
        public async Task<IActionResult> _Unassign(string userId, string roleId)
        {
            var role = await _roles.Find(x => x.Id == roleId);
            await _user.RemoveFromRoleAsync(userId, role);
            return RedirectToAction("_Assign", new {id = userId});
        }

        /// <summary>
        /// Assign role.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> _MakeAdmin(string userId)
        {
            await _claims.AddWorkSpaceAdmin(userId, User.GetWorkSpaceId());
            return RedirectToAction("_Assign", new {id = userId});
        }

        [HttpGet]
        public async Task<IActionResult> _RemoveAdmin(string userId)
        {
            await _claims.RemoveWorkSpaceAdmin(userId, User.GetWorkSpaceId());
            return RedirectToAction("_Assign", new { id = userId });
        }
    }
}
