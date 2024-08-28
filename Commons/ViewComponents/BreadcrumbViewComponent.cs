using System.Collections.Generic;
using Commons.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commons.ViewComponents
{
    public class BreadcrumbViewComponent : ViewComponent
    {

        public BreadcrumbViewComponent()
        {
            
        }

        public IViewComponentResult Invoke(string filter)
        {
            if (ViewBag.Breadcrumb == null)
            {
                ViewBag.Breadcrumb = new List<Message>();
            }
            
            return View("LayoutBreadcrumb", ViewBag.Breadcrumb as List<Message>);
        }
    }
}
