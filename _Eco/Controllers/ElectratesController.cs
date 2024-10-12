using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _Eco.Data;
using _Eco.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace _Eco.Controllers
{
    [Authorize]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID
            var electrates = await _context.Electrates
                                           .Where(e => e.UserId == userId) // Filter by UserId
                                           .ToListAsync();
            return View(electrates);
        }

        // GET: Electrates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var electrate = await _context.Electrates
                                          .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId); // Check UserId
            if (electrate == null)
            {
                return NotFound();
            }

            return View(electrate);
        }


        // GET: Electrates/Create
        public IActionResult Create()
        {
            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Create a list with just the current user
            var users = _context.Users.Where(u => u.Id == userId).ToList();

            // Add a blank option to the SelectList
            var userList = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = "Select your username" } // Add this line
    }.Concat(users.Select(u => new SelectListItem { Value = u.Id, Text = u.UserName })).ToList();

            ViewBag.Users = userList; // Set the ViewBag with the modified user list
            return View();
        }



        // POST: Electrates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Electrates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,kwh,totalbill,date,UserId")] Electrate electrate)
        {
            if (string.IsNullOrEmpty(electrate.UserId)) // Check if UserId is blank
            {
                ModelState.AddModelError("UserId", "Please select your username."); // Add custom error message
            }

            if (ModelState.IsValid)
            {
                electrate.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Ensure UserId is set
                _context.Add(electrate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload users for the dropdown in case of errors
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = _context.Users.Where(u => u.Id == userId).ToList();

            // Add a blank option to the SelectList
            var userList = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = "Select your username" } // Add this line
    }.Concat(users.Select(u => new SelectListItem { Value = u.Id, Text = u.UserName })).ToList();

            ViewBag.Users = userList; // Set the ViewBag with the modified user list

            return View(electrate);
        }




        // GET: Electrates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the Electrate record to edit
            var electrate = await _context.Electrates.FindAsync(id);
            if (electrate == null)
            {
                return NotFound();
            }

            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Create a list with just the current user
            var users = _context.Users.Where(u => u.Id == userId).ToList();

            // Add a blank option to the SelectList
            var userList = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = "Select your username" } // Add this line
    }.Concat(users.Select(u => new SelectListItem { Value = u.Id, Text = u.UserName })).ToList();

            ViewBag.Users = userList; // Set the ViewBag with the modified user list

            return View(electrate);
        }



        // POST: Electrates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,kwh,totalbill,date,UserId")] Electrate electrate)
        {
            if (id != electrate.Id)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(electrate.UserId)) // Check if UserId is blank
            {
                ModelState.AddModelError("UserId", "Please select your username."); // Add custom error message
            }

            if (ModelState.IsValid)
            {
                try
                {
                    electrate.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Ensure UserId is set
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

            // Reload users for the dropdown in case of errors
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = _context.Users.Where(u => u.Id == userId).ToList();

            // Add a blank option to the SelectList
            var userList = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = "Select your username" } // Add this line
    }.Concat(users.Select(u => new SelectListItem { Value = u.Id, Text = u.UserName })).ToList();

            ViewBag.Users = userList; // Set the ViewBag with the modified user list

            return View(electrate);
        }





        // GET: Electrates/Delete/5
        // GET: Electrates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var electrate = await _context.Electrates
                                          .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId); // Check UserId
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var electrate = await _context.Electrates
                                          .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId); // Check UserId
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
