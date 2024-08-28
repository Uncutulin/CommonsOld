using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonsDev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommonsDev.Controllers
{
    public class ModalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult _HeavyPartial()
        {
            return PartialView();
        }

        public IActionResult _LoadersTest()
        {
            return PartialView();
        }

        public IActionResult _ComboBox()
        {
            ViewBag.Items = _context.Institutes.Take(30).Select(x => new SelectListItem(x.Name, x.Id));
            
            return PartialView();
        }


        public IActionResult _Embed(string param)
        {
            ViewBag.File = "data:application/pdf;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes("wwwroot/docs/archivo.pdf"));
            return PartialView();
        }

        public IActionResult _Embed2(string param)
        {
            ViewBag.File = "data:application/pdf;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes("wwwroot/docs/archivo.pdf"));
            return PartialView();
        }

        public IActionResult Pdf()
        {
            var bytes = System.IO.File.ReadAllBytes("wwwroot/docs/archivo.pdf");

            return File(bytes, "application/pdf");
        }
    }
}