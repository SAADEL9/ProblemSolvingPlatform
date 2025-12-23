# ?? QUICK REFERENCE - FUNCTION TEMPLATES & TEST CASES

## ?? QUICK START (5 MINUTES)

### Step 1: Run Database Migration
```sql
-- Execute: Scripts/AddFunctionTemplates.sql
```

### Step 2: Create Your First Problem

**1. Go to:** `/Problemes/Create`

**2. Fill Basic Info:**
- Title: `Two Sum`
- Description: `Find two numbers that add up to target`
- Difficulty: `Easy`
- Language: `Python`

**3. Add Function Template:**
```python
def twoSum(nums, target):
    # Your code here
    pass
```

**4. Add Test Cases:**
```json
[
  {"input": "[2, 7, 11, 15], 9", "expected": "[0, 1]"},
  {"input": "[3, 2, 4], 6", "expected": "[1, 2]"}
]
```

**5. Click:** `Create`

### Step 3: Test It
- Click `Solve` on the problem
- See template on left
- Write solution in editor
- Click `Run Tests`
- See green checkmarks for passed tests!

---

## ?? FUNCTION TEMPLATE EXAMPLES

### Python
```python
def function_name(param1, param2):
    """Docstring"""
    # Your code here
    pass
```

### JavaScript
```javascript
function functionName(param1, param2) {
    // Your code here
}
```

### Java
```java
class Solution {
    public Type method(Type param) {
        // Your code here
        return null;
    }
}
```

### C++
```cpp
class Solution {
public:
    Type functionName(Type param) {
        // Your code here
    }
};
```

---

## ?? TEST CASES FORMAT

### Basic Format
```json
[
  {
    "input": "test_input_1",
    "expected": "expected_output_1"
  },
  {
    "input": "test_input_2",
    "expected": "expected_output_2"
  }
]
```

### For Arrays
```json
[
  {
    "input": "[1, 2, 3], 5",
    "expected": "True"
  }
]
```

### For Complex Data
```json
[
  {
    "input": "{'key': 'value'}, 10",
    "expected": "result"
  }
]
```

---

## ?? COMPILER FEATURES AT A GLANCE

| Button | Action |
|--------|--------|
| **Run Code** | Execute with custom input |
| **Run Tests** | Execute all test cases |
| **Clear** | Clear all editors |
| **Submit** | Save solution (backend) |

| Tab | Shows |
|-----|-------|
| **Code** | Your solution |
| **Input** | Test input |
| **Output** | Execution output |
| **Tests** | Test results |

---

## ? TEST RESULT COLORS

```
?? GREEN - Test Passed
?? RED   - Test Failed
?? BLUE  - Summary (X Passed, Y Failed)
```

---

## ?? WHAT'S NEW

### Database
- `FunctionTemplate` - Function structure
- `TestCases` - JSON test cases
- `Language` - Primary language
- `CreatedAt` - Creation date

### Views
- Function templates visible to users
- Modern dark compiler UI
- Test runner tab
- Color-coded results

### Features
- Automated test validation
- Real-time test results
- Professional styling
- Better user experience

---

## ?? TROUBLESHOOTING

### Test Cases Not Running
- **Check:** JSON format is valid
- **Check:** Input matches expected format
- **Check:** Expected output is exact

### Function Template Not Showing
- **Check:** FunctionTemplate field is filled
- **Check:** Problem details page is refreshed

### Tests All Fail
- **Check:** Your solution logic
- **Check:** Output format matches expected
- **Check:** No extra spaces/newlines

---

## ?? FILES INVOLVED

| File | Purpose |
|------|---------|
| `Models/Probleme.cs` | Database model |
| `Views/Problemes/Create.cshtml` | Admin creation |
| `Views/Problemes/Edit.cshtml` | Admin editing |
| `Views/Problemes/Details.cshtml` | User solving |
| `Controllers/ProblemesController.cs` | Backend logic |
| `Scripts/AddFunctionTemplates.sql` | Database setup |

---

## ?? EXAMPLE: REVERSE STRING

**Create Problem:**
```
Title: Reverse String
Difficulty: Easy
Language: Python

Function Template:
def reverseString(s):
    pass

Test Cases:
[
  {"input": "hello", "expected": "olleh"},
  {"input": "world", "expected": "dlrow"}
]
```

**User Solves:**
```python
def reverseString(s):
    return s[::-1]
```

**Test Results:**
```
? 2 Passed  ? 0 Failed  ?? 2 Total
? Test 1: PASSED
? Test 2: PASSED
```

---

## ?? READY TO GO!

1. ? Database updated
2. ? New fields added
3. ? UI redesigned
4. ? Test runner ready
5. ? Everything compiled

**Start creating problems now!** ??
