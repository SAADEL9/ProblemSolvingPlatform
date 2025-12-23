# ?? PROBLEM SOLUTIONS - JAVASCRIPT VERSIONS

For users who prefer JavaScript, here are solutions for key problems:

---

## Problem 1: Two Sum (JavaScript)

```javascript
function twoSum(nums, target) {
    const seen = {};
    
    for (let i = 0; i < nums.length; i++) {
        const complement = target - nums[i];
        if (complement in seen) {
            return [seen[complement], i];
        }
        seen[nums[i]] = i;
    }
    
    return [];
}

// Test Cases
console.log(twoSum([2, 7, 11, 15], 9));  // [0, 1]
console.log(twoSum([3, 2, 4], 6));       // [1, 2]
```

---

## Problem 2: Valid Parentheses (JavaScript)

```javascript
function isValid(s) {
    const stack = [];
    const matching = { '(': ')', '{': '}', '[': ']' };
    
    for (const char of s) {
        if (char in matching) {
            stack.push(char);
        } else {
            if (stack.length === 0 || matching[stack.pop()] !== char) {
                return false;
            }
        }
    }
    
    return stack.length === 0;
}

// Test Cases
console.log(isValid("()"));        // true
console.log(isValid("([)]"));      // false
console.log(isValid("{[]}"));      // true
```

---

## Problem 4: Binary Search (JavaScript)

```javascript
function binarySearch(nums, target) {
    let left = 0, right = nums.length - 1;
    
    while (left <= right) {
        const mid = Math.floor((left + right) / 2);
        
        if (nums[mid] === target) {
            return mid;
        } else if (nums[mid] < target) {
            left = mid + 1;
        } else {
            right = mid - 1;
        }
    }
    
    return -1;
}

// Test Cases
console.log(binarySearch([1, 3, 5, 6], 5));  // 2
console.log(binarySearch([1, 3, 5, 6], 4));  // -1
```

---

## Problem 6: Group Anagrams (JavaScript)

```javascript
function groupAnagrams(strs) {
    const anagrams = {};
    
    for (const word of strs) {
        const key = word.split('').sort().join('');
        
        if (!anagrams[key]) {
            anagrams[key] = [];
        }
        
        anagrams[key].push(word);
    }
    
    return Object.values(anagrams);
}

// Test Cases
console.log(groupAnagrams(["eat","tea","ate","nat","tan","bat"]));
// [["eat","tea","ate"],["nat","tan"],["bat"]]
```

---

## Problem 7: Maximum Subarray (JavaScript)

```javascript
function maxSubArray(nums) {
    let maxCurrent = nums[0];
    let maxGlobal = nums[0];
    
    for (let i = 1; i < nums.length; i++) {
        maxCurrent = Math.max(nums[i], maxCurrent + nums[i]);
        maxGlobal = Math.max(maxGlobal, maxCurrent);
    }
    
    return maxGlobal;
}

// Test Cases
console.log(maxSubArray([-2, 1, -3, 4, -1, 2, 1, -5, 4]));  // 6
console.log(maxSubArray([5, 4, -1, 7, 8]));                 // 23
```

---

## Problem 9: Word Search (JavaScript)

```javascript
function exist(board, word) {
    function dfs(i, j, k) {
        if (k === word.length) return true;
        
        if (i < 0 || i >= board.length || 
            j < 0 || j >= board[0].length || 
            board[i][j] !== word[k]) {
            return false;
        }
        
        const original = board[i][j];
        board[i][j] = '#';
        
        const found = dfs(i + 1, j, k + 1) ||
                      dfs(i - 1, j, k + 1) ||
                      dfs(i, j + 1, k + 1) ||
                      dfs(i, j - 1, k + 1);
        
        board[i][j] = original;
        return found;
    }
    
    for (let i = 0; i < board.length; i++) {
        for (let j = 0; j < board[0].length; j++) {
            if (dfs(i, j, 0)) return true;
        }
    }
    
    return false;
}

// Test Cases
const board = [
    ["A","B","C","E"],
    ["S","F","C","S"],
    ["A","D","E","E"]
];
console.log(exist(board, "ABCCED"));  // true
console.log(exist(board, "ABCB"));    // false
```

---

## Problem 10: Longest Valid Parentheses (JavaScript)

