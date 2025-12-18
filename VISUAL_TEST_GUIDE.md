# ?? VISUAL STEP-BY-STEP TEST GUIDE

## ?? How to Test the OneCompiler Integration

### STEP 1?? - Open the Test Page

Go to one of these URLs in your browser:
- `http://localhost:5000/onecompiler-test.html` (HTTP)
- `https://localhost:7295/onecompiler-test.html` (HTTPS)

You should see a dark interface with:
- Left side: "Code" section with language dropdown
- Right side: "Input" and "Output" sections
- Bottom: "Run Code" button and test case buttons

---

### STEP 2?? - Select a Test

Option A - Quick Click:
1. Scroll down to "Quick Test Cases"
2. Click any button (e.g., "Test 1: Python - Hello World")
3. Code auto-loads in the Code area

Option B - Manual:
1. Make sure "Python" is selected in dropdown
2. Click in the Code textarea (big text box)
3. Select all existing text (Ctrl+A)
4. Delete it
5. Paste test code from below

---

### STEP 3?? - Simple Python Test

**Copy this code exactly:**

```
print("Hello World!")
```

**Steps:**
1. Click in Code textarea
2. Select all (Ctrl+A)
3. Paste the code (Ctrl+V)
4. Make sure "Python" is in the language dropdown
5. Click the green "Run Code (Direct API)" button

**Wait 2-3 seconds...**

**You should see in Output:**
```
Hello World!
```

---

### STEP 4?? - If It Works ?

Congratulations! Your OneCompiler integration is working!

Try other tests:
- [Test 2] Python - Math Operations
- [Test 3] Python - Loop
- [Test 4] JavaScript - Hello World
- [Test 5] Java - Hello World

---

### STEP 5?? - If It Doesn't Work ?

Do these checks:

#### Check A: Code Area Has Text?
- Look at the Code textarea (left side, big text box)
- Should show: `print("Hello World!")`
- If empty: You didn't paste the code

#### Check B: Language Is Right?
- Look at dropdown that says "Select Language"
- Should say: "Python"
- If wrong: Click dropdown and select Python

#### Check C: Spinner Appears?
- Click "Run Code (Direct API)"
- Look for spinning circle
- Should appear for 1-3 seconds

#### Check D: Output Has Error?
- Look at Output section (right side)
- If you see red text or "Error": Something is wrong
- Copy the error message

#### Check E: Browser Console?
- Press F12
- Click "Console" tab
- Look for red error messages
- Copy any errors you see

---

## ?? QUICK TEST CODES

### Test A - Simplest (Python)
```
print(1)
```
Expected: `1`

### Test B - Variables (Python)
```
x = 5
y = 10
print(x + y)
```
Expected: `15`

### Test C - Loop (Python)
```
for i in range(3):
    print(i)
```
Expected:
```
0
1
2
```

### Test D - JavaScript
```
console.log("Test");
```
Expected: `Test`

### Test E - Java
```
public class Main {
    public static void main(String[] args) {
        System.out.println("Test");
    }
}
```
Expected: `Test`

---

## ?? TEST WORKFLOW

```
???????????????????????????????????????????
? 1. Open Test Page                       ?
?    http://localhost:5000/...html        ?
???????????????????????????????????????????
             ?
             ?
???????????????????????????????????????????
? 2. Select Language (or use Quick Test)  ?
???????????????????????????????????????????
             ?
             ?
???????????????????????????????????????????
? 3. Copy & Paste Test Code               ?
???????????????????????????????????????????
             ?
             ?
???????????????????????????????????????????
? 4. Click "Run Code (Direct API)"        ?
???????????????????????????????????????????
             ?
             ?
???????????????????????????????????????????
? 5. Wait 2-3 seconds                     ?
???????????????????????????????????????????
             ?
             ?
???????????????????????????????????????????
? 6. Check Output Section                 ?
?    - Success: See your result           ?
?    - Failure: See error message         ?
???????????????????????????????????????????
```

---

## ?? COMPLETE PYTHON TEST SUITE

### Suite Test 1
```python
print("Hello World!")
```

### Suite Test 2
```python
x = 10
print(x)
```

### Suite Test 3
```python
x = 5
y = 3
z = x + y
print(z)
```

### Suite Test 4
```python
for i in range(5):
    print(i)
```

### Suite Test 5
```python
numbers = [1, 2, 3, 4, 5]
print(sum(numbers))
```

