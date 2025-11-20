# Authentication Setup Guide

## Overview
Login and register functionality has been implemented using ASP.NET Identity with your custom User model.

## Changes Made

### 1. **User Model** (`Models/User.cs`)
- Updated to inherit from `IdentityUser<int>` for ASP.NET Identity integration
- Removed fields that are now handled by IdentityUser (UserId, Username, Email, PasswordHash)
- Retained custom fields: FirstName, LastName, RegistrationDate, ProfilePicture, Role, IsActive
- Kept navigation properties: Classement, Commentaires, Soumissions

### 2. **DbContext** (`Models/ProblemSolvingPlatformContext.cs`)
- Changed to inherit from `IdentityDbContext<User, IdentityRole<int>, int>`
- This automatically manages Identity tables (AspNetUsers, AspNetRoles, etc.)
- Removed the old User table mapping configuration

### 3. **Program.cs**
- Added `AddRazorPages()` for Razor Pages support
- Registered ASP.NET Identity services
- Configured Identity password and lockout policies
- Added authentication and authorization middleware
- Registered `IEmailSender` for email functionality

### 4. **New Services** (`Services/EmailSender.cs`)
- Created email sender service (stub implementation)
- For production, integrate with SendGrid, SMTP, or other email service

### 5. **Existing Razor Pages**
- Login page: `/Identity/Account/Login`
- Register page: `/Identity/Account/Register`
- Logout page: `/Identity/Account/Logout`

### 6. **Navigation**
- Updated `_Layout.cshtml` already includes Login/Register/Logout links in the navbar

## Database Setup

### Option 1: Using Entity Framework Core Migrations (Recommended)
```bash
# Open Package Manager Console in Visual Studio

# Add a migration
Add-Migration AddIdentity

# Apply the migration to database
Update-Database
```

### Option 2: Using SQL Script
1. Open SQL Server Management Studio
2. Connect to your database
3. Open `Scripts/CreateIdentityTables.sql`
4. Execute the script to create all Identity tables

## Testing Login/Register

1. **Start the application**
   ```bash
   dotnet run
   ```

2. **Navigate to Register**
   - Click "Register" link in the navbar
   - Enter email and password (minimum 6 characters)
   - Click Register button

3. **Login**
   - Click "Login" link in the navbar
   - Enter the email and password you registered with
   - Check "Remember me?" to stay logged in
   - Click Log in button

4. **Verify Authentication**
   - After login, the navbar should show "Logout" instead of "Login/Register"
   - You can now access protected pages/features

## Configuration

### Password Requirements
Current settings (in Program.cs):
- Minimum length: 6 characters
- No digit required
- No uppercase/lowercase required
- No special characters required

To change, modify the `IdentityOptions` configuration in `Program.cs`:
```csharp
options.Password.RequireDigit = true;
options.Password.RequireLowercase = true;
options.Password.RequireUppercase = true;
options.Password.RequiredLength = 10;
```

### Lockout Settings
- Default lockout time: 5 minutes after 5 failed attempts
- New users are allowed to be locked out

To change, modify:
```csharp
options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
options.Lockout.MaxFailedAccessAttempts = 5;
```

## Email Configuration

Currently, emails are logged to the console. To enable real email sending:

1. Update `Services/EmailSender.cs` with your email provider
2. Example with SendGrid:
```csharp
public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SendGridClient(_configuration["SendGrid:ApiKey"]);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_configuration["SendGrid:FromEmail"], "ProblemSolvingPlatform"),
            Subject = subject,
            HtmlContent = htmlMessage
        };
        msg.AddTo(new EmailAddress(email));
        await client.SendEmailAsync(msg);
    }
}
```

3. Add NuGet package: `SendGrid`

## Protecting Pages/Actions

To require authentication on a page or action:

### For Razor Pages:
```csharp
[Authorize]
public class MyPage : PageModel
{
    // Only authenticated users can access
}
```

### For Controllers:
```csharp
[Authorize]
public IActionResult MyAction()
{
    // Only authenticated users can access
}
```

### For entire page without specific roles:
```html
@{
    if (!User.Identity?.IsAuthenticated ?? true)
    {
        return RedirectToPage("/Identity/Account/Login");
    }
}
```

## Getting Current User

In Razor Pages or Controllers:
```csharp
// Get the current authenticated user
var user = await _userManager.GetUserAsync(User);
var userId = user?.Id;
var email = user?.Email;
```

## Troubleshooting

### "Invalid object name 'AspNetUsers'"
- Run the SQL script or Entity Framework migrations to create Identity tables
- Check database connection string in `appsettings.json`

### "The table 'dbo.User' not found"
- Old references to the User table might exist
- Ensure you're using the new table name `AspNetUsers`

### Email not sending
- The current implementation is a stub
- Configure a real email provider in `EmailSender.cs`

## Next Steps

1. Run the application and test login/register
2. Create a migration and update the database
3. Customize email templates in `Areas/Identity/Pages/Account/`
4. Implement role-based authorization if needed
5. Add email confirmation logic if required

For more information, visit: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity
