using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _Eco.Data;
using _Eco.Models;

namespace _Eco.Controllers
{
    public class ElectratesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElectratesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Electrates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Electrates.ToListAsync());
        }

        // GET: Electrates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electrate = await _context.Electrates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (electrate == null)
            {
                return NotFound();
            }

            return View(electrate);
        }

        // GET: Electrates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Electrates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,kwh,totalbill,date")] Electrate electrate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(electrate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(electrate);
        }

        // GET: Electrates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electrate = await _context.Electrates.FindAsync(id);
            if (electrate == null)
            {
                return NotFound();
            }
            return View(electrate);
        }

        // POST: Electrates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,kwh,totalbill,date")] Electrate electrate)
        {
            if (id != electrate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electrate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectrateExists(electrate.Id))
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
            return View(electrate);
        }

        // GET: Electrates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electrate = await _context.Electrates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (electrate == null)
            {
                return NotFound();
            }

            return View(electrate);
        }

        // POST: Electrates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electrate = await _context.Electrates.FindAsync(id);
            if (electrate != null)
            {
                _context.Electrates.Remove(electrate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectrateExists(int id)
        {
            return _context.Electrates.Any(e => e.Id == id);
        }
    }
}