### Suite Test 6
```python
def add(a, b):
    return a + b
result = add(7, 3)
print(result)
```

### Suite Test 7
```python
name = "Alice"
age = 25
print(f"{name} is {age} years old")
```

---

## ?? COMPLETE JAVASCRIPT TEST SUITE

### Suite Test 1
```javascript
console.log("Hello World!");
```

### Suite Test 2
```javascript
let x = 10;
console.log(x);
```

### Suite Test 3
```javascript
let a = 5;
let b = 3;
console.log(a + b);
```

### Suite Test 4
```javascript
for (let i = 0; i < 5; i++) {
    console.log(i);
}
```

### Suite Test 5
```javascript
let arr = [1, 2, 3, 4, 5];
console.log(arr.length);
```

---

## ? COMPLETE JAVA TEST SUITE

### Suite Test 1
```java
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello World!");
    }
}
```

### Suite Test 2
```java
public class Main {
    public static void main(String[] args) {
        int x = 10;
        System.out.println(x);
    }
}
```

### Suite Test 3
```java
public class Main {
    public static void main(String[] args) {
        int a = 5;
        int b = 3;
        System.out.println(a + b);
    }
}
```

---

## ?? IF STILL STUCK

### Nuclear Option (Start Fresh)
1. Close browser completely
2. Stop the app (Stop in Visual Studio)
3. Wait 5 seconds
4. Start the app again (F5)
5. Wait for "Ready" message
6. Open new browser tab
7. Go to test page
8. Try simplest test: `print(1)`

### Check Network
1. Press F12
2. Go to Network tab
3. Click "Run Code"
4. Look for request to api.onecompiler.com
5. Check if response is 200
6. Look at Response tab

### Internet Connectivity
1. Can you access https://api.onecompiler.com directly?
2. Try from incognito window
3. Try from different browser (Chrome, Edge, Firefox)
4. Restart WiFi router

---

## ? AFTER TESTING WORKS

### Next Step 1: Test in Problem Page
1. Go to home page
2. Click on any problem
3. See code editor on right
4. Paste test code
5. Click Run Code
6. Should work same as test page

### Next Step 2: Test Different Languages
1. Try JavaScript test
2. Try Java test
3. Try C++ test
4. Make sure language dropdown matches

### Next Step 3: Test with Input
1. Some tests use input()
2. Type input in Input textarea
3. Run code
4. Check if it reads input correctly

---

## ?? ERROR MESSAGES EXPLAINED

### Error: "Code editor is empty!"
- **Meaning:** You didn't paste any code
- **Fix:** Copy code and paste it in Code textarea

### Error: "Executing..."
- **Meaning:** Code is running, just wait
- **Fix:** Wait 2-3 seconds, don't click again

### Error: "Failed to execute code"
- **Meaning:** Backend error or OneCompiler down
- **Fix:** Check internet, try simpler code, wait and retry

### Error: "Error: xhr is not defined" (in console)
- **Meaning:** Cross-origin issue
- **Fix:** Ignore, test page handles this

### Output: "Error: syntax error"
- **Meaning:** Your code has typos
- **Fix:** Check code syntax, use a simple test

---

## ?? SUCCESS CRITERIA

You know it's working when:

- [ ] Test page loads (you see the interface)
- [ ] Language dropdown works (you can select Python/JavaScript/Java)
- [ ] Can type/paste code in Code textarea
- [ ] "Run Code" button is clickable (not grayed out)
- [ ] Python "Hello World" test produces output `Hello World!`
- [ ] Debug info shows "Execution successful"
- [ ] Different languages work (JavaScript, Java, C++)
- [ ] Can clear code and run new tests

---

## ?? LEARNING PATH

### Level 1 - Basic
1. ? Hello World (Python)
2. ? Print numbers (Python)
3. ? Simple math (Python)

### Level 2 - Variables
1. ? Assign variables
2. ? Use variables
3. ? Math with variables

### Level 3 - Control Flow
1. ? If statements
2. ? Loops
3. ? Functions

### Level 4 - Multiple Languages
1. ? JavaScript tests
2. ? Java tests
3. ? C++ tests

---

## ?? YOU'RE ALL SET!

Your OneCompiler integration is ready to use. Now:
1. Test the main app
2. Create problems
3. Have users solve them!

Good luck! ??

