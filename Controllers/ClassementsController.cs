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
    public class ClassementsController : Controller
    {
        private readonly ProblemSolvingPlatformContext _context;

        public ClassementsController(ProblemSolvingPlatformContext context)
        {
            _context = context;
        }

        // GET: Classements
        public async Task<IActionResult> Index()
        {
            var problemSolvingPlatformContext = _context.Classements.Include(c => c.User);
            return View(await problemSolvingPlatformContext.ToListAsync());
        }

        // GET: Classements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classement = await _context.Classements
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ClassementId == id);
            if (classement == null)
            {
                return NotFound();
            }

            return View(classement);
        }

        // GET: Classements/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Classements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassementId,UserId,Score,Rang")] Classement classement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", classement.UserId);
            return View(classement);
        }

        // GET: Classements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classement = await _context.Classements.FindAsync(id);
            if (classement == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", classement.UserId);
            return View(classement);
        }

        // POST: Classements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassementId,UserId,Score,Rang")] Classement classement)
        {
            if (id != classement.ClassementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassementExists(classement.ClassementId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", classement.UserId);
            return View(classement);
        }

        // GET: Classements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classement = await _context.Classements
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ClassementId == id);
            if (classement == null)
            {
                return NotFound();
            }

            return View(classement);
        }

        // POST: Classements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classement = await _context.Classements.FindAsync(id);
            if (classement != null)
            {
                _context.Classements.Remove(classement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassementExists(int id)
        {
            return _context.Classements.Any(e => e.ClassementId == id);
        }
    }
}
