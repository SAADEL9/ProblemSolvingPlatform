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
    public class ProblemesController : Controller
    {
        private readonly ProblemSolvingPlatformContext _context;

        public ProblemesController(ProblemSolvingPlatformContext context)
        {
            _context = context;
        }

        // GET: Problemes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Problemes.ToListAsync());
        }

        // GET: Problemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probleme = await _context.Problemes
                .FirstOrDefaultAsync(m => m.ProbId == id);
            if (probleme == null)
            {
                return NotFound();
            }

            return View(probleme);
        }

        // GET: Problemes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Problemes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProbId,Title,Descr,Difficulte")] Probleme probleme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(probleme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(probleme);
        }

        // GET: Problemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probleme = await _context.Problemes.FindAsync(id);
            if (probleme == null)
            {
                return NotFound();
            }
            return View(probleme);
        }

        // POST: Problemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProbId,Title,Descr,Difficulte")] Probleme probleme)
        {
            if (id != probleme.ProbId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(probleme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemeExists(probleme.ProbId))
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
            return View(probleme);
        }

        // GET: Problemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probleme = await _context.Problemes
                .FirstOrDefaultAsync(m => m.ProbId == id);
            if (probleme == null)
            {
                return NotFound();
            }

            return View(probleme);
        }

        // POST: Problemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var probleme = await _context.Problemes.FindAsync(id);
            if (probleme != null)
            {
                _context.Problemes.Remove(probleme);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemeExists(int id)
        {
            return _context.Problemes.Any(e => e.ProbId == id);
        }
    }
}
