using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProblemSolvingPlatform.Models;

namespace ProblemSolvingPlatform.Controllers
{
    public class SoumissionsController : Controller
    {
        private readonly ProblemSolvingPlatformContext _context;

        public SoumissionsController(ProblemSolvingPlatformContext context)
        {
            _context = context;
        }

        // GET: Soumissions
        public async Task<IActionResult> Index()
        {
            var problemSolvingPlatformContext = _context.Soumissions.Include(s => s.User);
            return View(await problemSolvingPlatformContext.ToListAsync());
        }

        // GET: Soumissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soumission = await _context.Soumissions
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SoumissionId == id);
            if (soumission == null)
            {
                return NotFound();
            }

            return View(soumission);
        }

        // GET: Soumissions/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Soumissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoumissionId,UserId,Code,Probleme,Langage")] Soumission soumission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soumission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", soumission.UserId);
            return View(soumission);
        }

        // GET: Soumissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soumission = await _context.Soumissions.FindAsync(id);
            if (soumission == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", soumission.UserId);
            return View(soumission);
        }

        // POST: Soumissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoumissionId,UserId,Code,Probleme,Langage")] Soumission soumission)
        {
            if (id != soumission.SoumissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soumission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoumissionExists(soumission.SoumissionId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", soumission.UserId);
            return View(soumission);
        }

        // GET: Soumissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soumission = await _context.Soumissions
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SoumissionId == id);
            if (soumission == null)
            {
                return NotFound();
            }

            return View(soumission);
        }

        // POST: Soumissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soumission = await _context.Soumissions.FindAsync(id);
            if (soumission != null)
            {
                _context.Soumissions.Remove(soumission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoumissionExists(int id)
        {
            return _context.Soumissions.Any(e => e.SoumissionId == id);
        }
    }
}
