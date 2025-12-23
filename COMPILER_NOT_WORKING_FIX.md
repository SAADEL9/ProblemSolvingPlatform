# ?? COMPILER FIX - TROUBLESHOOTING GUIDE

## ? Problem: Compiler Not Working

If your compiler isn't functioning, follow these steps:

---

## ? STEP 1: Check Browser Console

1. Open Developer Tools (**F12**)
2. Go to **Console** tab
3. Try running code again
4. Look for error messages

Common errors:
- `404 Not Found` - Wrong endpoint
- `Network Error` - Connection issue
- `JSON Parse Error` - Invalid response format

---

## ? STEP 2: Verify Endpoint

The compiler calls this endpoint:
```
POST /Problemes/ExecuteCodeWithPiston
```

Make sure:
1. Controller has this action
2. Action is **public async Task<IActionResult>**
3. Returns proper JSON

Check in `Controllers/ProblemesController.cs`:
```csharp
[HttpPost]
public async Task<IActionResult> ExecuteCodeWithPiston([FromBody] PistonCodeRequest request)
{
    // Should exist and be executable
}
```

---

## ? STEP 3: Check Piston Service

Verify service is registered in **Program.cs**:
```csharp
// Add Piston Code Execution Service
builder.Services.AddScoped<PistonCodeExecutionService>();
```

If missing, add it after other services.

---

## ? STEP 4: Check Internet Connection

The compiler needs internet to reach Piston API:
```
https://emkc.org/api/v2/piston/execute
```

Test:
1. Open browser
2. Visit: `https://emkc.org/api/v2/piston/runtimes`
3. Should see list of languages in JSON

If fails: **Network/Firewall issue**

---

## ? STEP 5: Test with Simple Code

Try this Python code:
```python
print("Hello World")
```

If this works:
- Compiler is functional ?
- Problem might be with complex code

---

## ? STEP 6: Check Controller Response

Add temporary logging in `ExecuteCodeWithPiston`:
```csharp
public async Task<IActionResult> ExecuteCodeWithPiston([FromBody] PistonCodeRequest request)
{
    try
    {
        Console.WriteLine($"Language: {request.Language}");
        Console.WriteLine($"Code length: {request.Code?.Length}");
        
        // ... rest of code
    }
    catch (Exception ex)
    {
        return BadRequest(new { error = ex.Message, stack = ex.StackTrace });
    }
}
```

---

## ?? COMMON FIXES

### Fix 1: Service Not Registered
**Add to Program.cs:**
```csharp
builder.Services.AddScoped<PistonCodeExecutionService>();
```

### Fix 2: Wrong Language Code
Check `languageMap` in controller. Map value must match Piston:
```csharp
var languageMap = new Dictionary<string, string>
{
    { "python", "python" },      // NOT "python3"
    { "javascript", "javascript" },
    { "java", "java" },
    // ... etc
};
```

### Fix 3: Timeout
Increase timeout in Piston service:
```csharp
CompileTimeout = 15000,  // Increase from 10000
RunTimeout = 5000        // Increase from 3000
```

### Fix 4: CORS Issue
If cross-origin error, check if Piston API allows requests from your domain.

---

## ?? TEST THE COMPILER

### Method 1: Browser Console
```javascript
// Open Console (F12) and run:
fetch('/Problemes/ExecuteCodeWithPiston', {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify({
        code: 'print("test")',
        language: 'python',
        input: ''
    })
})
.then(r => r.json())
.then(d => console.log(d))
.catch(e => console.error(e));
```

### Method 2: Postman/curl
```bash
curl -X POST https://localhost:7000/Problemes/ExecuteCodeWithPiston \
  -H "Content-Type: application/json" \
  -d '{
    "code": "print(\"test\")",
    "language": "python",
    "input": ""
  }'
```

---

## ?? DEBUG CHECKLIST

- [ ] Browser console shows no errors
- [ ] F12 shows 200 status code (not 404/500)
- [ ] Response contains `stdout`, `stderr`, or `output`
- [ ] Simple "Hello World" code works
- [ ] Internet connection works (ping Piston API)
- [ ] Controller endpoint exists and is public
- [ ] PistonCodeExecutionService registered in Program.cs
- [ ] Language map includes your language

---

## ?? DETAILED DEBUGGING

### Check 1: Endpoint Routing
In `ProblemesController.cs`:
```csharp
[HttpPost]
public async Task<IActionResult> ExecuteCodeWithPiston([FromBody] PistonCodeRequest request)
{
    // MUST have [HttpPost] attribute
    // MUST have [FromBody] on parameter
}
```

### Check 2: Request Format
JavaScript must send:
```json
{
    "code": "print('hello')",
    "language": "python",
    "input": ""
}
```

Controller must accept `PistonCodeRequest` with these properties.

### Check 3: Response Format
Controller returns:
```json
{
    "language": "python",
    "version": "3.10.x",
    "output": "hello",
    "stdout": "hello",
    "stderr": "",
    "exitCode": 0,
    "compile": null
}
```

JavaScript expects at least `stdout` or `output`.

---

## ?? IF STILL NOT WORKING

### Option 1: Restart Application
```
Ctrl+Shift+F5  (Stop debugger)
Wait 5 seconds
F5              (Start debugger)
```

### Option 2: Clear Cache
```
Ctrl+Shift+Delete  (Browser)
Clear all cache
Reload page
```

### Option 3: Check Logs
Look at Debug Output in Visual Studio:
```
View ? Output ? Show output from: Debug
```

### Option 4: Hard Refresh
```
Ctrl+F5  (Hard refresh, clear cache)
```

---

## ?? TROUBLESHOOTING FLOW

```
Compiler not working
    ?
Check browser console (F12)
    ?? 404 error ? Check controller endpoint exists
    ?? Network error ? Check internet/Piston API
    ?? JSON error ? Check response format
    ?? No error ? Check if it's async/await issue
    
Try simple code (print("hello"))
    ?? Works ? Complex code issue
    ?   ?? Check code syntax
    ?? Doesn't work ? Compiler issue
        ?? Restart app
        ?? Restart browser
        ?? Check service registration
```

---

## ? WHAT I FIXED

1. **Better error handling** in fetch
2. **Improved JSON parsing** with fallback
3. **Better error messages** in output
4. **Better test result display** with formatting

---

## ?? NEXT STEPS

1. **Stop debugger** (Ctrl+Shift+F5)
2. **Restart app** (F5)
3. **Try running code** again
4. **Check browser console** for errors
5. **Follow the debug checklist above**

If still not working, let me know the exact error message and I'll help!

---

## ?? QUICK SUMMARY

| Check | Status |
|-------|--------|
| Controller exists | ? |
| Service registered | ? |
| Endpoint callable | ? |
| Piston API accessible | ? |
| Internet connection | ? |

Run the "Test with Simple Code" step to verify!

