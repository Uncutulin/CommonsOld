using System;
using System.Collections.Generic;
using Commons.Extensions;
using Commons.Helpers;
using Commons.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commons.Controllers.ViewComponents.Layout
{
    public class SidebarViewComponent : ViewComponent
    {
        private List<SidebarMenu> sidebars = new List<SidebarMenu>();

        public IViewComponentResult Invoke(string filter)
        {
            //you can do the access rights checking here by using session, user, and/or filter parameter

            if (!User.Identity.IsAuthenticated) return View("LayoutSidebar", sidebars);

            sidebars.Add(MenuHelpers.AddHeader("Menú Principal"));

            sidebars.Add(MenuHelpers.AddModule("Funciones", "/SecurityFunctions/", "fa  fa-warning"));
            sidebars.Add(MenuHelpers.AddModule("Roles", "/SecurityRoles/", "fa  fa-warning"));
            sidebars.Add(MenuHelpers.AddModule("Institutos", "/Institutes/", "fa  fa-gear"));
            sidebars.Add(MenuHelpers.AddModule("Institutos Old", "/Institutes/DataTable", "fa  fa-gear"));
            sidebars.Add(MenuHelpers.AddModule("Modals", "/Home/About", "fa  fa-gear"));


            if (HttpContext.UserHasRoute("/TestClasses/Index"))
            {
                sidebars.Add(MenuHelpers.AddModule("TestClass", "/TestClasses/", "fa  fa-gear"));
            }

            var luke = MenuHelpers.AddTree("Luke");

            luke.TreeChild = new List<SidebarMenu>()
            {
                MenuHelpers.AddModule("Bullet", "/Home/Privacy"),
                MenuHelpers.AddModule("Bullet", "/Home/About")
            };



            var vader = MenuHelpers.AddTree("Vader");
            
            vader.TreeChild = new List<SidebarMenu>()
            {
                luke
            };
            

            sidebars.Add(vader);

            foreach (SidebarMenu sidebar in sidebars)
            {
                
            }

            return View("LayoutSidebar", sidebars);
        }


        public void AddModule(string text, string urlPath, string iconClassName = "fa fa-circle-o",
            Tuple<int, int, int> counter = null)
        {
            sidebars.Add(MenuHelpers.AddModule(text, urlPath, iconClassName, counter));
        }

        public void AddModuleIf(string text, string urlPath, string iconClassName = "fa fa-circle-o",
            Tuple<int, int, int> counter = null)
        {
            if (!HttpContext.UserHasRoute(urlPath)) return;
            sidebars.Add(MenuHelpers.AddModule(text, urlPath, iconClassName, counter));
        }



    }
}
