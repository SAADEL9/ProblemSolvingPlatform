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
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace ProblemSolvingPlatform.Controllers
{
    public class ProblemesController : Controller
    {
        private readonly ProblemSolvingPlatformContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PistonCodeExecutionService _pistonService;
        private readonly UserManager<User> _userManager;

        public ProblemesController(
            ProblemSolvingPlatformContext context,
            IHttpClientFactory httpClientFactory,
            PistonCodeExecutionService pistonService,
            UserManager<User> userManager)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _pistonService = pistonService;
            _userManager = userManager;
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

            // Load comments for this problem
            var comments = await _context.Commentaires
                .Include(c => c.User)
                .Where(c => c.Probleme == id.ToString())
                .OrderByDescending(c => c.DateCreation)
                .ToListAsync();
            
            ViewBag.Comments = comments;

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

                var payload = new OneCompilerPayload
                {
                    Language = language,
                    Code = request.Code,
                    Stdin = request.Input ?? ""
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

                return Ok(new PistonExecutionResponse
                {
                    Language = result.Language,
                    Version = result.Version,
                    Output = result.Run.Output,
                    Stdout = result.Run.Stdout,
                    Stderr = result.Run.Stderr,
                    ExitCode = result.Run.Code,
                    Compile = result.Compile != null ? new PistonCompileResult
                    {
                        Output = result.Compile.Output,
                        Stdout = result.Compile.Stdout,
                        Stderr = result.Compile.Stderr,
                        ExitCode = result.Compile.Code
                    } : null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "An error occurred: " + ex.Message });
            }
        }

        // POST: Problemes/RunTests - Optimized batch test execution
        [HttpPost]
        public async Task<IActionResult> RunTests([FromBody] RunTestsRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Code) || request.Tests == null)
            {
                return BadRequest(new { error = "Invalid request" });
            }

            try
            {
                var language = (request.Language ?? "python").ToLower();

                // Optimized batch execution for Python
                if (language.StartsWith("py"))
                {
                    var harness = GeneratePythonHarness(request.Code ?? string.Empty, request.Tests.Select(t => t.Input ?? string.Empty).ToArray());

                    var pistonRequest = new PistonExecutionRequest
                    {
                        Language = "python",
                        Code = harness,
                        Stdin = string.Empty,
                        Version = request.Version
                    };

                    var result = await _pistonService.ExecuteCodeAsync(pistonRequest);
                    var output = result?.Run?.Output ?? result?.Run?.Stdout ?? string.Empty;

                    try
                    {
                        var parsed = JsonSerializer.Deserialize<object>(output);
                        return Ok(new TestResultsResponse { Results = parsed });
                    }
                    catch
                    {
                        return Ok(new TestResultsResponse { Raw = output });
                    }
                }

                // Fallback for other languages: run first test only
                var fallbackReq = new PistonExecutionRequest
                {
                    Language = language,
                    Code = request.Code,
                    Stdin = request.Tests.FirstOrDefault()?.Input ?? string.Empty
                };

                var fallbackRes = await _pistonService.ExecuteCodeAsync(fallbackReq);
                return Ok(new TestResultsResponse { Output = fallbackRes?.Run?.Output ?? fallbackRes?.Run?.Stdout ?? string.Empty });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: Problemes/SubmitSolution - Submit and validate solution (single test quick check)
        [HttpPost]
        public async Task<IActionResult> SubmitSolution()
        {
            // Read raw body to give clearer errors when client sends malformed JSON
            string body;
            using (var reader = new System.IO.StreamReader(Request.Body))
            {
                body = await reader.ReadToEndAsync();
            }

            SubmitSolutionRequest? request = null;
            try
            {
                if (string.IsNullOrWhiteSpace(body))
                {
                    return BadRequest(new { error = "Empty request body" });
                }

                request = JsonSerializer.Deserialize<SubmitSolutionRequest>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (request == null || string.IsNullOrWhiteSpace(request.Code) || request.ProblemId == 0)
                {
                    return BadRequest(new { error = "Invalid submission: missing Code or ProblemId" });
                }
            }
            catch (JsonException jex)
            {
                return BadRequest(new { error = "Invalid JSON payload", details = jex.Message });
            }

            User? user = null;
            try
            {
                user = await _userManager.GetUserAsync(User);
                if (user == null) return Unauthorized(new { error = "User not authenticated" });

                var problem = await _context.Problemes.FindAsync(request.ProblemId);
                if (problem == null) return NotFound(new { error = "Problem not found" });

                // Parse test cases and pick first
                List<TestCase>? testCases;
                try
                {
                    testCases = JsonSerializer.Deserialize<List<TestCase>>(problem.TestCases ?? "[]", new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (testCases == null || testCases.Count == 0) return BadRequest(new { error = "No test cases defined for this problem" });
                }
                catch (JsonException)
                {
                    return BadRequest(new { error = "Invalid test cases format" });
                }

                var first = testCases.First();
                var language = (request.Language ?? problem.Language ?? "python").ToLower();

                // 1. Prepare Execution
                var inputs = testCases.Select(t => t.Input ?? string.Empty).ToArray();
                string executionCode = request.Code;
                string execLang = language;

                // Map common names to piston languages
                var langMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    {"python","python"},{"python3","python"},{"py","python"},
                    {"javascript","javascript"},{"js","javascript"},
                    {"java","java"}, {"cpp","c++"},{"c++","c++"},
                    {"csharp","csharp"},{"c#","csharp"}
                };

                if (langMap.ContainsKey(execLang)) execLang = langMap[execLang];

                // Use harness for Python to handle function calls
                if (execLang == "python")
                {
                    executionCode = GeneratePythonHarness(request.Code, inputs);
                }

                var pistonReq = new Services.PistonExecutionRequest
                {
                    Language = execLang,
                    Code = executionCode,
                    Stdin = execLang == "python" ? string.Empty : (testCases.FirstOrDefault()?.Input ?? string.Empty)
                };

                // 2. Execute
                var execResult = await _pistonService.ExecuteCodeAsync(pistonReq);
                var rawOutput = execResult?.Run?.Output ?? execResult?.Run?.Stdout ?? string.Empty;

                // 3. Evaluate results
                int passedCount = 0;
                bool overallPassed = false;
                List<object> checkResults = new List<object>();

                if (execLang == "python")
                {
                    try 
                    {
                        // Python returns a JSON array of results from the harness
                        var results = JsonSerializer.Deserialize<List<PythonExecutionResult>>(rawOutput, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (results != null)
                        {
                            for (int i = 0; i < testCases.Count && i < results.Count; i++)
                            {
                                var tc = testCases[i];
                                var expectedRaw = tc.Expected ?? tc.Output ?? tc.ExpectedOutput ?? string.Empty;
                                
                                var expectedNorm = NormalizeStringOrJson(expectedRaw);
                                var actualNorm = NormalizeStringOrJson(results[i].Output?.ToString() ?? string.Empty);
                                
                                bool tcPassed = string.Equals(actualNorm, expectedNorm, StringComparison.Ordinal);
                                if (tcPassed) passedCount++;
                                
                                checkResults.Add(new TestCaseCheckResult { 
                                    TestCase = i + 1, 
                                    Passed = tcPassed, 
                                    Expected = expectedRaw, 
                                    Actual = results[i].Output 
                                });
                            }
                        }
                    }
                    catch 
                    {
                        // Fallback/Error in parsing harness output
                        return BadRequest(new { error = "Evaluation error: Failed to parse execution results", raw = rawOutput });
                    }
                }
                else 
                {
                    // Fallback for other languages (evaluate only first test case for now as harness is language specific)
                    var firstTest = testCases.First();
                    var expectedRaw = firstTest.GetExpectedValue();
                    var expectedNorm = NormalizeStringOrJson(expectedRaw);
                    var actualNorm = NormalizeStringOrJson(rawOutput);
                    bool tcPassed = string.Equals(actualNorm, expectedNorm, StringComparison.Ordinal);
                    if (tcPassed) passedCount = 1;

                    checkResults.Add(new TestCaseCheckResult { TestCase = 1, Passed = tcPassed, Expected = expectedRaw, Actual = rawOutput });
                }

                overallPassed = passedCount == testCases.Count;

                // 4. Save submission
                var submission = new Soumission
                {
                    UserId = user.Id,
                    ProbId = request.ProblemId,
                    Code = request.Code,
                    Probleme = problem.Title ?? "Unknown",
                    Langage = request.Language ?? problem.Language ?? "python",
                    IsPassed = overallPassed,
                    TestsPassed = passedCount,
                    TestsTotal = testCases.Count,
                    PointsEarned = overallPassed ? 10 : 0, // Example point logic
                    SubmittedAt = DateTime.UtcNow
                };

                _context.Soumissions.Add(submission);
                await _context.SaveChangesAsync();

                // 5. Update Leaderboard if passed
                if (overallPassed)
                {
                    await UpdateLeaderboardRankings(user.Id, request.ProblemId, submission.PointsEarned ?? 0);
                }

                return Ok(new
                {
                    passed = overallPassed,
                    message = overallPassed ? "Correct! All tests passed." : $"Partially correct: {passedCount}/{testCases.Count} tests passed.",
                    results = checkResults
                });
            }
            catch (Exception ex)
            {
                var fullError = ex.Message;
                if (ex.InnerException != null)
                {
                    fullError += " | Inner: " + ex.InnerException.Message;
                }

                return BadRequest(new 
                { 
                    error = $"Submission error: {fullError}", 
                    stack = ex.StackTrace
                });
            }
        }

        // Helper: Normalize string or JSON for comparison
        private string NormalizeStringOrJson(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Handle line endings first to match client-side replace(/\r/g,'')
            input = input.Replace("\r", "").Trim();

            // Try to parse as JSON and normalize
            try
            {
                using var doc = JsonDocument.Parse(input);
                return NormalizeJsonElement(doc.RootElement);
            }
            catch
            {
                // Not valid JSON, return trimmed string
                return input;
            }
        }

        // Helper: Generate Python test harness
        private string GeneratePythonHarness(string code, string[] inputs)
        {
            var inputsJson = JsonSerializer.Serialize(inputs);
            var lines = new List<string>
            {
                "import ast, json, inspect",
                "# --- user code ---",
                code ?? string.Empty,
                "# --- harness runner ---",
                $"inputs = {inputsJson}",
                "def _call(inp):",
                "    try:",
                "        try:",
                "            parsed = ast.literal_eval('(' + inp + ')')",
                "        except Exception:",
                "            parsed = ast.literal_eval(inp)",
                "    except Exception as e:",
                "        raise Exception('Failed to parse input: ' + str(e))",
                "    ",
                "    # Try to find a suitable function to call",
                "    func = None",
                "    # Priority 1: Standard names",
                "    for fn_name in ('twoSum','solution','main','solve','isPalindrome','reverseString','fibonacci','binarySearch','isValidParentheses','mergeSortedArrays','maxSubArray','rotate','exist','longestValidParentheses'):",
                "        if fn_name in globals() and callable(globals()[fn_name]):",
                "            func = globals()[fn_name]",
                "            break",
                "    ",
                "    # Priority 2: Any function defined in the user code (not from imports)",
                "    if not func:",
                "        functions = [obj for name, obj in globals().items() if name != '_call' and callable(obj) and obj.__module__ == '__main__']",
                "        if functions:",
                "            func = functions[0]",
                "            ",
                "    if not func:",
                "        raise Exception('No callable function found')",
                "        ",
                "    try:",
                "        if isinstance(parsed, (list, tuple)): ",
                "            try:",
                "                return func(*parsed)",
                "            except TypeError:",
                "                return func(parsed)",
                "        else:",
                "            return func(parsed)",
                "    except Exception:",
                "        try:",
                "            return func(parsed)",
                "        except Exception:",
                "            try:",
                "                return func()",
                "            except Exception:",
                "                raise",
                "results = []",
                "for inp in inputs:",
                "    try:",
                "        out = _call(inp)",
                "        results.append({'status':'ok','output': out})",
                "    except Exception as e:",
                "        results.append({'status':'error','error': str(e)})",
                "print(json.dumps(results))"
            };
            return string.Join("\n", lines);
        }


        // Helper model for Python execution result
        public class PythonExecutionResult
        {
            public string? Status { get; set; }
            public object? Output { get; set; }
            public string? Error { get; set; }
        }

        // Helper: Normalize JsonElement to canonical string form
        private string NormalizeJsonElement(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.Object => JsonSerializer.Serialize(
                    JsonSerializer.Deserialize<Dictionary<string, object>>(element.GetRawText()),
                    new JsonSerializerOptions { WriteIndented = false }
                ),
                JsonValueKind.Array => JsonSerializer.Serialize(
                    JsonSerializer.Deserialize<List<object>>(element.GetRawText()),
                    new JsonSerializerOptions { WriteIndented = false }
                ),
                JsonValueKind.String => element.GetString() ?? string.Empty,
                JsonValueKind.Number => element.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Null => "null",
                _ => element.GetRawText()
            };
        }

        // Helper method to update leaderboard rankings
        private async Task UpdateLeaderboardRankings(int userId, int problemId, int points)
        {
            try
            {
                // 1. Check if user already solved this problem
                var alreadySolved = await _context.Soumissions
                    .AnyAsync(s => s.UserId == userId && s.ProbId == problemId && s.IsPassed == true && s.SubmittedAt < DateTime.UtcNow.AddSeconds(-2));

                var record = await _context.Classements.FirstOrDefaultAsync(c => c.UserId == userId);
                if (record == null)
                {
                    record = new Classement
                    {
                        UserId = userId,
                        Score = "0",
                        Rang = 0,
                        TotalPoints = points,
                        ProblemsSolved = 1,
                        LastUpdated = DateTime.UtcNow
                    };
                    _context.Classements.Add(record);
                }
                else
                {
                    // Only add points and solve count if not already solved before
                    if (!alreadySolved)
                    {
                        record.TotalPoints += points;
                        record.ProblemsSolved += 1;
                    }
                    record.LastUpdated = DateTime.UtcNow;
                }

                // Update 'Score' string column for compatibility if needed
                record.Score = record.TotalPoints.ToString();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log or handle error (non-critical for submission success)
                Console.WriteLine($"Leaderboard update failed: {ex.Message}");
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
        public string? Code { get; set; }
        public string? Language { get; set; }
        public string? Input { get; set; }
    }

    // Request model for Piston code execution
    public class PistonCodeRequest
    {
        public string? Code { get; set; }
        public string? Language { get; set; }
        public string? Version { get; set; }
        public string? Input { get; set; }
        public string[]? Args { get; set; }
    }

    // Request model for batch test execution
    public class RunTestsRequest
    {
        public string? Code { get; set; }
        public string? Language { get; set; }
        public string? Version { get; set; }
        public List<TestCase>? Tests { get; set; }
    }

    // Test case model with flexible property mapping
    public class TestCase
    {
        public string? Input { get; set; }
        public string? Expected { get; set; }
        
        // Flexible aliases for common variations in the DB
        [JsonPropertyName("output")]
        public string? Output { get; set; }

        [JsonPropertyName("expectedOutput")]
        public string? ExpectedOutput { get; set; }

        // Also handle 'args' as an alias for Input if needed
        [JsonPropertyName("args")]
        public string? Args { get => Input; set => Input = value; }

        // Method to get expected value from any of the possible properties
        public string GetExpectedValue()
        {
            return Expected ?? ExpectedOutput ?? Output ?? string.Empty;
        }
    }

    // Request model for solution submission
    public class SubmitSolutionRequest
    {
        public int ProblemId { get; set; }
        public string? Code { get; set; }
        public string? Language { get; set; }
    }

    public class OneCompilerPayload
    {
        [JsonPropertyName("language")]
        public string? Language { get; set; }
        
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        
        [JsonPropertyName("stdin")]
        public string? Stdin { get; set; }
    }

    public class PistonExecutionResponse
    {
        [JsonPropertyName("language")]
        public string? Language { get; set; }
        [JsonPropertyName("version")]
        public string? Version { get; set; }
        [JsonPropertyName("output")]
        public string? Output { get; set; }
        [JsonPropertyName("stdout")]
        public string? Stdout { get; set; }
        [JsonPropertyName("stderr")]
        public string? Stderr { get; set; }
        [JsonPropertyName("exitCode")]
        public int? ExitCode { get; set; }
        [JsonPropertyName("compile")]
        public PistonCompileResult? Compile { get; set; }
    }

    public class PistonCompileResult
    {
        [JsonPropertyName("output")]
        public string? Output { get; set; }
        [JsonPropertyName("stdout")]
        public string? Stdout { get; set; }
        [JsonPropertyName("stderr")]
        public string? Stderr { get; set; }
        [JsonPropertyName("exitCode")]
        public int? ExitCode { get; set; }
    }

    public class TestCaseCheckResult
    {
        [JsonPropertyName("testCase")]
        public int TestCase { get; set; }
        [JsonPropertyName("passed")]
        public bool Passed { get; set; }
        [JsonPropertyName("expected")]
        public string? Expected { get; set; }
        [JsonPropertyName("actual")]
        public object? Actual { get; set; }
    }

    public class TestResultsResponse
    {
        [JsonPropertyName("results")]
        public object? Results { get; set; }
        [JsonPropertyName("raw")]
        public string? Raw { get; set; }
        [JsonPropertyName("output")]
        public string? Output { get; set; }
    }
}
