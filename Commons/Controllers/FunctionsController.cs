using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Commons.Identity;
using Commons.Identity.Attributes;
using Commons.Identity.Extensions;
using Commons.Identity.Services;
using Commons.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Commons.Controllers
{
    [Secured]
    public class SecurityFunctionsController : CommonsController
    {
        private readonly IRolesService _roles;
        public ICommonsDbContext Context { get; set; }

        public SecurityFunctionsController(ICommonsDbContext context, IRolesService roles)
        {
            Context = context;
            _roles = roles;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            User.IsInRole()
            if (!User.IsAdmin()) return new ForbidResult();
            AddPageAlerts(PageAlertType.Info, "Esta área es solo para administradores. Aquí se configuran las funciones que verán los usuarios finales a la hora de crear roles.");
            return View("Functions/Index");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult FunctionsDataTable()
        {
            var query = from x in Context.AspNetFunctions
                select new CommonsFunctionDto()
                {
                    Name = x.Name,
                    Description = x.Description,
                    LastEditTime = x.LastEditTime.ToString("g"),
                    Show = x.Show,
                    Id = x.Id
                };

            return DataTable(query);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult _Create()
        {
            return PartialView("Functions/_Create");
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commonsFunction"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _Create(CommonsFunction commonsFunction)
        {
            if (!User.IsAdmin()) return new ForbidResult();
            if (ModelState.IsValid)
            {
                await Context.AspNetFunctions.AddAsync(new CommonsFunction()
                {
                    Name = commonsFunction.Name,
                    Description = commonsFunction.Description,
                });
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string id)
        {
            if (!User.IsAdmin()) return new ForbidResult();
            if (id == null)
            {
                return NotFound();
            }

            var commonsFunction = await Context.AspNetFunctions.FindAsync(id);
            if (commonsFunction == null)
            {
                return NotFound();
            }

            Context.AspNetRoleFunctions.RemoveRange(Context.AspNetRoleFunctions.Where(x => x.Function == commonsFunction));
            Context.AspNetFunctions.Remove(commonsFunction);

            await SaveFunctionChangesAsync(commonsFunction.Id);

            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Routes(string id)
        {
            var function = await Context.AspNetFunctions.FindAsync(id);
            if (function == null) return RedirectToAction(nameof(Index));

            return View("Functions/Routes", function);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RoutesDataTable(string functionId)
        {
            var function = await Context.AspNetFunctions.FindAsync(functionId);

            var query = function.GetRoutes().AsQueryable().Select(x => new
            {
                Area = x.Area,
                Action = x.Action,
                Controller = x.Controller,
                Url = x.Url
            });

            return DataTable(query);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> _AddRoute(string id)
        {
            var function = await Context.AspNetFunctions.FindAsync(id);
            List<CommonsRoute> available = AssemblyService.GetRoutes();

            var notAvailable = new List<string>()
            {
                "SecurityFunctions"
            };

            foreach (string s in notAvailable)
            {
                available.RemoveAll(x => x.Url.Contains(s));
            }

            foreach (CommonsRoute route in function.GetRoutes())
            {
                available.RemoveAll(x => x.Url == route.Url);
            }

            ViewBag.Routes = available.Select(x => new SelectListItem()
            {
                Value = x.Url,
                Text = $"{x.Area ?? "Ninguna"}, {x.Controller}, {x.Action}"
            });

            ViewBag.Controllers = available
                .GroupBy(x => new { Area = x.Area, Controller = x.Controller})
                .Select(x => new SelectListItem(
                    $"{x.Key.Area ?? "Ninguna"}, {x.Key.Controller}"
                    , $"{x.Key.Area}-{x.Key.Controller}"
                ));

            return PartialView("Functions/_AddRoute", id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionId"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> _AddRoute(string functionId, string route)
        {
            if (!User.IsAdmin()) return new ForbidResult();
            var croute = new CommonsRoute(route);
            var function = await Context.AspNetFunctions.FindAsync(functionId);

            var routes = function.GetRoutes();
            routes.Add(croute);
            function.SetRoutes(routes);

            Context.AspNetFunctions.Update(function);

            await SaveFunctionChangesAsync(function.Id);

            return RedirectToAction("Routes", new { id = function.Id });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionId"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddController(string functionId, string controller)
        {
            List<CommonsRoute> available = AssemblyService.GetRoutes();

            var array = controller.Split('-');

            var function = await Context.AspNetFunctions.FindAsync(functionId);

            foreach (CommonsRoute route in available.Where(x => x.Url.Contains(array[0]) && x.Url.Contains(array[1])))
            {
                function.AddRoute(route);
            }

            await SaveFunctionChangesAsync(function.Id);

            return RedirectToAction("Routes", new { id = function.Id });
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionId"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveRoute(string functionId, string route)
        {
            if (!User.IsAdmin()) return new ForbidResult();
            var croute = new CommonsRoute(route);
            var function = await Context.AspNetFunctions.FindAsync(functionId);

            var routes = function.GetRoutes();
            routes.RemoveAll(x => x.Url == route);
            function.SetRoutes(routes);

            Context.AspNetFunctions.Update(function);

            await SaveFunctionChangesAsync(function.Id);

            return RedirectToAction("Routes", new { id = function.Id });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        private async Task SaveFunctionChangesAsync(string functionId)
        {
            await Context.SaveChangesAsync();
        }
    }
}
