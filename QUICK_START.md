# Quick Start: Login & Register

## ? 3-Step Setup

### Step 1: Create Database Tables
Open Package Manager Console in Visual Studio and run:
```bash
Add-Migration AddIdentity
Update-Database
```

Or, if you prefer SQL:
1. Open SQL Server Management Studio
2. Run: `Scripts/CreateIdentityTables.sql`

### Step 2: Run Your App
```bash
dotnet run
```

### Step 3: Test It
1. Click **Register** in navbar
2. Enter email & password (min 6 chars)
3. Click **Register**
4. You're logged in! ?
5. Click **Logout** to test logout
6. Click **Login** to test login

## ?? Where to Find Pages

- **Register**: `/Identity/Account/Register`
- **Login**: `/Identity/Account/Login`
- **Logout**: `/Identity/Account/Logout`

Or use navbar links!

## ?? Get Current User in Code

```csharp
var user = await _userManager.GetUserAsync(User);
var userId = user.Id;
var email = user.Email;
```

## ? Check if Logged In

In Razor markup:
```html
@if (User.Identity?.IsAuthenticated ?? false)
{
    <p>Hello @User.Identity?.Name</p>
}
```

## ?? Protect a Page/Action

Add [Authorize]:
```csharp
[Authorize]
public IActionResult MyProtectedAction()
{
    return View();
}
```

## ?? Email Service

Currently logs to console. To use real email:

1. Install NuGet package: `SendGrid`
2. Edit `Services/EmailSender.cs`
3. Add your SendGrid API key to `appsettings.json`

## ?? What Was Changed

? User model updated to use ASP.NET Identity
? DbContext updated with IdentityDbContext
? Program.cs configured with Identity services
? Login & Register pages ready to use
? Email service created
? Navigation links working

## ?? Important

**You MUST create the database tables** (Step 1) before testing!

## ?? More Help

See detailed guides:
- `AUTHENTICATION_SETUP.md` - Full setup guide
- `LOGIN_REGISTER_SUMMARY.md` - Features overview
