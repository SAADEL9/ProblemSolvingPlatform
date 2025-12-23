using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProblemSolvingPlatform.Models;

namespace ProblemSolvingPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProblemSolvingPlatformContext _context;

        public HomeController(ILogger<HomeController> logger, ProblemSolvingPlatformContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get latest 5 problems (filter out null CreatedAt)
            var latestProblems = await _context.Problemes
                .Where(p => p.CreatedAt != null)
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .ToListAsync();

            // If no problems with CreatedAt, get by ProbId
            if (latestProblems.Count == 0)
            {
                latestProblems = await _context.Problemes
                    .OrderByDescending(p => p.ProbId)
                    .Take(5)
                    .ToListAsync();
            }

            // Get problem counts by difficulty
            var easyCount = await _context.Problemes.Where(p => p.Difficulte == "Easy").CountAsync();
            var mediumCount = await _context.Problemes.Where(p => p.Difficulte == "Medium").CountAsync();
            var hardCount = await _context.Problemes.Where(p => p.Difficulte == "Hard").CountAsync();

            // Prepare difficulty stats
            var difficultyStats = new Dictionary<string, int>
            {
                { "Easy", easyCount },
                { "Medium", mediumCount },
                { "Hard", hardCount }
            };

            // Admin stats
            var totalUsers = await _context.Users.CountAsync();
            var totalProblems = await _context.Problemes.CountAsync();

            ViewData["LatestProblems"] = latestProblems;
            ViewData["DifficultyStats"] = difficultyStats;
            ViewData["TotalUsers"] = totalUsers;
            ViewData["TotalProblems"] = totalProblems;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
