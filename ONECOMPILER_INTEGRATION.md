# OneCompiler API Integration - Problem Details Page

## Overview
This document describes the OneCompiler API integration added to the Problem Details page, enabling users to write, test, and execute code directly in the browser.

## Changes Made

### 1. **Program.cs** - Service Registration
Added HTTP client factory for making API calls to OneCompiler:
```csharp
builder.Services.AddHttpClient();
```

### 2. **ProblemesController.cs** - Backend API Handler

#### New Dependencies
- `IHttpClientFactory` - For HTTP requests to OneCompiler API

#### New Models
```csharp
public class CodeExecutionRequest
{
    public string Code { get; set; }
    public string Language { get; set; }
    public string Input { get; set; }
}
```

#### New Action Method
**`ExecuteCode`** - Handles code execution requests
- **Route:** `POST /Problemes/ExecuteCode`
- **Input:** JSON with code, language, and input
- **Output:** JSON response from OneCompiler API
- **Features:**
  - Language mapping to OneCompiler codes
  - Error handling and user feedback
  - Supports 13+ programming languages

**Supported Languages:**
- JavaScript
- Python 3
- Java
- C++
- C#
- Go
- Rust
- PHP
- Ruby
- TypeScript

### 3. **Views/Problemes/Details.cshtml** - Enhanced UI

#### Layout Structure
```
???????????????????????????????????????????????????????????
?                  Problem Details Page                   ?
???????????????????????????????????????????????????????????
?  Problem Information     ?   Code Editor               ?
?  - Title                 ?  ???????????????????????    ?
?  - Description           ?  ?  Code Area          ?    ?
?  - Difficulty            ?  ?  (Editable)         ?    ?
?  - (Scrollable)          ?  ???????????????????????    ?
?                          ?  ? Input | Output      ?    ?
?                          ?  ? ????????             ?    ?
?                          ?  ? ? I/O  ?             ?    ?
?                          ?  ? ? Area ?             ?    ?
?                          ?  ? ????????             ?    ?
?                          ?  ???????????????????????    ?
?                          ?  ? Run | Clear |Submit ?    ?
?                          ?  ???????????????????????    ?
???????????????????????????????????????????????????????????
```

#### Components

**1. Language Selector**
- Dropdown menu with 10+ programming languages
- Located in card header
- Changes which language is sent to API

