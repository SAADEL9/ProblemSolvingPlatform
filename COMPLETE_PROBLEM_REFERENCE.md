# ?? COMPLETE PROBLEM SET - REFERENCE GUIDE

## ?? All 10 Problems at a Glance

| # | Problem | Difficulty | Key Concept | Time | Space |
|---|---------|------------|-------------|------|-------|
| 1 | Two Sum | Medium | Hash Map | O(n) | O(n) |
| 2 | Valid Parentheses | Easy | Stack | O(n) | O(n) |
| 3 | Reverse Linked List | Easy | Pointers | O(n) | O(1) |
| 4 | Binary Search | Easy | Divide & Conquer | O(log n) | O(1) |
| 5 | Flood Fill | Medium | DFS | O(n) | O(n) |
| 6 | Group Anagrams | Medium | Hash Map | O(nk log k) | O(nk) |
| 7 | Maximum Subarray | Medium | Kadane's Algo | O(n) | O(1) |
| 8 | Rotate Image | Medium | Matrix Ops | O(n²) | O(1) |
| 9 | Word Search | Hard | DFS + Backtrack | O(n × m × 4^l) | O(l) |
| 10 | Longest Valid Paren | Hard | DP | O(n) | O(n) |

---

## ?? QUICK REFERENCE - KEY ALGORITHMS

### 1. Hash Map (Two Sum, Group Anagrams)
```python
# Use dictionary to store values
seen = {}
for num in nums:
    if target - num in seen:
        return [seen[target - num], idx]
    seen[num] = idx
```

### 2. Stack (Valid Parentheses)
```python
stack = []
for char in s:
    if char in opening:
        stack.append(char)
    else:
        if not stack or not matches(stack.pop(), char):
            return False
return len(stack) == 0
```

### 3. Two Pointers (Reverse List)
```python
prev = None
while current:
    next_temp = current.next
    current.next = prev
    prev = current
    current = next_temp
```

### 4. Binary Search
```python
left, right = 0, len(nums) - 1
while left <= right:
    mid = (left + right) // 2
    if nums[mid] == target:
        return mid
    elif nums[mid] < target:
        left = mid + 1
    else:
        right = mid - 1
```

### 5. DFS (Flood Fill, Word Search)
```python
def dfs(node):
    if invalid_state:
        return
    visited.add(node)
    for neighbor in neighbors:
        if neighbor not in visited:
            dfs(neighbor)
```

### 6. Kadane's Algorithm (Maximum Subarray)
```python
max_current = max_global = nums[0]
for i in range(1, len(nums)):
    max_current = max(nums[i], max_current + nums[i])
    max_global = max(max_global, max_current)
```

### 7. Dynamic Programming (Longest Valid Parentheses)
```python
dp = [0] * len(s)
for i in range(1, len(s)):
    if s[i] == ')':
        # Calculate dp[i] based on previous states
        dp[i] = calculate_length()
```

---

## ?? LEARNING PATH

### Week 1 - Fundamentals
- **Problem 2:** Valid Parentheses (Stack)
- **Problem 3:** Reverse Linked List (Pointers)
- **Problem 4:** Binary Search (Divide & Conquer)

### Week 2 - Hash Maps & Arrays
- **Problem 1:** Two Sum (Hash Map)
- **Problem 6:** Group Anagrams (Hash Map + Sorting)
- **Problem 7:** Maximum Subarray (Kadane's Algorithm)

### Week 3 - Graph & Matrix
- **Problem 5:** Flood Fill (DFS)
- **Problem 8:** Rotate Image (Matrix Manipulation)

### Week 4 - Advanced
- **Problem 9:** Word Search (DFS + Backtracking)
- **Problem 10:** Longest Valid Parentheses (Dynamic Programming)

---

## ?? PROBLEM SOLVING STRATEGIES

### When to use Hash Map
- Need O(1) lookup
- Counting occurrences
- Finding pairs/complements

### When to use Stack
- Matching brackets/parentheses
- Last-in-first-out operations
- Undo/Redo functionality

### When to use DFS
- Tree/Graph traversal
- Backtracking problems
- Path finding

### When to use DP
- Overlapping subproblems
- Optimal substructure
- Need to avoid recalculation

---

## ? TESTING YOUR UNDERSTANDING

After solving each problem, ask yourself:

1. **Time Complexity:** Can I optimize this?
2. **Space Complexity:** Am I using extra space unnecessarily?
3. **Edge Cases:** Empty input, single element, duplicates?
4. **Alternative Approaches:** Is there a better way?
5. **Real-world Application:** Where would this be used?

---

## ?? NEXT CHALLENGES

After mastering these 10, try:

### Easy
- Palindrome Number
- Reverse String
- Contains Duplicate
- Valid Anagram

### Medium
- Next Permutation
- Longest Substring Without Repeating
- Search in Rotated Sorted Array
- 3Sum

### Hard
- Merge K Sorted Lists
- Median of Two Sorted Arrays
- Trapping Rain Water
- Regular Expression Matching

---

## ?? RESOURCES

### Learn More:
- **Hash Maps:** Python dictionaries are hash maps
- **Stacks:** Use lists with append/pop
- **Linked Lists:** Define custom ListNode class
- **DFS:** Use recursion with visit tracking
- **DP:** Build bottom-up solutions

### Practice:
- Solve problems in multiple languages
- Optimize for both time and space
- Try different approaches
- Teach someone else the solution

---

## ? TIPS FOR SUCCESS

1. **Read Carefully** - Understand constraints and requirements
2. **Start Simple** - Get working solution first, optimize later
3. **Test Thoroughly** - Include edge cases
4. **Document Code** - Clear variable names and comments
5. **Analyze Complexity** - Always calculate time/space
6. **Practice Daily** - Consistency beats intensity
7. **Code Interviews** - Use these problems to prepare

---

## ?? PROBLEM DIFFICULTY BREAKDOWN

### Easy (30% of interviews)
- Valid Parentheses
- Reverse Linked List
- Binary Search

These test fundamentals. Master these first!

### Medium (50% of interviews)
- Two Sum
- Flood Fill
- Group Anagrams
- Maximum Subarray
- Rotate Image

Most job interviews focus here.

### Hard (20% of interviews)
- Word Search
- Longest Valid Parentheses

These distinguish top candidates.

---

## ?? MASTERY CHECKLIST

For each problem, ensure you can:

- [ ] Explain the problem in your own words
- [ ] Write solution without looking at hints
- [ ] Solve it in under 30 minutes
- [ ] Explain time/space complexity
- [ ] Code it in multiple languages
- [ ] Solve 3 similar problems

---

## ?? NEED HELP?

If stuck on a problem:

1. **Re-read the description** - You might miss something
2. **Work through an example** - Write it out step by step
3. **Identify the pattern** - What algorithm does this need?
4. **Check the hints** - Reference hints in PROBLEM_SOLUTIONS_HINTS.md
5. **Look at similar problems** - Patterns repeat

---

## ?? FINAL THOUGHTS

These 10 problems represent:
- ? 70% of interview questions
- ? Core data structures & algorithms
- ? Multiple complexity levels
- ? Real-world applicability

**Master them, and you're ready to ace coding interviews!**

Happy coding! ??
