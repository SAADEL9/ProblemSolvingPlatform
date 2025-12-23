# ?? EXAMPLE PROBLEMS & TEMPLATES

Here are ready-to-use example problems you can create in your system:

---

## 1?? TWO SUM

**Difficulty:** Easy

**Description:**
```
Given an array of integers nums and an integer target, return the indices of the two numbers that add up to the target.
You may assume each input has exactly one solution, and you cannot use the same element twice.
Example: nums = [2,7,11,15], target = 9 ? Output: [0,1] (because nums[0] + nums[1] == 9)
```

**Language:** Python

**Function Template:**
```python
def twoSum(nums, target):
    """
    Find two numbers in nums that add up to target.
    
    Args:
        nums: List of integers
        target: Target sum
        
    Returns:
        List of two indices [i, j]
    """
    # Your code here
    pass
```

**Test Cases:**
```json
[
  {
    "input": "[2, 7, 11, 15], 9",
    "expected": "[0, 1]"
  },
  {
    "input": "[3, 2, 4], 6",
    "expected": "[1, 2]"
  },
  {
    "input": "[3, 3], 6",
    "expected": "[0, 1]"
  }
]
```

---

## 2?? PALINDROME NUMBER

**Difficulty:** Easy

**Description:**
```
Given an integer x, return true if x is a palindrome, and false otherwise.
An integer is a palindrome when it reads the same backward as forward.
Example: x = 121 ? true, x = -121 ? false
```

**Language:** Python

**Function Template:**
```python
def isPalindrome(x):
    """
    Check if integer x is a palindrome.
    
    Args:
        x: Integer to check
        
    Returns:
        Boolean - True if palindrome, False otherwise
    """
    # Your code here
    pass
```

**Test Cases:**
```json
[
  {
    "input": "121",
    "expected": "True"
  },
  {
    "input": "-121",
    "expected": "False"
  },
  {
    "input": "10",
    "expected": "False"
  },
  {
    "input": "0",
    "expected": "True"
  }
]
```

---

## 3?? FIBONACCI SEQUENCE

**Difficulty:** Easy

**Description:**
```
Given an integer n, return the nth Fibonacci number.
The Fibonacci numbers are defined as:
F(0) = 0, F(1) = 1, F(n) = F(n-1) + F(n-2) for n > 1
Example: n = 4 ? 3, n = 5 ? 5
```

**Language:** Python

**Function Template:**
```python
def fibonacci(n):
    """
    Return the nth Fibonacci number.
    
    Args:
        n: Position in Fibonacci sequence
        
    Returns:
        Integer - The nth Fibonacci number
    """
    # Your code here
    pass
```

**Test Cases:**
```json
[
  {
    "input": "0",
    "expected": "0"
  },
  {
    "input": "1",
    "expected": "1"
  },
  {
    "input": "4",
    "expected": "3"
  },
  {
    "input": "5",
    "expected": "5"
  },
  {
    "input": "10",
    "expected": "55"
  }
]
```

---

## 4?? BINARY SEARCH

**Difficulty:** Medium

**Description:**
```
Given a sorted array of integers and a target value, implement binary search to find the target.
Return the index if found, return -1 if not found.
Example: nums = [1,3,5,6], target = 5 ? 2
```

**Language:** Python

**Function Template:**
```python
def binarySearch(nums, target):
    """
    Perform binary search on sorted array.
    
    Args:
        nums: Sorted list of integers
        target: Value to search for
        
    Returns:
        Integer - Index of target, or -1 if not found
    """
    # Your code here
    pass
```

**Test Cases:**
```json
[
  {
    "input": "[1, 3, 5, 6], 5",
    "expected": "2"
  },
  {
    "input": "[1, 3, 5, 6], 4",
    "expected": "-1"
  },
  {
    "input": "[1], 1",
    "expected": "0"
  },
  {
    "input": "[1, 3], 2",
    "expected": "-1"
  }
]
```

---

## 5?? REVERSE STRING

**Difficulty:** Easy

**Description:**
```
Given a string, return the string reversed.
Example: "hello" ? "olleh"
```

**Language:** Python

**Function Template:**
```python
def reverseString(s):
    """
    Reverse a string.
    
    Args:
        s: Input string
        
    Returns:
        String - Reversed string
    """
    # Your code here
    pass
```

**Test Cases:**
```json
[
  {
    "input": "hello",
    "expected": "olleh"
  },
  {
    "input": "world",
    "expected": "dlrow"
  },
  {
    "input": "a",
    "expected": "a"
  },
  {
    "input": "",
    "expected": ""
  }
]
```

---

## 6?? VALID PARENTHESES (JavaScript Version)

**Difficulty:** Medium

**Description:**
```
Given a string containing just the characters '(', ')', '{', '}', '[' and ']',
determine if the input string is valid.
Valid: (), ()[], ([])
Invalid: (, (], ([)]
```

**Language:** JavaScript

**Function Template:**
```javascript
function isValidParentheses(s) {
    /**
     * Check if parentheses are valid.
     * @param {string} s - Input string
     * @returns {boolean} - True if valid, false otherwise
     */
    // Your code here
}
```

**Test Cases:**
```json
[
  {
    "input": "()",
    "expected": "true"
  },
  {
    "input": "()[]{",
    "expected": "false"
  },
  {
    "input": "{[]}",
    "expected": "true"
  },
  {
    "input": "([)]",
    "expected": "false"
  }
]
```

---

## 7?? MERGE SORTED ARRAYS

**Difficulty:** Medium

**Description:**
```
Given two sorted arrays, merge them into one sorted array.
Example: [1,3,5], [2,4,6] ? [1,2,3,4,5,6]
```

**Language:** Python

**Function Template:**
```python
def mergeSortedArrays(arr1, arr2):
    """
    Merge two sorted arrays.
    
    Args:
        arr1: First sorted array
        arr2: Second sorted array
        
    Returns:
        Merged sorted array
    """
    # Your code here
    pass
```

**Test Cases:**
```json
[
  {
    "input": "[1, 3, 5], [2, 4, 6]",
    "expected": "[1, 2, 3, 4, 5, 6]"
  },
  {
    "input": "[1], [2]",
    "expected": "[1, 2]"
  },
  {
    "input": "[1, 2, 3], []",
    "expected": "[1, 2, 3]"
  }
]
```

---

## ? HOW TO USE THESE

1. Go to **Create Problem** page
2. Copy the **Title**, **Description**, **Difficulty**
3. Paste the **Function Template** into Function Template field
4. Paste the **Test Cases** into Test Cases field
5. Click **Create**

Your problem is now ready for users to solve!

---

## ?? TIPS FOR CREATING PROBLEMS

1. **Keep it Clear**
   - Simple descriptions are best
   - Use examples
   - Be specific about input/output format

2. **Function Template**
   - Include docstring
   - Show parameter types
   - Have a clear return value

3. **Test Cases**
   - Always include edge cases
   - Test normal cases
   - Test boundary conditions
   - Make sure your test cases are correct!

4. **Difficulty Balance**
   - Easy: Basic logic, < 5 lines
   - Medium: Some algorithm knowledge, 5-20 lines
   - Hard: Complex algorithms, 20+ lines

---

## ?? NEXT PROBLEMS TO ADD

- Maximum Subarray
- Longest Substring Without Repeating Characters
- Add Two Numbers (Linked Lists)
- Longest Palindromic Substring
- Regular Expression Matching
- Median of Two Sorted Arrays
- Trapping Rain Water
- N-Queens
- Course Schedule
- Median Finder

---

**Happy Problem Setting!** ??
