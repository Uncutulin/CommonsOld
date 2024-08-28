using System;
using System.Collections.Generic;
using System.Text;
using Commons.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commons.ViewComponents
{
    public class ConfirmModalViewComponent : ViewComponent
    {
        public ConfirmModalViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("ConfirmModal");
        }
    }
}
