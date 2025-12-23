# ?? ADD ALL 10 PROBLEMS TO YOUR PLATFORM

## ? QUICK SETUP (Copy & Paste)

Follow these steps to add each problem to your LeetCode clone:

---

## ?? PROBLEM 1: Two Sum

**Step 1:** Go to `/Problemes/Create`

**Step 2:** Fill in:
```
Title: Two Sum
Description: Given an array of integers, return the indices of the two distinct numbers whose sum equals the target. You must solve it in a single pass using a hash map for efficiency.
Difficulty: Medium
Language: Python
```

**Step 3:** Function Template - Copy this:
```python
def twoSum(nums, target):
    """
    Find two distinct numbers that sum to target.
    
    Args:
        nums: List of integers
        target: Target sum
        
    Returns:
        List of two indices [i, j] where i < j
    """
    # Use a hash map to store value -> index
    seen = {}
    
    for i, num in enumerate(nums):
        complement = target - num
        if complement in seen:
            return [seen[complement], i]
        seen[num] = i
    
    return []
```

**Step 4:** Test Cases - Copy this JSON:
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
  },
  {
    "input": "[2, 5, 5, 11], 10",
    "expected": "[1, 2]"
  }
]
```

**Step 5:** Click **Create** ?

---

## ?? PROBLEM 2: Valid Parentheses

**Go to:** `/Problemes/Create`

```
Title: Valid Parentheses
Description: Given a string containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid. An input string is valid if: 1) Open brackets must be closed by the same type of bracket 2) Open brackets must be closed in the correct order
Difficulty: Easy
Language: Python
```

**Function Template:**
```python
def isValid(s):
    """
    Check if parentheses string is valid.
    
    Args:
        s: String containing only brackets
        
    Returns:
        Boolean - True if valid, False otherwise
    """
    stack = []
    matching = {'(': ')', '{': '}', '[': ']'}
    
    for char in s:
        if char in matching:
            stack.append(char)
        else:
            if not stack or matching[stack.pop()] != char:
                return False
    
    return len(stack) == 0
```

**Test Cases:**
```json
[
  {
    "input": "()",
    "expected": "True"
  },
  {
    "input": "()[]{",
    "expected": "False"
  },
  {
    "input": "{[]}",
    "expected": "True"
  },
  {
    "input": "([)]",
    "expected": "False"
  },
  {
    "input": "",
    "expected": "True"
  }
]
```

---

## ?? PROBLEM 3: Reverse Linked List

**Go to:** `/Problemes/Create`

```
Title: Reverse Linked List
Description: Reverse a single linked list by reassigning pointers iteratively or recursively. Return the head of the new reversed list.
Difficulty: Easy
Language: Python
```

**Function Template:**
```python
class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next

def reverseList(head):
    """
    Reverse a linked list.
    
    Args:
        head: Head of the linked list
        
    Returns:
        New head of reversed list
    """
    prev = None
    current = head
    
    while current:
        next_temp = current.next
        current.next = prev
        prev = current
        current = next_temp
    
    return prev
```

**Test Cases:**
```json
[
  {
    "input": "[1, 2, 3, 4, 5]",
    "expected": "[5, 4, 3, 2, 1]"
  },
  {
    "input": "[1, 2]",
    "expected": "[2, 1]"
  },
  {
    "input": "[1]",
    "expected": "[1]"
  },
  {
    "input": "[]",
    "expected": "[]"
  }
]
```

---

## ?? PROBLEM 4: Binary Search

**Go to:** `/Problemes/Create`

```
Title: Binary Search
Description: Given a sorted array of integers and a target value, implement binary search to find the target. Return the index if found, return -1 if not found.
Difficulty: Easy
Language: Python
```

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
    left, right = 0, len(nums) - 1
    
    while left <= right:
        mid = (left + right) // 2
        
        if nums[mid] == target:
            return mid
        elif nums[mid] < target:
            left = mid + 1
        else:
            right = mid - 1
    
    return -1
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
    "input": "[1, 3], 3",
    "expected": "1"
  }
]
```

---

## ?? PROBLEM 5: Flood Fill

**Go to:** `/Problemes/Create`

