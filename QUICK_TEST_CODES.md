# Quick Copy-Paste Test Codes for OneCompiler

## ?? EASIEST - Start Here!

### Test 1: Python - Hello World
```
Language: Python
Code:
print("Hello World!")
```
**Expected Output:** `Hello World!`

---

### Test 2: Python - Simple Math
```
Language: Python
Code:
x = 5
y = 3
print(x + y)
```
**Expected Output:** `8`

---

## ?? How to Use This Guide

1. **Open the test page:**
   - Go to: `http://localhost:5000/onecompiler-test.html`
   - OR `https://localhost:7295/onecompiler-test.html`

2. **Copy the code from below**

3. **Paste into the Code textarea** (the big text area on the left)

4. **Make sure language matches** (select from dropdown)

5. **Click "Run Code (Direct API)"**

6. **Check Output** on the right side

---

## ? WORKING TEST CODES

### Python Tests

#### Python Test 1 - Print Statement
**Select Language:** Python
```python
print("Hello from Python!")
```

#### Python Test 2 - Variables and Math
**Select Language:** Python
```python
a = 10
b = 20
sum_result = a + b
print("Sum:", sum_result)
```

#### Python Test 3 - Loop
**Select Language:** Python
```python
for i in range(5):
    print(i)
```

#### Python Test 4 - List
**Select Language:** Python
```python
numbers = [1, 2, 3, 4, 5]
total = sum(numbers)
print("Total:", total)
```

#### Python Test 5 - Function
**Select Language:** Python
```python
def greet(name):
    return f"Hello, {name}!"

result = greet("Alice")
print(result)
```

---

### JavaScript Tests

#### JavaScript Test 1 - Console Log
**Select Language:** JavaScript
```javascript
console.log("Hello from JavaScript!");
```

#### JavaScript Test 2 - Variables
**Select Language:** JavaScript
```javascript
let x = 10;
let y = 20;
console.log("Sum: " + (x + y));
```

#### JavaScript Test 3 - Loop
**Select Language:** JavaScript
```javascript
for (let i = 0; i < 5; i++) {
    console.log(i);
}
```

#### JavaScript Test 4 - Array
**Select Language:** JavaScript
```javascript
let arr = [1, 2, 3, 4, 5];
let sum = arr.reduce((a, b) => a + b, 0);
console.log("Sum: " + sum);
```

---

### Java Tests

#### Java Test 1 - Hello World
**Select Language:** Java
```java
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello from Java!");
    }
}
```

#### Java Test 2 - Variables
**Select Language:** Java
```java
public class Main {
    public static void main(String[] args) {
        int a = 5;
        int b = 3;
        System.out.println("Sum: " + (a + b));
    }
}
```

#### Java Test 3 - Loop
**Select Language:** Java
```java
public class Main {
    public static void main(String[] args) {
        for (int i = 0; i < 5; i++) {
            System.out.println(i);
        }
    }
}
```

---

### C++ Tests

#### C++ Test 1 - Hello World
**Select Language:** C++
```cpp
#include <iostream>
using namespace std;

int main() {
    cout << "Hello from C++!" << endl;
    return 0;
}
```

#### C++ Test 2 - Variables
**Select Language:** C++
```cpp
#include <iostream>
using namespace std;

int main() {
    int x = 10;
    int y = 5;
    cout << "Sum: " << (x + y) << endl;
    return 0;
}
```

#### C++ Test 3 - Loop
**Select Language:** C++
```cpp
#include <iostream>
using namespace std;

int main() {
    for (int i = 0; i < 5; i++) {
        cout << i << endl;
    }
    return 0;
}
```

---

### C# Tests

#### C# Test 1 - Hello World
**Select Language:** C#
```csharp
using System;

class Program {
    static void Main() {
        Console.WriteLine("Hello from C#!");
    }
}
```

#### C# Test 2 - Variables
**Select Language:** C#
```csharp
using System;

class Program {
    static void Main() {
        int a = 8;
        int b = 2;
        Console.WriteLine("Sum: " + (a + b));
    }
}
```

---

### Go Tests

#### Go Test 1 - Hello World
**Select Language:** Go
```go
package main

import "fmt"

func main() {
    fmt.Println("Hello from Go!")
}
```

#### Go Test 2 - Variables
**Select Language:** Go
```go
package main

import "fmt"

func main() {
    x := 7
    y := 3
    fmt.Println("Sum:", x+y)
}
```

---

## ?? STEP-BY-STEP TEST PROCESS

### Step 1: Open Test Page
- Navigate to: `http://localhost:5000/onecompiler-test.html`
- You should see the compiler interface

### Step 2: Select Language
- Click the "Select Language" dropdown
- Choose the language for your test code

