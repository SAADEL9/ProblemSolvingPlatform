using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ProblemSolvingPlatform.Models;
using ProblemSolvingPlatform.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// DbContext
builder.Services.AddDbContext<ProblemSolvingPlatformContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ProblemSolvingPlatformContext")
    )
);

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

if (!app.Environment.IsDevelopment())
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

    // Optionally create a default admin user (uncomment if needed)
    // var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    // if (adminUser == null)
    // {
    //     var newAdmin = new User 
    //     { 
    //         UserName = "admin",
    //         Email = "admin@example.com",
    //         FirstName = "Admin",
    //         LastName = "User",
    //         RegistrationDate = DateTime.Now,
    //         IsActive = true
    //     };
    //     var result = await userManager.CreateAsync(newAdmin, "Admin123!");
    //     if (result.Succeeded)
    //     {
    //         await userManager.AddToRoleAsync(newAdmin, "Admin");
    //     }
    // }
}

app.Run();