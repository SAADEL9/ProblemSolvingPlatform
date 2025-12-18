# OneCompiler API - Test Code Examples

## Quick Test Codes to Copy & Paste

### 1. **Python - Simple Hello World** ? EASIEST
```python
print("Hello World!")
```
**Expected Output:**
```
Hello World!
```

---

### 2. **Python - Sum Calculator**
```python
a = 10
b = 20
result = a + b
print(f"Sum: {result}")
```
**Expected Output:**
```
Sum: 30
```

---

### 3. **Python - Loop Example**
```python
for i in range(5):
    print(f"Number {i}")
```
**Expected Output:**
```
Number 0
Number 1
Number 2
Number 3
Number 4
```

---

### 4. **Python - User Input Example**
```python
name = input("Enter your name: ")
print(f"Hello, {name}!")
```
**Test Input (paste in Input tab):**
```
Alice
```
**Expected Output:**
```
Enter your name: Hello, Alice!
```

---

### 5. **JavaScript - Simple Output**
```javascript
console.log("Hello from JavaScript!");
console.log("2 + 2 = " + (2 + 2));
```
**Expected Output:**
```
Hello from JavaScript!
2 + 2 = 4
```

---

### 6. **JavaScript - Array Operations**
```javascript
let arr = [1, 2, 3, 4, 5];
let sum = arr.reduce((a, b) => a + b, 0);
console.log("Array: " + arr);
console.log("Sum: " + sum);
```
**Expected Output:**
```
Array: 1,2,3,4,5
Sum: 15
```

---

### 7. **Java - Hello World**
```java
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello from Java!");
    }
}
```
**Expected Output:**
```
Hello from Java!
```

---

### 8. **Java - Simple Math**
```java
public class Main {
    public static void main(String[] args) {
        int x = 5;
        int y = 3;
        System.out.println("x + y = " + (x + y));
        System.out.println("x * y = " + (x * y));
    }
}
```
**Expected Output:**
```
x + y = 8
x * y = 15
```

---

### 9. **C++ - Hello World**
```cpp
#include <iostream>
using namespace std;

int main() {
    cout << "Hello from C++!" << endl;
    return 0;
}
```
**Expected Output:**
```
Hello from C++!
```

---

### 10. **C++ - Simple Calculation**
```cpp
#include <iostream>
using namespace std;

int main() {
    int a = 7;
    int b = 3;
    cout << "a + b = " << (a + b) << endl;
    cout << "a * b = " << (a * b) << endl;
    return 0;
}
```
**Expected Output:**
```
a + b = 10
a * b = 21
```

---

### 11. **C# - Hello World**
```csharp
using System;

class Program {
    static void Main() {
        Console.WriteLine("Hello from C#!");
    }
}
```
**Expected Output:**
```
Hello from C#!
```

---

### 12. **Go - Hello World**
```go
package main

import "fmt"

func main() {
    fmt.Println("Hello from Go!")
}
```
**Expected Output:**
```
Hello from Go!
```

---

### 13. **Python - Problem Solving Example**
```python
# Find the sum of first n numbers
def sum_of_n_numbers(n):
    return n * (n + 1) // 2

result = sum_of_n_numbers(10)
print(f"Sum of first 10 numbers: {result}")
```
**Expected Output:**
```
Sum of first 10 numbers: 55
```

---

### 14. **Python - Fibonacci Series**
```python
def fibonacci(n):
    a, b = 0, 1
    for _ in range(n):
        print(a, end=" ")
        a, b = b, a + b
    print()

fibonacci(10)
```
**Expected Output:**
```
0 1 1 2 3 5 8 13 21 34 
```

---

### 15. **Python - With Input**
```python
# Read a number and print its table
n = int(input("Enter a number: "))
print(f"Table of {n}:")
for i in range(1, 6):
    print(f"{n} x {i} = {n * i}")
```
**Test Input:**
```
5
```
**Expected Output:**
```
Enter a number: Table of 5:
5 x 1 = 5
5 x 2 = 10
5 x 3 = 15
5 x 4 = 20
5 x 5 = 25
```

---

## How to Test

### Step 1: Navigate to Problem Details
1. Go to any problem details page
2. You should see the code editor on the right side

### Step 2: Clear the Editor
1. Click the **Clear** button to remove placeholder code
2. Or just select all and delete

