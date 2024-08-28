using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommonsDev.Data;
using DataTablesParser;

namespace CommonsDev.Controllers
{
    public class InstitutesController : CommonsController
    {
        private readonly ApplicationDbContext _context;

        public InstitutesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Institutes
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Institutes
        public async Task<IActionResult> DataTable()
        {
            return View();
        }

        public IActionResult Data()
        {
            return DataTable(_context.Institutes.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                LongName = x.LongName
            }));
        }

        public IActionResult Data2()
        {
            //var parser = new Parser<Institute>(Request.Form, _context.Institutes.Where(x => true));


            var parser = new Parser<Institute>(Request.Form, _context.Institutes.Where(x => true));

            return Json(parser.Parse());
        }

        // GET: Institutes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        public IActionResult RandomInstitutes()
        {
            for (int i = 0; i < 1000; i++)
            {
                var name = RandomString(20);

                _context.Institutes.Add(new Institute()
                {
                    Name = name.PadLeft(5),
                    LongName = name
                });
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Institutes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Institutes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LongName")] Institute institute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(institute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(institute);
        }

        // GET: Institutes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes.FindAsync(id);
            if (institute == null)
            {
                return NotFound();
            }


            return View(institute);
        }

        // POST: Institutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,LongName")] Institute institute)
        {
            if (id != institute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituteExists(institute.Id))
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
            return View(institute);
        }

        // GET: Institutes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institute = await _context.Institutes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (institute == null)
            {
                return NotFound();
            }

            return View(institute);
        }

        // POST: Institutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var institute = await _context.Institutes.FindAsync(id);
            _context.Institutes.Remove(institute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstituteExists(string id)
        {
            return _context.Institutes.Any(e => e.Id == id);
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
