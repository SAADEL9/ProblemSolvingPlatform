using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProblemSolvingPlatform.Services
{
    public class PistonCodeExecutionService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string PistonApiUrl = "https://emkc.org/api/v2/piston";

        public PistonCodeExecutionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get list of available runtimes from Piston API
        /// </summary>
        public async Task<List<PistonRuntime>> GetRuntimesAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(10);

                var response = await client.GetAsync($"{PistonApiUrl}/runtimes");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var runtimes = JsonSerializer.Deserialize<List<PistonRuntime>>(content, new JsonSerializerOptions 
                    { 
                        PropertyNameCaseInsensitive = true 
                    });
                    return runtimes ?? new List<PistonRuntime>();
                }
                
                return new List<PistonRuntime>();
            }
            catch (Exception)
            {
                return new List<PistonRuntime>();
            }
        }

        /// <summary>
        /// Execute code using Piston API
        /// </summary>
        public async Task<PistonExecutionResult> ExecuteCodeAsync(PistonExecutionRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(30);

                var payload = new
                {
                    language = request.Language,
                    version = request.Version ?? "*",
                    files = new[]
                    {
                        new
                        {
                            name = request.FileName ?? "main",
                            content = request.Code
                        }
                    },
                    stdin = request.Stdin ?? "",
                    args = request.Args ?? Array.Empty<string>(),
                    compile_timeout = request.CompileTimeout ?? 10000,
                    run_timeout = request.RunTimeout ?? 3000,
                    compile_memory_limit = request.CompileMemoryLimit ?? -1,
                    run_memory_limit = request.RunMemoryLimit ?? -1
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync($"{PistonApiUrl}/execute", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<PistonExecutionResult>(resultContent, new JsonSerializerOptions 
                    { 
                        PropertyNameCaseInsensitive = true 
                    });
                    return result ?? new PistonExecutionResult 
                    { 
                        Run = new PistonStageResult { Output = "Error: Failed to parse response" } 
                    };
                }
                else
                {
                    return new PistonExecutionResult
                    {
                        Run = new PistonStageResult
                        {
                            Output = $"Error: API returned status code {response.StatusCode}",
                            Stderr = await response.Content.ReadAsStringAsync()
                        }
                    };
                }
            }
            catch (HttpRequestException hre)
            {
                return new PistonExecutionResult
                {
                    Run = new PistonStageResult
                    {
                        Output = "Network error: Cannot connect to Piston API",
                        Stderr = hre.Message
                    }
                };
            }
            catch (TaskCanceledException)
            {
                return new PistonExecutionResult
                {
                    Run = new PistonStageResult
                    {
                        Output = "Error: Request timeout",
                        Stderr = "Code execution took too long"
                    }
                };
            }
            catch (Exception ex)
            {
                return new PistonExecutionResult
                {
                    Run = new PistonStageResult
                    {
                        Output = $"Error: {ex.Message}",
                        Stderr = ex.ToString()
                    }
                };
            }
        }
    }

    // Request and Response Models
    public class PistonExecutionRequest
    {
        public string Language { get; set; } = string.Empty;
        public string? Version { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? FileName { get; set; }
        public string? Stdin { get; set; }
        public string[]? Args { get; set; }
        public int? CompileTimeout { get; set; }
        public int? RunTimeout { get; set; }
        public int? CompileMemoryLimit { get; set; }
        public int? RunMemoryLimit { get; set; }
    }

    public class PistonExecutionResult
    {
        public string? Language { get; set; }
        public string? Version { get; set; }
        public PistonStageResult Run { get; set; } = new PistonStageResult();
        public PistonStageResult? Compile { get; set; }
    }

    public class PistonStageResult
    {
        public string Stdout { get; set; } = string.Empty;
        public string Stderr { get; set; } = string.Empty;
        public string Output { get; set; } = string.Empty;
        public int? Code { get; set; }
        public string? Signal { get; set; }
    }

    public class PistonRuntime
    {
        public string Language { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public List<string> Aliases { get; set; } = new List<string>();
        public string? Runtime { get; set; }
    }
}