```javascript
function longestValidParentheses(s) {
    if (s.length < 2) return 0;
    
    const dp = new Array(s.length).fill(0);
    let maxLen = 0;
    
    for (let i = 1; i < s.length; i++) {
        if (s[i] === ')') {
            if (s[i - 1] === '(') {
                dp[i] = (i >= 2 ? dp[i - 2] : 0) + 2;
            } else if (dp[i - 1] > 0) {
                const matchIdx = i - dp[i - 1] - 1;
                if (matchIdx >= 0 && s[matchIdx] === '(') {
                    dp[i] = dp[i - 1] + 2 + (matchIdx > 0 ? dp[matchIdx - 1] : 0);
                }
            }
            
            maxLen = Math.max(maxLen, dp[i]);
        }
    }
    
    return maxLen;
}

// Test Cases
console.log(longestValidParentheses("(()"));     // 2
console.log(longestValidParentheses(")()())"));  // 4
```

---

## Java Versions

### Two Sum (Java)

```java
class Solution {
    public int[] twoSum(int[] nums, int target) {
        Map<Integer, Integer> seen = new HashMap<>();
        
        for (int i = 0; i < nums.length; i++) {
            int complement = target - nums[i];
            if (seen.containsKey(complement)) {
                return new int[]{seen.get(complement), i};
            }
            seen.put(nums[i], i);
        }
        
        return new int[]{};
    }
}
```

### Valid Parentheses (Java)

```java
class Solution {
    public boolean isValid(String s) {
        Stack<Character> stack = new Stack<>();
        Map<Character, Character> matching = new HashMap<>();
        matching.put('(', ')');
        matching.put('{', '}');
        matching.put('[', ']');
        
        for (char c : s.toCharArray()) {
            if (matching.containsKey(c)) {
                stack.push(c);
            } else {
                if (stack.isEmpty() || matching.get(stack.pop()) != c) {
                    return false;
                }
            }
        }
        
        return stack.isEmpty();
    }
}
```

### Binary Search (Java)

```java
class Solution {
    public int search(int[] nums, int target) {
        int left = 0, right = nums.length - 1;
        
        while (left <= right) {
            int mid = left + (right - left) / 2;
            
            if (nums[mid] == target) {
                return mid;
            } else if (nums[mid] < target) {
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }
        
        return -1;
    }
}
```

---

## C++ Versions

### Two Sum (C++)

```cpp
class Solution {
public:
    vector<int> twoSum(vector<int>& nums, int target) {
        unordered_map<int, int> seen;
        
        for (int i = 0; i < nums.size(); i++) {
            int complement = target - nums[i];
            if (seen.find(complement) != seen.end()) {
                return {seen[complement], i};
            }
            seen[nums[i]] = i;
        }
        
        return {};
    }
};
```

### Valid Parentheses (C++)

```cpp
class Solution {
public:
    bool isValid(string s) {
        stack<char> st;
        unordered_map<char, char> matching = {
            {'(', ')'}, {'{', '}'}, {'[', ']'}
        };
        
        for (char c : s) {
            if (matching.count(c)) {
                st.push(c);
            } else {
                if (st.empty() || matching[st.top()] != c) {
                    return false;
                }
                st.pop();
            }
        }
        
        return st.empty();
    }
};
```

---

## ?? KEY DIFFERENCES BY LANGUAGE

| Feature | Python | JavaScript | Java | C++ |
|---------|--------|-----------|------|-----|
| Arrays | Lists | Arrays | Arrays | Vectors |
| Hash Map | dict | Object/Map | HashMap | unordered_map |
| Stack | list | Array | Stack | stack |
| String Split | split() | split() | split() | stringstream |
| Sorting | sorted() | sort() | Collections.sort() | sort() |

---

## ?? USING IN YOUR PLATFORM

You can add these multi-language versions:

1. **Create problem with Python** (as template)
2. **Add JavaScript, Java, C++** as additional languages
3. Let users code in **any language** they prefer
4. Test runner works for all

---

## ? SUMMARY

You now have:
- ? Python solutions (complete)
- ? JavaScript solutions (key problems)
- ? Java solutions (key problems)
- ? C++ solutions (key problems)

Users can learn patterns across languages!
