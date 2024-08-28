using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Commons.Controllers;
using Commons.Extensions;
using Commons.Identity;
using Commons.Identity.Attributes;
using Commons.Identity.Extensions;
using Commons.Identity.Services;
using Commons.Models;
using CommonsDev.Data;
using Microsoft.AspNetCore.Mvc;
using CommonsDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CommonsDev.Controllers
{
    [Secured]
    public class HomeController : CommonsController
    {
        private readonly IRolesService _roles;
        private readonly UserService<User> _user;
        private readonly ApplicationDbContext _context;
        private readonly IClaimsService _claims;
        private IHttpContextAccessor _http;

        public HomeController(ApplicationDbContext context,
            IRolesService roles,
            UserService<User> user,
            IClaimsService claims,
            IHttpContextAccessor http)
        {
            _roles = roles;
            _user = user;
            _context = context;
            _claims = claims;
            _http = http;
        }

        [Unsecured]
        public async Task<IActionResult> Index([FromServices] UserService<User> userService, int nro = 0)
        {
            ViewBag.Nro = nro + 1;
            ViewBag.UserId = userService.FindByNameAsync(User.Identity.Name).Result.Id;


            var user = await _user.FindByNameAsync(User.GetUserName());

            AddPageAlerts(PageAlertType.Info, $"Username {user.UserName}");

            //var routes = await HttpContext.GetUserRoutes();
            var routes = new List<string>();


            if (User.IsInRole("47a29155-98ac-4a63-879c-4a827e782f81: Test"))
            {
                AddPageAlerts(PageAlertType.Success, "Esta en el rol de testing.");
            }



            ViewBag.Routes = routes;

            AddBreadcrumb($"Primero", $"/Home/", $"fa fa-circle");
            AddBreadcrumb($"Primero", $"/Home/", $"fa fa-circle");
            AddBreadcrumb($"Primero", $"/Home/", $"fa fa-circle");
            AddBreadcrumb($"Primero", $"/Home/", $"fa fa-circle");

            return View();
        }

        public IActionResult ErrorAsync()
        {
            return BadRequest("Mensaje de error que da el controlador.");
        }

        public IActionResult _Partial()
        {
            return PartialView();
        }

        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Returns partial view for role assignment.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> _Assign(string id)
        {
            var userRoles = await _user.GetRoles(id, User.GetWorkSpaceId());
            ViewBag.UserRoles = userRoles;

            var roles = await _roles.GetRoles(x => x.WorkSpaceId == User.GetWorkSpaceId());

            roles.RemoveAll(x => userRoles.Contains(x));

            ViewBag.Roles = roles.Select(x => new SelectListItem(x.ShowName, x.Id));
            ViewBag.IsAdmin = _claims.IsWorkSpaceAdmin(id, User.GetWorkSpaceId());
            return PartialView("_Assign", id);
        }

        public IActionResult SelectListJson(string q)
        {
            var items = _context.Tests.Where(x => x.Name.Contains(q))
                .Select(x => new 
                {
                    Text = $"{x.Name}, {x.Doc}",
                    Value = x.Id,
                    Subtext = $"{x.Doc} {x.Doc}",
                    Icon = "fa fa-plus",
                    Class = ""
                }).Take(50).ToArray();

            return Json(items);
        }


        [Secured]
        public async Task<IActionResult> SelectListJson2(string q)
        {
            var items = await _context.Tests.Where(x => x.Name.Contains(q))
                .Select(x => new SelectpickerItem()
                {
                    Text = x.Name,
                    Value = x.Id,
                    Subtext = x.Doc,
                    Icon = "fa fa-plus",
                    //Content = "<small class=\"label label-success\">Sí</small>"
                }).Take(10).ToArrayAsync();

            return Json(items);
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<IActionResult> GenerateRandomFunctions()
        {
            var routes = new List<CommonsRoute>();

            for (int i = 0; i < 1000; i++)
            {
                routes.Add(new CommonsRoute()
                {
                    Area = RandomString(10),
                    Controller = RandomString(10),
                    Action = RandomString(10)
                });
            }

            var function = new CommonsFunction(){
                Name = $"1000 routes function {RandomString(3)}",
            };

            function.SetRoutes(routes);

            _context.AspNetFunctions.Add(function);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult DoubleModal()
        {
            return PartialView("DoubleModal");
        }

        public IActionResult TripleModal()
        {
            return PartialView("TripleModal");
        }

        public IActionResult Bool(bool uno, bool dos)
        {
            return Json(new {uno = uno, dos = dos});
        }
        
        [HttpGet]
        public IActionResult File()
        {
            return PartialView();
        }
        
        [HttpPost]
        public IActionResult File(IFormFile file)
        {
            throw new Exception(file.Name);
        }

        [HttpGet]
        public IActionResult _ModalError()
        {
            return PartialView("_ModalError", "Mensaje de error.");
        }
    }
}
