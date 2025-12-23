# ?? PROBLEM SOLUTIONS - HINTS & EXAMPLES

## Problem 1: Two Sum
**Difficulty:** Medium

### ?? Description
Given an array of integers, return the indices of the two distinct numbers whose sum equals the target. You must solve it in a single pass using a hash map for efficiency.

### ?? Hints
1. Use a **hash map** to store values you've seen and their indices
2. For each number, check if `target - current_number` exists in the map
3. Time complexity: O(n), Space complexity: O(n)

### ?? Function Template (Python)
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
    
    return []  # No solution found
```

### ? Test Cases (JSON)
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

---

## Problem 2: Valid Parentheses
**Difficulty:** Easy

### ?? Description
Given a string containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid. An input string is valid if:
1. Open brackets must be closed by the same type of bracket
2. Open brackets must be closed in the correct order

### ?? Hints
1. Use a **stack** data structure
2. Push opening brackets onto the stack
3. For closing brackets, check if the stack top matches
4. Stack should be empty at the end

### ?? Function Template (Python)
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
        if char in matching:  # Opening bracket
            stack.append(char)
        else:  # Closing bracket
            if not stack or matching[stack.pop()] != char:
                return False
    
    return len(stack) == 0
```

### ? Test Cases (JSON)
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

## Problem 3: Reverse Linked List
**Difficulty:** Easy

### ?? Description
Reverse a single linked list by reassigning pointers iteratively or recursively. Return the head of the new reversed list.

### ?? Hints
1. Keep track of three pointers: **prev**, **current**, **next**
2. In each iteration, reverse the link
3. Move pointers forward
4. Continue until current is None

### ?? Function Template (Python)
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
        # Store next node
        next_temp = current.next
        # Reverse the link
        current.next = prev
        # Move prev and current forward
        prev = current
        current = next_temp
    
    return prev
```

### ? Test Cases (JSON)
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

## Problem 4: Binary Search
**Difficulty:** Easy

### ?? Description
Given a sorted array of integers and a target value, implement binary search to find the target. Return the index if found, return -1 if not found.

### ?? Hints
1. Use **left** and **right** pointers
2. Calculate **mid** point
3. Compare target with mid value
4. Eliminate half of the search space in each iteration
5. Time complexity: O(log n)

### ?? Function Template (Python)
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

### ? Test Cases (JSON)
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

## Problem 5: Flood Fill
**Difficulty:** Medium

### ?? Description
Given a 2D grid, an sr, sc position and a newColor, perform a flood fill (like paint bucket in image editors). Change the color of sr, sc and all adjacent cells with the same color.

### ?? Hints
1. Use **DFS** or **BFS** approach
2. Start from the given position
3. Change color and explore all 4 directions (up, down, left, right)
4. Stop when you hit a different color or boundary

### ?? Function Template (Python)
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
        
        # Explore 4 directions
        dfs(row + 1, col)
        dfs(row - 1, col)
        dfs(row, col + 1)
        dfs(row, col - 1)
    
    dfs(sr, sc)
    return image
```

### ? Test Cases (JSON)
```json
[
  {
    "input": "[[1,1,1],[1,1,0],[1,0,1]], 1, 1, 2",
    "expected": "[[2,2,2],[2,2,0],[2,0,1]]"
  },
  {
    "input": "[[0,0,0],[0,0,0]], 0, 0, 2",
    "expected": "[[2,2,2],[2,2,2]]"
  },
  {
    "input": "[[0,0,0],[0,1,1]], 1, 1, 1",
    "expected": "[[0,0,0],[0,1,1]]"
  }
]
```

---

## Problem 6: Group Anagrams
**Difficulty:** Medium

### ?? Description
Given an array of strings, group the anagrams together by sorting letters. Anagrams are words with the same letters in different orders.

### ?? Hints
1. Sort characters in each word
2. Use sorted string as **key** in a hash map
3. Group words with the same key
4. Return all groups

### ?? Function Template (Python)
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
        # Sort letters to create key
        key = ''.join(sorted(word))
        
        if key not in anagrams:
            anagrams[key] = []
        
        anagrams[key].append(word)
    
    return list(anagrams.values())
