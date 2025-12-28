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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ProblemSolvingPlatform.Areas.Identity.Pages.Account
{
    [Authorize]
    public class EditProfileModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ProblemSolvingPlatformContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<EditProfileModel> _logger;

        [ActivatorUtilitiesConstructor]
        public EditProfileModel(UserManager<User> userManager, ProblemSolvingPlatformContext context, IWebHostEnvironment env, ILogger<EditProfileModel> logger)
        {
            _userManager = userManager;
            _context = context;
            _env = env;
            _logger = logger;
        }

        public User CurrentUser { get; set; } = default!;

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        // Bind property for uploaded profile image
        [BindProperty]
        public IFormFile? ProfileImage { get; set; }

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

            CurrentUser = user;

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
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                CurrentUser = user;

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                // Update user properties on the Identity user instance
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.PhoneNumber = Input.PhoneNumber;

                string? newFileRelative = null;
                string? newFileFullPath = null;

                // Handle profile image upload
                if (ProfileImage != null && ProfileImage.Length > 0)
                {
                    // Validate file type
                    var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var ext = Path.GetExtension(ProfileImage.FileName).ToLowerInvariant();
                    if (!allowed.Contains(ext))
                    {
                        ModelState.AddModelError("ProfileImage", "Invalid image format. Allowed: jpg, jpeg, png, gif.");
                        return Page();
                    }

                    // Limit size to 2 MB
                    const long maxSize = 2 * 1024 * 1024;
                    if (ProfileImage.Length > maxSize)
                    {
                        ModelState.AddModelError("ProfileImage", "Image size must be 2 MB or less.");
                        return Page();
                    }

                    // Prepare directory
                    var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "profiles");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    // Delete old file if exists and is under uploads/profiles
                    if (!string.IsNullOrEmpty(user.ProfilePicture) && user.ProfilePicture.StartsWith("/uploads/profiles/", StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            var oldPath = Path.Combine(_env.WebRootPath ?? "wwwroot", user.ProfilePicture.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Failed deleting old profile image");
                        }
                    }

                    var fileName = Guid.NewGuid().ToString("N") + ext;
                    var fullPath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await ProfileImage.CopyToAsync(stream);
                    }

                    newFileRelative = "/uploads/profiles/" + fileName;
                    newFileFullPath = fullPath;

                    // Tentatively set
                    user.ProfilePicture = newFileRelative;
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Ensure EF context reflects changes (some stores may require explicit save)
                    try
                    {
                        _context.Users.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        // If EF save fails, roll back uploaded file
                        if (newFileFullPath != null && System.IO.File.Exists(newFileFullPath))
                        {
                            try { System.IO.File.Delete(newFileFullPath); } catch { }
                        }

                        _logger.LogError(ex, "EF SaveChanges failed after updating user profile.");
                        TempData["ErrorMessage"] = "Saved identity changes but failed to persist to DB.";
                        return RedirectToPage("./Profile");
                    }

                    TempData["SuccessMessage"] = "Your profile has been updated successfully!";
                    return RedirectToPage("./Profile");
                }

                // If update failed, delete uploaded file to avoid orphan
                if (newFileFullPath != null && System.IO.File.Exists(newFileFullPath))
                {
                    try { System.IO.File.Delete(newFileFullPath); } catch { }
                }

                // Surface identity update errors
                var aggregated = string.Join("; ", result.Errors.Select(e => e.Description));
                TempData["ErrorMessage"] = "Failed to update profile: " + aggregated;

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return Page();
            }
            catch (Exception ex)
            {
                // Log and show user-friendly message instead of letting app crash
                _logger.LogError(ex, "Unexpected error in EditProfile OnPostAsync");
                TempData["ErrorMessage"] = "An unexpected error occurred while saving your profile. Please try again later.";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAccountAsync()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in DeleteAccount");
                TempData["ErrorMessage"] = "An unexpected error occurred while deleting your account. Please try again later.";
                return Page();
            }
        }
    }
}
