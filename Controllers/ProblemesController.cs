using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProblemSolvingPlatform.Models;
using ProblemSolvingPlatform.Services;

namespace ProblemSolvingPlatform.Controllers
{
    public class ProblemesController : Controller
    {
        private readonly ProblemSolvingPlatformContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PistonCodeExecutionService _pistonService;

        public ProblemesController(
            ProblemSolvingPlatformContext context, 
            IHttpClientFactory httpClientFactory,
            PistonCodeExecutionService pistonService)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _pistonService = pistonService;
        }

        // GET: Problemes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Problemes.ToListAsync());
        }

        // GET: Problemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probleme = await _context.Problemes
                .FirstOrDefaultAsync(m => m.ProbId == id);
            if (probleme == null)
            {
                return NotFound();
            }

            return View(probleme);
        }

        // GET: Problemes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Problemes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Title,Descr,Difficulte,Language,FunctionTemplate,TestCases")] Probleme probleme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(probleme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(probleme);
        }

        // GET: Problemes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probleme = await _context.Problemes.FindAsync(id);
            if (probleme == null)
            {
                return NotFound();
            }
            return View(probleme);
        }

        // POST: Problemes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProbId,Title,Descr,Difficulte,Language,FunctionTemplate,TestCases")] Probleme probleme)
        {
            if (id != probleme.ProbId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(probleme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemeExists(probleme.ProbId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(probleme);
        }

        // GET: Problemes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var probleme = await _context.Problemes
                .FirstOrDefaultAsync(m => m.ProbId == id);
            if (probleme == null)
            {
                return NotFound();
            }

            return View(probleme);
        }

        // POST: Problemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var probleme = await _context.Problemes.FindAsync(id);
            if (probleme != null)
            {
                _context.Problemes.Remove(probleme);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Problemes/ExecuteCode
        [HttpPost]
        public async Task<IActionResult> ExecuteCode([FromBody] CodeExecutionRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                
                // Set timeout to prevent hanging
                client.Timeout = TimeSpan.FromSeconds(10);
                
                // Map language names to OneCompiler language codes
                var languageMap = new Dictionary<string, string>
                {
                    { "javascript", "javascript" },
                    { "python", "python3" },
                    { "python3", "python3" },
                    { "java", "java" },
                    { "cpp", "cpp" },
                    { "c++", "cpp" },
                    { "csharp", "csharp" },
                    { "c#", "csharp" },
                    { "go", "go" },
                    { "rust", "rust" },
                    { "php", "php" },
                    { "ruby", "ruby" },
                    { "typescript", "typescript" }
                };

                var language = languageMap.ContainsKey(request.Language?.ToLower() ?? "")
                    ? languageMap[request.Language.ToLower()]
                    : "python3";

                var payload = new
                {
                    language = language,
                    code = request.Code,
                    stdin = request.Input ?? ""
                };

                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(payload),
                    System.Text.Encoding.UTF8,
                    "application/json");

                try
                {
                    var response = await client.PostAsync("https://api.onecompiler.com/api/v1/code/exec", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(new { error = "Failed to execute code", status = response.StatusCode });
                    }
                }
                catch (HttpRequestException hre) when (hre.InnerException is System.Net.Sockets.SocketException)
                {
                    return BadRequest(new 
                    { 
                        error = "Network error: Cannot connect to OneCompiler API. Check your internet connection or firewall settings.",
                        details = hre.Message
                    });
                }
                catch (TaskCanceledException)
                {
                    return BadRequest(new { error = "Request timeout: Code execution took too long (>10 seconds)" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "An error occurred: " + ex.Message, type = ex.GetType().Name });
            }
        }

        // GET: Problemes/GetPistonRuntimes
        [HttpGet]
        public async Task<IActionResult> GetPistonRuntimes()
        {
            try
            {
                var runtimes = await _pistonService.GetRuntimesAsync();
                return Ok(runtimes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Failed to fetch runtimes", details = ex.Message });
            }
        }

        // POST: Problemes/ExecuteCodeWithPiston
        [HttpPost]
        public async Task<IActionResult> ExecuteCodeWithPiston([FromBody] PistonCodeRequest request)
        {
            try
            {
                var languageMap = new Dictionary<string, string>
                {
                    { "javascript", "javascript" },
                    { "js", "javascript" },
                    { "python", "python" },
                    { "python3", "python" },
                    { "py", "python" },
                    { "java", "java" },
                    { "cpp", "c++" },
                    { "c++", "c++" },
                    { "csharp", "csharp" },
                    { "c#", "csharp" },
                    { "cs", "csharp" },
                    { "go", "go" },
                    { "rust", "rust" },
                    { "php", "php" },
                    { "ruby", "ruby" },
                    { "typescript", "typescript" },
                    { "ts", "typescript" }
                };

                var language = languageMap.ContainsKey(request.Language?.ToLower() ?? "")
                    ? languageMap[request.Language.ToLower()]
                    : request.Language?.ToLower() ?? "python";

                var pistonRequest = new Services.PistonExecutionRequest
                {
                    Language = language,
                    Version = request.Version,
                    Code = request.Code,
                    Stdin = request.Input,
                    Args = request.Args,
                    CompileTimeout = 10000,
                    RunTimeout = 3000
                };

                var result = await _pistonService.ExecuteCodeAsync(pistonRequest);
                
                return Ok(new
                {
                    language = result.Language,
                    version = result.Version,
                    output = result.Run.Output,
                    stdout = result.Run.Stdout,
                    stderr = result.Run.Stderr,
                    exitCode = result.Run.Code,
                    compile = result.Compile != null ? new
                    {
                        output = result.Compile.Output,
                        stdout = result.Compile.Stdout,
                        stderr = result.Compile.Stderr,
                        exitCode = result.Compile.Code
                    } : null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "An error occurred: " + ex.Message });
            }
        }

        private bool ProblemeExists(int id)
        {
            return _context.Problemes.Any(e => e.ProbId == id);
        }
    }

    // Request model for code execution
    public class CodeExecutionRequest
    {
        public string Code { get; set; }
        public string Language { get; set; }
        public string Input { get; set; }
    }

    // Request model for Piston code execution
    public class PistonCodeRequest
    {
        public string Code { get; set; }
        public string Language { get; set; }
        public string? Version { get; set; }
        public string? Input { get; set; }
        public string[]? Args { get; set; }
    }
}
