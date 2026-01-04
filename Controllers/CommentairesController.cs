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
    public class CommentairesController : Controller
    {
        private readonly ProblemSolvingPlatformContext _context;

        public CommentairesController(ProblemSolvingPlatformContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(int probId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest(new { error = "Comment content cannot be empty" });
            }

            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized(new { error = "You must be logged in to comment" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            var comment = new Commentaire
            {
                UserId = user.Id,
                Probleme = probId.ToString(),
                Contenu = content,
                DateCreation = DateTime.Now
            };

            _context.Commentaires.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                commentId = comment.CommentaireId,
                userName = string.IsNullOrEmpty(user.FirstName) ? user.UserName : (user.FirstName + " " + user.LastName),
                userProfilePic = user.ProfilePicture ?? "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png",
                date = comment.DateCreation.ToString("MMM dd, yyyy HH:mm"),
                content = comment.Contenu
            });
        }

        // GET: Commentaires
        public async Task<IActionResult> Index()
        {
            var problemSolvingPlatformContext = _context.Commentaires.Include(c => c.User);
            return View(await problemSolvingPlatformContext.ToListAsync());
        }

        // GET: Commentaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentaire = await _context.Commentaires
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CommentaireId == id);
            if (commentaire == null)
            {
                return NotFound();
            }

            return View(commentaire);
        }

        // GET: Commentaires/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Commentaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentaireId,UserId,Probleme,Contenu,DateCreation")] Commentaire commentaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", commentaire.UserId);
            return View(commentaire);
        }

        // GET: Commentaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentaire = await _context.Commentaires.FindAsync(id);
            if (commentaire == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", commentaire.UserId);
            return View(commentaire);
        }

        // POST: Commentaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentaireId,UserId,Probleme,Contenu,DateCreation")] Commentaire commentaire)
        {
            if (id != commentaire.CommentaireId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentaireExists(commentaire.CommentaireId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", commentaire.UserId);
            return View(commentaire);
        }

        // GET: Commentaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentaire = await _context.Commentaires
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CommentaireId == id);
            if (commentaire == null)
            {
                return NotFound();
            }

            return View(commentaire);
        }

        // POST: Commentaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commentaire = await _context.Commentaires.FindAsync(id);
            if (commentaire != null)
            {
                _context.Commentaires.Remove(commentaire);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentaireExists(int id)
        {
            return _context.Commentaires.Any(e => e.CommentaireId == id);
        }
    }
}