```
Title: Flood Fill
Description: Given a 2D grid, an sr, sc position and a newColor, perform a flood fill (like paint bucket in image editors). Change the color of sr, sc and all adjacent cells with the same color.
Difficulty: Medium
Language: Python
```

**Function Template:**
```python
def floodFill(image, sr, sc, newColor):
    """
    Perform flood fill on image starting at (sr, sc).
    
    Args:
        image: 2D grid of colors
        sr, sc: Starting row and column
        newColor: New color to fill with
        
    Returns:
        Modified image
    """
    originalColor = image[sr][sc]
    
    if originalColor == newColor:
        return image
    
    def dfs(row, col):
        if row < 0 or row >= len(image) or col < 0 or col >= len(image[0]):
            return
        if image[row][col] != originalColor:
            return
        
        image[row][col] = newColor
        
        dfs(row + 1, col)
        dfs(row - 1, col)
        dfs(row, col + 1)
        dfs(row, col - 1)
    
    dfs(sr, sc)
    return image
```

**Test Cases:**
```json
[
  {
    "input": "[[1,1,1],[1,1,0],[1,0,1]], 1, 1, 2",
    "expected": "[[2,2,2],[2,2,0],[2,0,1]]"
  },
  {
    "input": "[[0,0,0],[0,0,0]], 0, 0, 2",
    "expected": "[[2,2,2],[2,2,2]]"
  }
]
```

---

## ?? PROBLEM 6: Group Anagrams

**Go to:** `/Problemes/Create`

```
Title: Group Anagrams
Description: Given an array of strings, group the anagrams together by sorting letters. Anagrams are words with the same letters in different orders.
Difficulty: Medium
Language: Python
```

**Function Template:**
```python
def groupAnagrams(strs):
    """
    Group anagrams together.
    
    Args:
        strs: List of strings
        
    Returns:
        List of lists, each containing grouped anagrams
    """
    anagrams = {}
    
    for word in strs:
        key = ''.join(sorted(word))
        
        if key not in anagrams:
            anagrams[key] = []
        
        anagrams[key].append(word)
    
    return list(anagrams.values())
```

**Test Cases:**
```json
[
  {
    "input": "[\"eat\",\"tea\",\"ate\",\"nat\",\"tan\",\"bat\"]",
    "expected": "[[\"eat\",\"tea\",\"ate\"],[\"nat\",\"tan\"],[\"bat\"]]"
  },
  {
    "input": "[\"\"]",
    "expected": "[[\"\"]]"
  },
  {
    "input": "[\"a\"]",
    "expected": "[[\"a\"]]"
  }
]
```

---

## ?? PROBLEM 7: Maximum Subarray

**Go to:** `/Problemes/Create`

```
Title: Maximum Subarray
Description: Find the contiguous subarray with the largest sum using Kadane's algorithm. Return the maximum sum.
Difficulty: Medium
Language: Python
```

**Function Template:**
```python
def maxSubArray(nums):
    """
    Find maximum sum of any contiguous subarray.
    
    Args:
        nums: List of integers
        
    Returns:
        Integer - Maximum subarray sum
    """
    max_current = max_global = nums[0]
    
    for i in range(1, len(nums)):
        max_current = max(nums[i], max_current + nums[i])
        max_global = max(max_global, max_current)
    
    return max_global
```

**Test Cases:**
```json
[
  {
    "input": "[-2, 1, -3, 4, -1, 2, 1, -5, 4]",
    "expected": "6"
  },
  {
    "input": "[5, 4, -1, 7, 8]",
    "expected": "23"
  },
  {
    "input": "[-2]",
    "expected": "-2"
  },
  {
    "input": "[1]",
    "expected": "1"
  }
]
```

---

## ?? PROBLEM 8: Rotate Image

**Go to:** `/Problemes/Create`

```
Title: Rotate Image
Description: Rotate an n × n 2D matrix 90 degrees clockwise in-place without using extra space.
Difficulty: Medium
Language: Python
```

**Function Template:**
```python
def rotate(matrix):
    """
    Rotate matrix 90 degrees clockwise in-place.
    
    Args:
        matrix: n × n 2D list
        
    Returns:
        None (modifies in-place)
    """
    n = len(matrix)
    
    for i in range(n):
        for j in range(i + 1, n):
            matrix[i][j], matrix[j][i] = matrix[j][i], matrix[i][j]
    
    for i in range(n):
        matrix[i].reverse()
```

