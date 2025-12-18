# Piston API Integration Complete

## Summary
Successfully integrated the Piston API (https://piston.readthedocs.io/en/latest/api-v2/) into your LeetCode clone platform. Piston is a high-performance code execution engine that supports 50+ programming languages.

## What Was Added

### 1. **PistonCodeExecutionService** (`Services/PistonCodeExecutionService.cs`)
A comprehensive service for interacting with the Piston API v2:

**Features:**
- `GetRuntimesAsync()` - Fetches list of available programming languages and versions
- `ExecuteCodeAsync()` - Executes code with full Piston API support
- Proper error handling for network issues, timeouts, and API errors
- Support for all Piston API parameters:
  - Language and version selection
  - Multiple files
  - Standard input (stdin)
  - Command-line arguments
  - Compile and run timeouts
  - Memory limits

**Models:**
- `PistonExecutionRequest` - Request model with all parameters
- `PistonExecutionResult` - Response model with run and compile results
- `PistonStageResult` - Stage-specific results (stdout, stderr, exit codes)
- `PistonRuntime` - Runtime information (language, version, aliases)

### 2. **Controller Endpoints** (`Controllers/ProblemesController.cs`)

#### GET `/Problemes/GetPistonRuntimes`
Returns list of all available programming languages and versions.

**Example Response:**
```json
[
  {
    "language": "python",
    "version": "3.10.0",
    "aliases": ["py", "python3"]
  },
  {
    "language": "javascript",
    "version": "18.15.0",
    "aliases": ["js", "node-javascript"],
    "runtime": "node"
  }
]
```

#### POST `/Problemes/ExecuteCodeWithPiston`
Executes code using the Piston API.

**Request Body:**
```json
{
  "code": "print('Hello, World!')",
  "language": "python",
  "version": "3.10.0",  // optional
  "input": "test input",  // optional stdin
  "args": ["arg1", "arg2"]  // optional command-line args
}
```

**Response:**
```json
{
  "language": "python",
  "version": "3.10.0",
  "output": "Hello, World!\n",
  "stdout": "Hello, World!\n",
  "stderr": "",
  "exitCode": 0,
  "compile": null  // only present for compiled languages
}
```

### 3. **Language Mapping**
Automatic mapping of common language names to Piston identifiers:
- `javascript`, `js` → `javascript`
- `python`, `python3`, `py` → `python`
- `java` → `java`
- `cpp`, `c++` → `c++`
- `csharp`, `c#`, `cs` → `csharp`
- `go`, `rust`, `php`, `ruby`, `typescript`, `ts` → respective languages

## How to Use

### From JavaScript (Frontend)

#### Get Available Languages:
```javascript
fetch('/Problemes/GetPistonRuntimes')
  .then(response => response.json())
  .then(runtimes => {
    console.log('Available languages:', runtimes);
  });
```

#### Execute Code:
```javascript
const executeCode = async () => {
  const response = await fetch('/Problemes/ExecuteCodeWithPiston', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      code: 'console.log("Hello from Piston!");',
      language: 'javascript',
      input: ''
    })
  });
  
  const result = await response.json();
  console.log('Output:', result.output);
  console.log('Exit code:', result.exitCode);
};
```

### From C# (Backend)
```csharp
// Inject the service
private readonly PistonCodeExecutionService _pistonService;

// Get runtimes
var runtimes = await _pistonService.GetRuntimesAsync();

// Execute code
var request = new PistonExecutionRequest
{
    Language = "python",
    Code = "print('Hello')",
    Stdin = "input data"
};
var result = await _pistonService.ExecuteCodeAsync(request);
Console.WriteLine(result.Run.Output);
```

## Advantages Over OneCompiler

1. **More Languages** - 50+ languages vs ~15
2. **Open Source** - Free and self-hostable
3. **Better Control** - Timeout and memory limit configuration
4. **Compile Stage Info** - Separate compile and run results for compiled languages
5. **Version Selection** - Choose specific language versions
6. **Command-line Args** - Pass arguments to programs
7. **No Rate Limits** - Public instance or self-host

## Supported Languages (Examples)
- **Web**: JavaScript, TypeScript, PHP
- **Systems**: C, C++, Rust, Go
- **Scripting**: Python, Ruby, Perl, Bash
- **JVM**: Java, Kotlin, Scala
- **Functional**: Haskell, OCaml, F#
- **Others**: C#, Swift, R, Julia, and many more

## Next Steps

1. **Update Frontend** - Modify your code editor UI to use the new Piston endpoints
2. **Language Selector** - Add a dropdown populated from `/GetPistonRuntimes`
3. **Version Selector** - Allow users to choose language versions
4. **Test Cases** - Use Piston to run automated test cases for problems
5. **Submissions** - Store submission results with language and version info

## Testing

Test the endpoints using curl or Postman:

```bash
# Get runtimes
curl https://localhost:7178/Problemes/GetPistonRuntimes

# Execute Python code
curl -X POST https://localhost:7178/Problemes/ExecuteCodeWithPiston \
  -H "Content-Type: application/json" \
  -d '{
    "code": "print(\"Hello, Piston!\")",
    "language": "python"
  }'
```

## Files Modified/Created

1. ✅ **Created**: `Services/PistonCodeExecutionService.cs`
2. ✅ **Modified**: `Program.cs` - Registered Piston service
3. ✅ **Modified**: `Controllers/ProblemesController.cs` - Added endpoints and models

## Notes

- The existing OneCompiler integration remains intact
- Both APIs can be used side-by-side
- Piston API URL: `https://emkc.org/api/v2/piston`
- Default timeouts: 10s compile, 3s run
- You can self-host Piston for better performance and privacy

---

**Status**: ✅ Integration Complete - Ready to use!
