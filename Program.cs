using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ProblemSolvingPlatform.Models;
using ProblemSolvingPlatform.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Create a logger factory early to use in global handlers
using var loggerFactory = LoggerFactory.Create(lb =>
{
    lb.AddConsole();
    lb.AddDebug();
});
var globalLogger = loggerFactory.CreateLogger("Global");

// Helper to write unhandled exceptions to a file for post-mortem
void WriteUnhandledToFile(Exception? ex)
{
    try
    {
        var logsDir = Path.Combine(Directory.GetCurrentDirectory(), "logs");
        if (!Directory.Exists(logsDir)) Directory.CreateDirectory(logsDir);
        var path = Path.Combine(logsDir, "unhandled.log");
        var text = $"[{DateTime.UtcNow:O}] Unhandled: {ex?.GetType().FullName}: {ex?.Message}\n{ex?.StackTrace}\n\n";
        File.AppendAllText(path, text);
    }
    catch { }
}

// Global unhandled exception handlers - log to console/file
AppDomain.CurrentDomain.UnhandledException += (s, e) =>
{
    try
    {
        globalLogger.LogCritical(e.ExceptionObject as Exception, "Unhandled exception (AppDomain)");
        WriteUnhandledToFile(e.ExceptionObject as Exception);
    }
    catch { }
};

TaskScheduler.UnobservedTaskException += (s, e) =>
{
    try
    {
        globalLogger.LogError(e.Exception, "Unobserved task exception");
        WriteUnhandledToFile(e.Exception);
    }
    catch { }
};

// Configure Kestrel limits (allow larger uploads if needed)
builder.WebHost.ConfigureKestrel(options =>
{
    // Increase request body size to 50 MB
    options.Limits.MaxRequestBodySize = 50 * 1024 * 1024; // 50 MB
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// DbContext
builder.Services.AddDbContext<ProblemSolvingPlatformContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ProblemSolvingPlatformContext")
    )
);

// Configure form options for multipart uploads
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB
});

// Add HttpClientFactory for API calls
builder.Services.AddHttpClient();

// Add ASP.NET Identity
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<ProblemSolvingPlatformContext>()
    .AddDefaultTokenProviders();

// Add Email Sender
builder.Services.AddScoped<IEmailSender, EmailSender>();

// Add Piston Code Execution Service
builder.Services.AddScoped<PistonCodeExecutionService>();

// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    // Sign-in settings
    options.SignIn.RequireConfirmedEmail = false;
});

// Configure application cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.Name = "ProblemSolvingPlatform.AuthCookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
});

var app = builder.Build();

// Show developer exception page in development for detailed errors
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 

app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Seed roles and admin user
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    // Ensure Admin role exists
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin" });
    }

    // Ensure User role exists
    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole<int> { Name = "User" });
    }
}

// Wrap Run in try/catch to log fatal errors
try
{
    app.Run();
}
catch (Exception ex)
{
    globalLogger.LogCritical(ex, "Host terminated unexpectedly");
    WriteUnhandledToFile(ex);
    throw;
}