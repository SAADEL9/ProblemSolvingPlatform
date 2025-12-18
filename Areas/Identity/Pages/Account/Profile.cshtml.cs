using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProblemSolvingPlatform.Models;

namespace ProblemSolvingPlatform.Areas.Identity.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ProblemSolvingPlatformContext _context;

        public ProfileModel(UserManager<User> userManager, ProblemSolvingPlatformContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public User CurrentUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var currentUser = await _context.Users
                .Include(u => u.Soumissions)
                .Include(u => u.Commentaires)
                .Include(u => u.Classement)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (currentUser == null)
            {
                return NotFound();
            }

            CurrentUser = currentUser;

            return Page();
        }
    }
}