**Test Cases:**
```json
[
  {
    "input": "[[1,2,3],[4,5,6],[7,8,9]]",
    "expected": "[[7,4,1],[8,5,2],[9,6,3]]"
  },
  {
    "input": "[[1,2],[3,4]]",
    "expected": "[[3,1],[4,2]]"
  }
]
```

---

## ?? PROBLEM 9: Word Search

**Go to:** `/Problemes/Create`

```
Title: Word Search
Description: Given an m x n grid of characters and a word, determine if the word exists in the grid. You can move to adjacent cells horizontally or vertically. The same letter cell may not be used more than once.
Difficulty: Hard
Language: Python
```

**Function Template:**
```python
def exist(board, word):
    """
    Determine if word exists in board.
    
    Args:
        board: m × n grid of characters
        word: Word to search for
        
    Returns:
        Boolean - True if word exists, False otherwise
    """
    def dfs(i, j, k):
        if k == len(word):
            return True
        
        if i < 0 or i >= len(board) or j < 0 or j >= len(board[0]) or board[i][j] != word[k]:
            return False
        
        original = board[i][j]
        board[i][j] = '#'
        
        found = (dfs(i + 1, j, k + 1) or
                dfs(i - 1, j, k + 1) or
                dfs(i, j + 1, k + 1) or
                dfs(i, j - 1, k + 1))
        
        board[i][j] = original
        
        return found
    
    for i in range(len(board)):
        for j in range(len(board[0])):
            if dfs(i, j, 0):
                return True
    
    return False
```

**Test Cases:**
```json
[
  {
    "input": "[[\"A\",\"B\",\"C\",\"E\"],[\"S\",\"F\",\"C\",\"S\"],[\"A\",\"D\",\"E\",\"E\"]], \"ABCCED\"",
    "expected": "True"
  },
  {
    "input": "[[\"A\",\"B\",\"C\",\"E\"],[\"S\",\"F\",\"C\",\"S\"],[\"A\",\"D\",\"E\",\"E\"]], \"ABCB\"",
    "expected": "False"
  }
]
```

---

## ?? PROBLEM 10: Longest Valid Parentheses

**Go to:** `/Problemes/Create`

```
Title: Longest Valid Parentheses
Description: Given a string containing only '(' and ')', find the length of the longest valid (well-formed) parentheses substring.
Difficulty: Hard
Language: Python
```

**Function Template:**
```python
def longestValidParentheses(s):
    """
    Find length of longest valid parentheses substring.
    
    Args:
        s: String containing only ( and )
        
    Returns:
        Integer - Length of longest valid substring
    """
    if len(s) < 2:
        return 0
    
    dp = [0] * len(s)
    max_len = 0
    
    for i in range(1, len(s)):
        if s[i] == ')':
            if s[i - 1] == '(':
                dp[i] = (dp[i - 2] if i >= 2 else 0) + 2
            elif dp[i - 1] > 0:
                match_idx = i - dp[i - 1] - 1
                if match_idx >= 0 and s[match_idx] == '(':
                    dp[i] = dp[i - 1] + 2 + (dp[match_idx - 1] if match_idx > 0 else 0)
            
            max_len = max(max_len, dp[i])
    
    return max_len
```

**Test Cases:**
```json
[
  {
    "input": "\"(()\"",
    "expected": "2"
  },
  {
    "input": "\")()())\"",
    "expected": "4"
  },
  {
    "input": "\"\"",
    "expected": "0"
  },
  {
    "input": "\"()(()\"",
    "expected": "2"
  }
]
```

---

## ? SUMMARY

You now have **10 complete LeetCode-style problems** ready to add!

**Each problem includes:**
- ? Complete description
- ? Function template (ready to paste)
- ? Test cases (ready to paste)
- ? Difficulty level
- ? Python solution

**To add them all:**
1. Go to each `/Problemes/Create`
2. Copy-paste from above
3. Click Create
4. Done!

Your platform is now fully loaded with quality coding problems! ??
