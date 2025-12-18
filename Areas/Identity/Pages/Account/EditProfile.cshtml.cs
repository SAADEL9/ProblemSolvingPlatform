using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProblemSolvingPlatform.Models;
using Microsoft.AspNetCore.Authentication;

namespace ProblemSolvingPlatform.Areas.Identity.Pages.Account
{
    [Authorize]
    public class EditProfileModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ProblemSolvingPlatformContext _context;

        public EditProfileModel(UserManager<User> userManager, ProblemSolvingPlatformContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public User CurrentUser { get; set; } = default!;

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public class InputModel
        {
            [Display(Name = "First Name")]
            public string? FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string? LastName { get; set; }

            [Phone]
            [Display(Name = "Phone Number")]
            public string? PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var currentUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (currentUser == null)
            {
                return NotFound();
            }

            CurrentUser = currentUser;

            Input = new InputModel
            {
                FirstName = CurrentUser.FirstName,
                LastName = CurrentUser.LastName,
                PhoneNumber = CurrentUser.PhoneNumber
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var currentUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (currentUser == null)
            {
                return NotFound();
            }

            CurrentUser = currentUser;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Update user properties
            CurrentUser.FirstName = Input.FirstName;
            CurrentUser.LastName = Input.LastName;
            CurrentUser.PhoneNumber = Input.PhoneNumber;

            var result = await _userManager.UpdateAsync(CurrentUser);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Your profile has been updated successfully!";
                return RedirectToPage("./Profile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAccountAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Delete user data first (submissions, comments, etc.)
            var submissions = _context.Soumissions.Where(s => s.UserId == user.Id);
            _context.Soumissions.RemoveRange(submissions);

            var comments = _context.Commentaires.Where(c => c.UserId == user.Id);
            _context.Commentaires.RemoveRange(comments);

            var ranking = _context.Classements.FirstOrDefault(r => r.UserId == user.Id);
            if (ranking != null)
            {
                _context.Classements.Remove(ranking);
            }

            await _context.SaveChangesAsync();

            // Delete user account
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                return LocalRedirect("/");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