### Step 3: Enter Code
- Clear the current code (or just select all and delete)
- Copy a test code from above
- Paste it into the Code textarea

### Step 4: Run
- Click the green **"Run Code (Direct API)"** button
- Wait 2-3 seconds for execution

### Step 5: Check Result
- Look at the **Output** section
- Should show the expected output
- Check Debug Info for any errors

---

## ?? TROUBLESHOOTING

### Issue: Nothing happens when I click "Run Code"
**Solution:**
1. Open Browser DevTools: Press `F12`
2. Click **Console** tab
3. Look for any red error messages
4. Take a screenshot and share the error

### Issue: Error "Code editor is empty!"
**Solution:**
1. Make sure you pasted the code in the right textarea (the big one on the left labeled "Code")
2. The code should appear in the textarea
3. Try again

### Issue: Output shows error about language
**Solution:**
1. Make sure the selected language matches the code type
2. Python code ? Select "Python"
3. JavaScript code ? Select "JavaScript"
4. Java code ? Select "Java"

### Issue: API Error or timeout
**Solution:**
1. Check internet connection (OneCompiler API needs internet)
2. Try a simpler code first (like `print("test")`)
3. Wait a few seconds and try again
4. OneCompiler API might be temporarily down

---

## ? QUICK TEST CHECKLIST

Use this to verify everything works:

- [ ] Page loads (you see the interface)
- [ ] Select language dropdown works
- [ ] Can type in Code textarea
- [ ] "Run Code" button is clickable
- [ ] Python "Hello World" test runs
- [ ] Output appears in Output section
- [ ] Debug Info shows execution logs
- [ ] Try JavaScript test
- [ ] Try Java test

---

## ?? PRO TIPS

### Tip 1: Use Quick Test Buttons
- The bottom has 7 quick test buttons
- Click any of them to auto-load a test
- Then just click "Run Code"

### Tip 2: Check Debug Info
- Bottom section shows execution logs
- Helps debug what went wrong
- Shows response status and timing

### Tip 3: Input/Output Tab
- The **Input** textarea is optional
- Only needed if code uses `input()` in Python
- Leave blank if not needed

### Tip 4: Keyboard Shortcuts
- Ctrl+A - Select all code
- Ctrl+C - Copy
- Ctrl+V - Paste
- Tab - Won't work in textarea (will focus out)

---

## ?? VERIFICATION FLOW

```
Open Test Page
    ?
Click "Test 1: Python - Hello World"
    ?
Code loads automatically
    ?
Click "Run Code (Direct API)"
    ?
See "Hello World!" in Output
    ?
Success! ?
```

---

## ?? EXAMPLES BY DIFFICULTY

### ? EASIEST (Start here)
- Python - Print
- JavaScript - Console.log
- Print statements with no logic

### ?? EASY
- Simple math operations
- Variable assignments
- Basic loops

### ??? MEDIUM
- Functions
- Arrays/Lists
- Conditionals

### ???? HARD
- Complex algorithms
- Recursion
- Multiple functions

---

## ?? NEXT STEPS AFTER TESTING

Once you verify the test page works:

1. Go to any **Problem Details** page on the main site
2. You should see the same code editor
3. Try running code there too
4. The implementation should work the same way

---

## ?? IF NOTHING WORKS

Try this in order:

1. **Check Internet**: Can you access https://api.onecompiler.com?
2. **Try Direct**: Go to https://onecompiler.com and test their IDE
3. **Check Browser**: Use Chrome or Edge (not old IE)
4. **Clear Cache**: Press Ctrl+Shift+Delete and clear browser cache
5. **Restart**: Close and reopen the browser
6. **Check URL**: Make sure you're at the right localhost URL

---

## ?? HELPFUL REFERENCES

- OneCompiler Docs: https://onecompiler.com/apis/code-execution
- Supported Languages: https://onecompiler.com/languages
- Bootstrap Docs: https://getbootstrap.com/docs/5.0/

---

## ? COMMON QUESTIONS

**Q: Why is my output not showing?**
A: Check if the language matches your code. Also check the Debug Info section for errors.

**Q: Can I use file I/O?**
A: OneCompiler doesn't support file operations. Only stdin/stdout.

**Q: How long can code run?**
A: Usually 5-10 seconds max before timeout.

**Q: Is there a code size limit?**
A: Very large code might fail. Keep it reasonable (< 10KB usually).

**Q: Can I import libraries?**
A: Most standard libraries work. External packages usually don't.

---

## Version Info
- Test Page: OneCompiler API Direct Test v1.0
- Bootstrap: v5.3.0
- .NET: 9.0
- Date: 2024

