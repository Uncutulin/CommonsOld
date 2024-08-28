using System;
using System.Linq;
using System.Threading.Tasks;
using Commons.Controllers;
using Commons.Extensions;
using Commons.Identity.Attributes;
using Commons.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommonsDev.Data;
using CommonsDev.Models;

namespace CommonsDev.Controllers
{
    public class TestClassesController : CommonsController
    {
        private readonly ApplicationDbContext _context;

        public TestClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TestClasses
        [Secured]
        public async Task<IActionResult> Index()
        {

            HttpContext.Session.SetComplexData("test", "hola");

            AddPageAlerts(PageAlertType.Info, HttpContext.Session.GetComplexData<string>("test"));
            return View(await _context.Tests.ToListAsync());
        }

        public IActionResult _TestTable(Page<TestClass> page)
        {
            page.SelectPage(HttpContext
                , _context.Tests
                    .Where(x => x.Name.Contains(page.SearchText))
                    .OrderBy(x => x.Name)
                , true
                , 20
                );

            
            
            return PartialView(page);
        }

        // GET: TestClasses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testClass = await _context.Tests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testClass == null)
            {
                return NotFound();
            }

            return View(testClass);
        }

        // GET: TestClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestClass testClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testClass);
        }

        // GET: TestClasses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testClass = await _context.Tests.FindAsync(id);
            if (testClass == null)
            {
                return NotFound();
            }
            return View(testClass);
        }

        // POST: TestClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Id,CreationDate,DeletedDate,DeletedById,LastEditTime,LastEditById,Display,Show")] TestClass testClass)
        {
            if (id != testClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestClassExists(testClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(testClass);
        }

        // GET: TestClasses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testClass = await _context.Tests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testClass == null)
            {
                return NotFound();
            }

            return View(testClass);
        }

        // POST: TestClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var testClass = await _context.Tests.FindAsync(id);
            _context.Tests.Remove(testClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestClassExists(string id)
        {
            return _context.Tests.Any(e => e.Id == id);
        }

        public IActionResult RandomTest()
        {
            for (int i = 0; i < 1000; i++)
            {
                var name = RandomString(20);

                _context.Tests.Add(new TestClass()
                {
                    Name = name.Substring(15),
                    TipoDoc = name.Substring(1,3),
                    Doc = name.Substring(4,7)
                });
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
