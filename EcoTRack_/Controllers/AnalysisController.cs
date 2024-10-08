using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcoTRack_.Areas.Identity.Data;
using EcoTRack_.NewModel;
using EcoTRack_.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcoTRack_.Controllers
{
    [Authorize]  // Only authorized users can access this controller
    public class AnalysisController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<EcoTrackUser> _userManager;

        public AnalysisController(AppDbContext context, UserManager<EcoTrackUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Analysis
        public async Task<IActionResult> Analysis()
        {
            // Get the currently logged-in user's ID (Uid)
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Register", "Account");

            var userUid = user.Id; // The user's Uid (Identity User ID), expected to be a string

            // Fetch the Electrate data for this user
            var electrateData = await _context.Electrates
                .Where(e => e.Uid == userUid) // Ensure Uid is a string
                .ToListAsync();

            // Fetch the Insight data for this user
            var insightData = await _context.Insights
                .Where(i => i.Uid == userUid) // Ensure Uid is a string
                .ToListAsync();

            // Create the ViewModel to hold both Electrate and Insight data
            var viewModel = new AnalysisViewModel
            {
                ElectrateList = electrateData,
                InsightList = insightData
            };

            return View(viewModel);  // Pass ViewModel to the view
        }

        // GET: Electrates/Details/5
        public async Task<IActionResult> Details(string id) // Change int? id to string id
        {
            if (string.IsNullOrEmpty(id)) // Check for null or empty string
            {
                return NotFound();
            }

            var electrate = await _context.Electrates
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id); // Change m.Id to string comparison
            if (electrate == null)
            {
                return NotFound();
            }

            return View(electrate);
        }

        // GET: Electrates/Create
        public IActionResult Create()
        {
            ViewData["Uid"] = new SelectList(_context.Users, "Uid", "Uid");
            return View();
        }

        // POST: Electrates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Uid,kwr,totalbill,date")] Electrate electrate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(electrate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Uid"] = new SelectList(_context.Users, "Uid", "Uid", electrate.Uid);
            return View(electrate);
        }

        // GET: Electrates/Edit/5
        public async Task<IActionResult> Edit(string id) // Change int? id to string id
        {
            if (string.IsNullOrEmpty(id)) // Check for null or empty string
            {
                return NotFound();
            }

            var electrate = await _context.Electrates.FindAsync(id); // Change to FindAsync with string
            if (electrate == null)
            {
                return NotFound();
            }
            ViewData["Uid"] = new SelectList(_context.Users, "Uid", "Uid", electrate.Uid);
            return View(electrate);
        }

        // POST: Electrates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Uid,kwr,totalbill,date")] Electrate electrate) // Change int id to string id
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
            ViewData["Uid"] = new SelectList(_context.Users, "Uid", "Uid", electrate.Uid);
            return View(electrate);
        }

        // GET: Electrates/Delete/5
        public async Task<IActionResult> Delete(string id) // Change int? id to string id
        {
            if (string.IsNullOrEmpty(id)) // Check for null or empty string
            {
                return NotFound();
            }

            var electrate = await _context.Electrates
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id); // Change to string comparison
            if (electrate == null)
            {
                return NotFound();
            }

            return View(electrate);
        }

        // POST: Electrates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) // Change int id to string id
        {
            var electrate = await _context.Electrates.FindAsync(id); // Change to FindAsync with string
            if (electrate != null)
            {
                _context.Electrates.Remove(electrate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectrateExists(string id) // Change int id to string id
        {
            return _context.Electrates.Any(e => e.Id == id); // Change to string comparison
        }
    }
}
