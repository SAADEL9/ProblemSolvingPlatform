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
        private readonly SignInManager<User> _signInManager;
        private readonly ProblemSolvingPlatformContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<EditProfileModel> _logger;

        public EditProfileModel(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ProblemSolvingPlatformContext context, 
            IWebHostEnvironment env, 
            ILogger<EditProfileModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _env = env;
            _logger = logger;
        }

        public User CurrentUser { get; set; } = default!;

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        // Bind property for uploaded profile image
        [BindProperty]
        public IFormFile? ProfileImageFile { get; set; }

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
            // Global try-catch to prevent crash
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

                // Update user properties
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.PhoneNumber = Input.PhoneNumber;

                string? newFileFullPath = null;

                // Handle profile image upload
                if (ProfileImageFile != null && ProfileImageFile.Length > 0)
                {
                    try
                    {
                        _logger.LogInformation("Processing profile image upload: {FileName}", ProfileImageFile.FileName);

                        // Validate file type
                        var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var ext = Path.GetExtension(ProfileImageFile.FileName).ToLowerInvariant();
                        if (!allowed.Contains(ext))
                        {
                            _logger.LogWarning("Invalid file extension: {Extension}", ext);
                            ModelState.AddModelError("ProfileImageFile", "Invalid image format. Allowed: jpg, jpeg, png, gif.");
                            return Page();
                        }

                        // Limit size to 2 MB
                        const long maxSize = 2 * 1024 * 1024;
                        if (ProfileImageFile.Length > maxSize)
                        {
                            _logger.LogWarning("File too large: {Size} bytes", ProfileImageFile.Length);
                            ModelState.AddModelError("ProfileImageFile", "Image size must be 2 MB or less.");
                            return Page();
                        }

                        // Robust path handling
                        var webRoot = _env.WebRootPath;
                        if (string.IsNullOrEmpty(webRoot))
                        {
                            webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        }

                        var uploadsFolder = Path.Combine(webRoot, "uploads", "profiles");
                        
                        // Ensure directory exists with explicit try-catch
                        try 
                        {
                            if (!Directory.Exists(uploadsFolder))
                            {
                                _logger.LogInformation("Creating uploads directory: {Path}", uploadsFolder);
                                Directory.CreateDirectory(uploadsFolder);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to create uploads directory: {Path}", uploadsFolder);
                            ModelState.AddModelError(string.Empty, "Server error: Could not create upload directory.");
                            return Page();
                        }

                        // Delete old file if exists
                        if (!string.IsNullOrEmpty(user.ProfilePicture))
                        {
                            try
                            {
                                // Only try to delete if it's an uploaded file (not a default or external URL)
                                if (user.ProfilePicture.StartsWith("/uploads/", StringComparison.OrdinalIgnoreCase))
                                {
                                    var oldPath = Path.Combine(webRoot, user.ProfilePicture.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                                    if (System.IO.File.Exists(oldPath))
                                    {
                                        _logger.LogInformation("Deleting old profile picture: {Path}", oldPath);
                                        System.IO.File.Delete(oldPath);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning(ex, "Could not delete old profile picture: {Path}", user.ProfilePicture);
                                // Not a fatal error, continue
                            }
                        }

                        var fileName = Guid.NewGuid().ToString("N") + ext;
                        var fullPath = Path.Combine(uploadsFolder, fileName);
                        newFileFullPath = fullPath;

                        try
                        {
                            _logger.LogInformation("Reading uploaded file into memory for safe write: {FileName}", ProfileImageFile.FileName);
                            // Read file into memory once
                            byte[] fileBytes;
                            using (var ms = new MemoryStream())
                            {
                                await ProfileImageFile.CopyToAsync(ms);
                                fileBytes = ms.ToArray();
                            }

                            _logger.LogInformation("Saving new profile picture to disk: {Path}", fullPath);
                            try
                            {
                                // Write bytes atomically
                                await System.IO.File.WriteAllBytesAsync(fullPath, fileBytes);

                                // Verify file exists
                                if (!System.IO.File.Exists(fullPath))
                                {
                                    throw new IOException("File was not created successfully");
                                }

                                user.ProfilePicture = "/uploads/profiles/" + fileName;
                                _logger.LogInformation("New profile picture path saved to user: {Path}", user.ProfilePicture);
                            }
                            catch (IOException ioEx)
                            {
                                _logger.LogWarning(ioEx, "IO error while writing file to disk: {Path} - attempting in-memory fallback", fullPath);

                                // Fallback: store as data URI in the database to avoid file system dependency
                                try
                                {
                                    var base64 = Convert.ToBase64String(fileBytes);
                                    var contentType = ProfileImageFile.ContentType ?? "image/png";
                                    user.ProfilePicture = $"data:{contentType};base64,{base64}";
                                    _logger.LogInformation("Saved profile picture as data URI for user: {UserId}", user.Id);

                                    // Ensure we don't attempt to delete/move a non-existent file later
                                    newFileFullPath = null;
                                }
                                catch (Exception fallbackEx)
                                {
                                    _logger.LogError(fallbackEx, "Fallback in-memory save also failed");
                                    ModelState.AddModelError("ProfileImageFile", "Failed to save the image. The file may be in use or the disk is full.");
                                    return Page();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to process uploaded file: {FileName}", ProfileImageFile.FileName);
                            ModelState.AddModelError("ProfileImageFile", "Failed to save the image to the server: " + ex.Message);
                            return Page();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unexpected error during profile image processing");
                        ModelState.AddModelError("ProfileImageFile", "An unexpected error occurred while processing the image.");
                        return Page();
                    }
                }

                try
                {
                    _logger.LogInformation("Attempting to update user: {UserId}", user.Id);
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User update succeeded. Refreshing sign-in.");
                        
                        // Wrap RefreshSignInAsync in try-catch as it's not critical for the update itself
                        try
                        {
                            await _signInManager.RefreshSignInAsync(user);
                        }
                        catch (Exception refreshEx)
                        {
                            _logger.LogWarning(refreshEx, "Failed to refresh sign-in after profile update.");
                        }
                        
                        TempData["SuccessMessage"] = "Your profile has been updated successfully!";
                        return RedirectToPage("./Profile");
                    }

                    _logger.LogError("User update failed: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));

                    // If UpdateAsync failed, cleanup new file and show errors
                    if (newFileFullPath != null && System.IO.File.Exists(newFileFullPath))
                    {
                        try 
                        { 
                            _logger.LogInformation("Cleaning up file after failed update: {Path}", newFileFullPath);
                            System.IO.File.Delete(newFileFullPath); 
                        } 
                        catch { }
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return Page();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Critical error during user update in EditProfile OnPostAsync");
                    ModelState.AddModelError(string.Empty, "An internal error occurred while saving your profile.");
                    
                    // Cleanup new file if user update failed
                    if (newFileFullPath != null && System.IO.File.Exists(newFileFullPath))
                    {
                        try 
                        { 
                            System.IO.File.Delete(newFileFullPath); 
                        } 
                        catch { }
                    }
                    
                    return Page();
                }
            }
            catch (Exception globalEx)
            {
                // Absolute safety net for any other unhandled exception
                _logger.LogError(globalEx, "Unhandled exception in EditProfile OnPostAsync");
                ModelState.AddModelError(string.Empty, "Available to save profile due to an unexpected error.");
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
