# Quick Reference Card

## ?? Setup (One Time)

```bash
# Step 1: Create Identity tables
Add-Migration AddIdentity
Update-Database

# Step 2: Run app
dotnet run
```

## ?? Login/Register URLs

| Page | URL |
|------|-----|
| Register | `/Identity/Account/Register` |
| Login | `/Identity/Account/Login` |
| Logout | `/Identity/Account/Logout` |

Or use navbar links (Login/Register/Logout buttons)

## ?? Code Snippets

### Inject UserManager
```csharp
public class MyController : Controller
{
    private readonly UserManager<User> _userManager;
    
    public MyController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
}
```

### Get Current User
```csharp
var user = await _userManager.GetUserAsync(User);
var id = user.Id;
var email = user.Email;
var name = user.FirstName;
```

### Check if Authenticated
```csharp
// In Controller
if (User.Identity?.IsAuthenticated ?? false)
{
    // User is logged in
}

// In Razor View
@if (User.Identity?.IsAuthenticated ?? false)
{
    <p>Logged in as: @User.Identity?.Name</p>
}
```

### Require Authentication
```csharp
[Authorize]  // Only logged-in users
public IActionResult MyAction()
{
    return View();
}

[Authorize(Roles = "Admin")]  // Only admins
public IActionResult AdminPanel()
{
    return View();
}
```

### Logout User
```csharp
var signInManager = HttpContext.RequestServices
    .GetRequiredService<SignInManager<User>>();
await signInManager.SignOutAsync();
```

### Create User (Admin)
```csharp
var user = new User
{
    Email = "test@example.com",
    UserName = "test@example.com",
    FirstName = "John",
    LastName = "Doe"
};

var result = await _userManager.CreateAsync(user, "Password123!");
if (!result.Succeeded)
{
    foreach (var error in result.Errors)
        Console.WriteLine(error.Description);
}
```

### Update User
```csharp
var user = await _userManager.FindByIdAsync(userId.ToString());
user.FirstName = "Jane";
await _userManager.UpdateAsync(user);
```

### Delete User
```csharp
var user = await _userManager.FindByIdAsync(userId.ToString());
await _userManager.DeleteAsync(user);
```

## ?? User Properties Available

```csharp
user.Id                    // User ID
user.Email                 // Email address
user.UserName              // Username (usually email)
user.FirstName             // Your custom field
user.LastName              // Your custom field
user.RegistrationDate      // Your custom field
user.ProfilePicture        // Your custom field
user.Role                  // Your custom field
user.IsActive              // Your custom field
user.EmailConfirmed        // Email confirmed?
user.PhoneNumber           // Phone (if set)
user.TwoFactorEnabled      // 2FA enabled?
user.LockoutEnd            // Locked until when?
user.AccessFailedCount     // Failed login attempts
```

## ?? Common Tasks

### Redirect to Login if Not Authenticated
```csharp
if (!(User?.Identity?.IsAuthenticated ?? false))
{
    return RedirectToPage("/Identity/Account/Login");
}
```

### Get User ID
```csharp
var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
// OR
var user = await _userManager.GetUserAsync(User);
var id = user.Id;
```

### Get User Email
```csharp
var email = User.Identity?.Name;
// OR
var user = await _userManager.GetUserAsync(User);
var email = user.Email;
```

### Verify Password
```csharp
var user = await _userManager.FindByEmailAsync(email);
var passwordValid = await _userManager.CheckPasswordAsync(user, password);
```

### Change Password
```csharp
var user = await _userManager.GetUserAsync(User);
var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
```

## ?? Related Classes

- `UserManager<User>` - User operations (CRUD, password, etc.)
- `SignInManager<User>` - Login/logout operations
- `User` - User model (in Models/User.cs)
- `ProblemSolvingPlatformContext` - Database context

## ?? Settings Location

All settings in `Program.cs`:
- Password policy (search: `options.Password`)
- Lockout policy (search: `options.Lockout`)
- Cookie settings (search: `options.Cookie`)
- Login path (search: `options.LoginPath`)

## ?? Common Issues

### "Invalid object name 'AspNetUsers'"
? Run migrations: `Add-Migration AddIdentity` + `Update-Database`

### "UserManager not injected"
? Add to constructor: `UserManager<User> userManager`

### "Email already exists"
? Email is unique by default. Check database for duplicate.

### "Can't find user"
? Use `FindByEmailAsync()` or `FindByIdAsync()` (not LINQ directly)

### "Password too weak"
? Use at least 6 characters (or change policy in Program.cs)

## ? Pro Tips

1. **Always use UserManager** - Never update User table directly
2. **Use Email as Username** - Both are set to same value
3. **Check IsActive** - Custom field for soft delete
4. **Use Claims** - More flexible than roles
5. **Implement IEmailSender** - For production emails

## ?? Learn More

- `QUICK_START.md` - Getting started
- `AUTHENTICATION_SETUP.md` - Full configuration
- `LOGIN_REGISTER_SUMMARY.md` - All features
- Microsoft Learn: https://learn.microsoft.com/aspnet/core/security/authentication/identity

---

**Bookmark this page! You'll reference it often.**