### Step 3: Copy Test Code
1. Copy one of the test codes above
2. Paste it into the code editor

### Step 4: Select Language
1. Make sure the language dropdown matches your code:
   - Python ? "Python"
   - JavaScript ? "JavaScript"
   - Java ? "Java"
   - C++ ? "C++"
   - C# ? "C#"
   - Go ? "Go"

### Step 5: Run the Code
1. Click **Run Code** button
2. Wait for execution (should be fast)
3. Output will appear in the Output tab

### Step 6: Test with Input (Optional)
1. If code has `input()`, click the **Input** tab
2. Enter the test input
3. Click **Run Code** again

---

## Common Issues & Solutions

### Issue 1: "Output will appear here..."
**Problem:** Nothing happens when clicking Run
**Solution:** 
- Check browser console (F12 ? Console tab)
- Verify Internet connection (OneCompiler API needs internet)
- Check if code has syntax errors

### Issue 2: "Error: Failed to execute code"
**Problem:** Backend returns error
**Solution:**
- Start with the simplest test: `print("Hello World!")`
- Check Python is selected if using Python code
- Look at server logs for more details

### Issue 3: "Code editor is empty!"
**Problem:** Won't let you run empty code
**Solution:**
- Make sure you've typed or pasted code
- Click in editor and paste code again

### Issue 4: Output tab shows error, not output
**Problem:** Compilation or runtime error
**Solution:**
- Check code syntax
- Start with simpler examples
- Verify language selection matches code

---

## Debugging Tips

### Using Browser Developer Tools
1. Press **F12** to open Developer Tools
2. Click **Console** tab
3. Click **Run Code**
4. Look for any JavaScript errors

### Using Network Tab
1. Press **F12**
2. Click **Network** tab
3. Click **Run Code**
4. Look for request to `/Problemes/ExecuteCode`
5. Check Response tab to see API response

### Check Server Logs
1. Look at Visual Studio Output window
2. Check Application Insights if configured
3. Check Event Viewer for ASP.NET errors

---

## Expected Behavior

### ? Success
- Click Run Code
- Spinner appears for 1-3 seconds
- Output tab automatically switches
- Code output appears in Output area
- Run button becomes enabled again

### ? Failure
- Click Run Code
- Spinner appears but doesn't disappear
- OR error message appears
- Check console for details

---

## Additional Test Codes for Problem Solving Practice

### LeetCode Style: Two Sum
```python
def twoSum(nums, target):
    for i in range(len(nums)):
        for j in range(i + 1, len(nums)):
            if nums[i] + nums[j] == target:
                return [i, j]
    return []

result = twoSum([2, 7, 11, 15], 9)
print(f"Indices: {result}")
```
**Expected Output:**
```
Indices: [0, 1]
```

---

### LeetCode Style: Reverse String
```python
def reverseString(s):
    return s[::-1]

text = "Hello"
print(f"Original: {text}")
print(f"Reversed: {reverseString(text)}")
```
**Expected Output:**
```
Original: Hello
Reversed: olleH
```

---

### LeetCode Style: Palindrome Check
```python
def isPalindrome(s):
    s = s.lower().replace(" ", "")
    return s == s[::-1]

test_cases = ["racecar", "hello", "a man a plan a canal panama"]
for test in test_cases:
    result = isPalindrome(test)
    print(f"'{test}' is palindrome: {result}")
```
**Expected Output:**
```
'racecar' is palindrome: True
'hello' is palindrome: False
'a man a plan a canal panama' is palindrome: True
```

---

## Quick Start Commands

### If nothing works, try this sequence:
1. **First test** - Run this Python code:
   ```python
   print("Test")
   ```

2. **If Test 1 fails** - Check:
   - Internet connection
   - Backend running (check URL shows app)
   - Browser console for errors (F12)

3. **If Test 1 works** - Try next test:
   ```python
   x = 5
   y = 10
   print(x + y)
   ```

4. **If both work** - Backend is fine, try any test above

---

## Contact OneCompiler Support
If API is down:
- Check: https://status.onecompiler.com
- Visit: https://onecompiler.com
- Try in their web IDE first before testing here

---

## Notes
- OneCompiler API is **rate limited** - don't spam requests
- Timeout is typically **5-10 seconds** per execution
- Very large outputs may be truncated
- File I/O may not work (no persistent storage)