**2. Code Editor**
- Multi-line textarea with monospace font
- Dark theme (#1e1e1e background)
- Tab key support for indentation
- Syntax highlighting ready (extendable)

**3. Input/Output Tabs**
- **Input Tab:** For test case inputs
- **Output Tab:** For code execution results
- Tab switching for better UX

**4. Action Buttons**
- **Run Code:** Executes code via OneCompiler API
  - Disabled during execution
  - Shows spinner while running
  - Automatically switches to Output tab
- **Clear:** Clears all editors (with confirmation)
- **Submit:** Placeholder for submission logic

#### Styling
- Dark theme matching VS Code
- Code editor: #1e1e1e background, #d4d4d4 text
- Responsive layout on medium/large screens
- Professional borders and spacing

### Features Implemented

#### ? Code Execution
- Real-time code execution via OneCompiler API
- Support for 13+ programming languages
- Automatic language detection
- Handles stdout and stderr output

#### ? Input/Output Management
- Separate input and output areas
- Tab-based UI for organized viewing
- Pre-formatted output display
- Error message formatting

#### ? User Experience
- Loading spinner during execution
- Button state management
- Auto-focus on code editor
- Tab character support
- Keyboard shortcuts ready

#### ? Error Handling
- API error handling
- Network error catching
- Validation messages
- User-friendly error display

#### ? Responsive Design
- Mobile-friendly layout
- Adjusts to screen size
- Touch-friendly buttons

## How It Works

### Request Flow
```
1. User writes code in editor
2. User clicks "Run Code"
3. JavaScript collects code, language, input
4. POST request sent to ExecuteCode action
   ?
5. Controller maps language to OneCompiler code
6. HTTP request sent to OneCompiler API
7. OneCompiler executes code
8. Response returned to controller
   ?
9. JSON response sent back to frontend
10. JavaScript parses and displays output
11. Output tab automatically activated
12. Loading spinner hidden, button re-enabled
```

### OneCompiler API Details
- **Endpoint:** `https://api.onecompiler.com/api/v1/code/exec`
- **Method:** POST
- **Request Format:**
  ```json
  {
    "language": "python3",
    "code": "print('Hello')",
    "stdin": ""
  }
  ```
- **Response Format:**
  ```json
  {
    "stdout": "output text",
    "stderr": "error text"
  }
  ```

## Language Mapping

| User Selection | API Code |
|---|---|
| JavaScript | javascript |
| Python | python3 |
| Java | java |
| C++ | cpp |
| C# | csharp |
| Go | go |
| Rust | rust |
| PHP | php |
| Ruby | ruby |
| TypeScript | typescript |

## Security Considerations

?? **Important Notes:**
1. OneCompiler API is public - no authentication required
2. Code is sent to external service - inform users
3. No code persistence on OneCompiler servers
4. Consider adding server-side execution limits
5. Validate input size to prevent DoS
6. Add rate limiting if needed

## Keyboard Shortcuts
- **Tab:** Insert tab character in code editor
- Can be extended with more shortcuts (Ctrl+S for save, etc.)

## Future Enhancements

### Potential Improvements:
1. **Syntax Highlighting** - Add CodeMirror or Monaco Editor
2. **Code Formatting** - Prettier/Black integration
3. **Test Cases** - Multiple test case execution
4. **Submission Tracking** - Save submissions to database
5. **Code Themes** - User theme preferences (light/dark)
6. **Collaborative Coding** - Real-time collaboration
7. **Performance Metrics** - Execution time and memory
8. **Debugging** - Step-through debugging
9. **Auto-Complete** - IntelliSense features
10. **Version History** - Code revision tracking
11. **Local Execution** - Option for local/cloud execution toggle
12. **Rate Limiting** - API call throttling

## Testing

### Manual Testing Steps:
1. Navigate to any problem details page
2. Select a programming language from dropdown
3. Enter code in the editor (e.g., `print("Hello World")`)
4. Click "Run Code"
5. Verify output appears in Output tab
6. Test with invalid code to verify error handling
7. Test "Clear" button
8. Test with different languages

### Test Cases:
```python
# Python: Simple print
print("Hello World")

# JavaScript: Simple console.log
console.log("Hello World")

# Java: Simple main method
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello World");
    }
}

# C++: Simple cout
#include <iostream>
using namespace std;
int main() {
    cout << "Hello World" << endl;
    return 0;
}
```

## Troubleshooting

### Issue: Output not appearing
- **Check:** Network tab in browser DevTools
- **Solution:** Verify OneCompiler API is accessible
- **Alternative:** Check firewall/proxy settings

### Issue: Language not working
- **Check:** Selected language is in supported list
- **Solution:** Verify language mapping in controller
- **Alternative:** Test with different language

### Issue: Code takes too long
- **Cause:** OneCompiler API timeout or slow execution
- **Solution:** Check code for infinite loops
- **Alternative:** Use simpler test cases

## API Limitations

?? **OneCompiler API Limits:**
- Execution timeout: ~5-10 seconds
- No persistent storage
- Rate limiting: Check OneCompiler docs
- File I/O: May have restrictions
- Memory limits: Varies by language

## Database Integration

To implement submission tracking, add to `SoumissionsController`:

```csharp
[HttpPost]
public async Task<IActionResult> SaveSubmission(int problemId, string code, string language)
{
    var submission = new Soumission
    {
        ProbId = problemId,
        UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
        Code = code,
        Language = language,
        SubmissionDate = DateTime.Now,
        Status = "Submitted"
    };

    _context.Soumissions.Add(submission);
    await _context.SaveChangesAsync();
    return Ok(new { id = submission.SoumissionId });
}
```

## References

- [OneCompiler API Documentation](https://onecompiler.com/apis/code-execution)
- [OneCompiler Languages](https://onecompiler.com/languages)
- [Bootstrap Tabs Documentation](https://getbootstrap.com/docs/5.0/components/navs-tabs/)
- [Fetch API](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API)

## Files Modified

1. ? `Program.cs` - Added HttpClientFactory
2. ? `Controllers/ProblemesController.cs` - Added ExecuteCode action
3. ? `Views/Problemes/Details.cshtml` - Complete redesign with code editor

## Build Status
? Solution builds successfully with no errors or warnings