```

### ? Test Cases (JSON)
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

## Problem 7: Maximum Subarray
**Difficulty:** Medium

### ?? Description
Find the contiguous subarray with the largest sum using Kadane's algorithm. Return the maximum sum.

### ?? Hints
1. Use **Kadane's Algorithm**
2. Keep track of max sum ending at current position
3. Keep track of overall maximum
4. Time complexity: O(n)

### ?? Function Template (Python)
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

### ? Test Cases (JSON)
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

## Problem 8: Rotate Image
**Difficulty:** Medium

### ?? Description
Rotate an n × n 2D matrix 90 degrees clockwise in-place without using extra space.

### ?? Hints
1. **Transpose** the matrix first
2. **Reverse** each row
3. Both operations in-place
4. Time: O(n²), Space: O(1)

### ?? Function Template (Python)
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
    
    # Step 1: Transpose
    for i in range(n):
        for j in range(i + 1, n):
            matrix[i][j], matrix[j][i] = matrix[j][i], matrix[i][j]
    
    # Step 2: Reverse each row
    for i in range(n):
        matrix[i].reverse()
```

### ? Test Cases (JSON)
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

## Problem 9: Word Search
**Difficulty:** Hard

### ?? Description
Given an m x n grid of characters and a word, determine if the word exists in the grid. You can move to adjacent cells (horizontally or vertically). The same letter cell may not be used more than once.

### ?? Hints
1. Use **DFS** with backtracking
2. Mark visited cells to avoid reusing
3. Explore all 4 directions
4. Backtrack when path doesn't lead to solution

### ?? Function Template (Python)
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
        # If we've matched all characters, word found
        if k == len(word):
            return True
        
        # Check bounds and if character matches
        if i < 0 or i >= len(board) or j < 0 or j >= len(board[0]) or board[i][j] != word[k]:
            return False
        
        # Mark as visited
        original = board[i][j]
        board[i][j] = '#'
        
        # Explore 4 directions
        found = (dfs(i + 1, j, k + 1) or
                dfs(i - 1, j, k + 1) or
                dfs(i, j + 1, k + 1) or
                dfs(i, j - 1, k + 1))
        
        # Backtrack
        board[i][j] = original
        
        return found
    
    # Try starting from each cell
    for i in range(len(board)):
        for j in range(len(board[0])):
            if dfs(i, j, 0):
                return True
    
    return False
```

### ? Test Cases (JSON)
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

## Problem 10: Longest Valid Parentheses
**Difficulty:** Hard

### ?? Description
Given a string containing only '(' and ')', find the length of the longest valid (well-formed) parentheses substring.

### ?? Hints
1. Use **dynamic programming** or stack approach
2. DP[i] = length of valid parentheses ending at index i
3. For ')' at index i:
   - If s[i-1] is '(', then DP[i] = DP[i-2] + 2
   - If s[i-1] is ')' and s[i - DP[i-1] - 1] is '(', then DP[i] = DP[i-1] + 2 + DP[i - DP[i-1] - 2]

### ?? Function Template (Python)
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

### ? Test Cases (JSON)
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

## ?? USAGE IN YOUR PLATFORM

1. Go to **Create Problem** as Admin
2. Copy **Title**, **Description**, **Difficulty**
3. Paste **Function Template** into "Function Template" field
4. Paste **Test Cases** JSON into "Test Cases" field
5. Click **Create**

Each problem is now ready for users to solve!

---

## ?? KEY TAKEAWAYS

| Problem | Key Concept | Time | Space |
|---------|-------------|------|-------|
| Two Sum | Hash Map | O(n) | O(n) |
| Valid Parentheses | Stack | O(n) | O(n) |
| Reverse Linked List | Pointers | O(n) | O(1) |
| Binary Search | Divide & Conquer | O(log n) | O(1) |
| Flood Fill | DFS | O(n) | O(n) |
| Group Anagrams | Hash Map | O(nk log k) | O(nk) |
| Maximum Subarray | Kadane's Algo | O(n) | O(1) |
| Rotate Image | Matrix Ops | O(n²) | O(1) |
| Word Search | DFS + Backtrack | O(n × m × 4^l) | O(l) |
| Longest Valid Paren | DP | O(n) | O(n) |

