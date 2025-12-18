# ?? NETWORK ERROR TROUBLESHOOTING GUIDE

## Error You're Seeing
```
Error: Network error: Cannot connect to OneCompiler API. Check your internet connection or firewall settings.
```

---

## ?? QUICK FIXES (Try These First)

### Fix 1: Refresh Page (Most Common Fix)
```
1. Press F5 to refresh the page
2. Try running code again
3. If it works, you're done! ?
```

### Fix 2: Hard Refresh
```
1. Press Ctrl+Shift+R (Windows/Linux) or Cmd+Shift+R (Mac)
2. This clears browser cache
3. Try running code again
```

### Fix 3: Use Backend (No Direct API Call Needed)
```
The system should automatically fallback to backend.
If it's not working, the backend might have the same network issue.
```

---

## ?? DIAGNOSTIC STEPS

### Step 1: Check Internet Connection
```
1. Open a new tab
2. Go to: https://www.google.com
3. If Google loads: Internet is working ?
4. If Google doesn't load: You have no internet ?
```

### Step 2: Check If OneCompiler is Reachable
```
1. Open a new tab
2. Go to: https://onecompiler.com
3. If the site loads: OneCompiler is up ?
4. If it doesn't load: OneCompiler might be down ?
   (Try later or check: https://status.onecompiler.com)
```

### Step 3: Test Direct API in Browser Console
```
1. Go to your problem page
2. Press F12 to open DevTools
3. Go to Console tab
4. Paste this code:

fetch('https://api.onecompiler.com/api/v1/code/exec', {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify({
        language: 'python3',
        code: 'print("test")',
        stdin: ''
    })
}).then(r => r.json()).then(d => console.log(d))

5. Press Enter
6. Look at the result:
   - If you see output: Direct API works ?
   - If you see error: Firewall might be blocking ?
```

### Step 4: Check Browser Console for Errors
```
1. Open problem page
2. Press F12
3. Go to Console tab
4. Click "Run Code"
5. Look for messages starting with:
   - "Attempt 1: Trying direct OneCompiler API..."
   - "? Direct API failed..."
   - "Attempt 2: Trying backend API..."
   - "? Backend success..." or "? Backend failed..."
```

---

## ??? FIREWALL/NETWORK ISSUES

### If Behind Corporate Firewall
```
Your company might be blocking:
1. External API calls
2. The OneCompiler domain (api.onecompiler.com)
3. CORS requests
```

**Solution:** Ask IT department to whitelist:
- `api.onecompiler.com`
- `*.onecompiler.com`
- Your app's domain

### If on Corporate WiFi
```
Try:
1. Use personal hotspot from phone
2. Go to coffee shop with WiFi
3. Ask IT to allow API access
```

---

## ?? RECOMMENDED SOLUTION

### Use Backend Fallback (Most Reliable)
The system now automatically falls back to backend when direct API fails.

**How it works:**
```
Try: Browser ? OneCompiler API (fast, 1-2 sec)
  ? Fails
Fallback: Browser ? Your Backend ? OneCompiler API (slower, 2-3 sec)
  ? Should work even with network issues
```

**For this to work, make sure:**
- ? Your backend is running
- ? HTTP calls are allowed from backend
- ? Backend can reach OneCompiler API

---

## ?? BACKEND NETWORK FIX

If backend also can't reach OneCompiler, add this to `Program.cs`:

```csharp
// Add proxy if behind corporate proxy
builder.Services.AddHttpClient()
    .ConfigureHttpClient(client => {
        var proxy = new System.Net.WebProxy("your-proxy-url:port", false);
        var handler = new HttpClientHandler { Proxy = proxy };
        // Configure based on your proxy
    });
```

---

## ?? STEP-BY-STEP COMPLETE DIAGNOSTIC

### 1. Check Everything Works Locally
```
1. Start your app (F5)
2. Go to a problem page
3. Code should have: print("Hello World!")
4. Click "Run Code"
5. Watch browser console (F12)
```

### 2. What to Look For in Console
```
Console should show:
? Attempt 1: Trying direct OneCompiler API...
? Direct API Response status: 200  (if success)
   OR
? Attempt 1: Trying direct OneCompiler API...
? Direct API failed: [error message]
? Attempt 2: Trying backend API...
? Backend response status: 200
? Backend success: {result}
```

### 3. Expected Outcomes

**Scenario 1: Everything Works**
```
Output appears in Output tab ?
Status: SUCCESS
```

**Scenario 2: Direct Fails, Backend Works**
```
- Console shows: ? Direct API failed, ? Backend success
- Output still appears ?
- Status: SUCCESS (via backend)
```

**Scenario 3: Both Fail**
```
- Console shows: ? Direct API failed, ? Backend failed
- Error message appears
- Status: FAILURE
Solution: Check network/firewall
```

---

## ?? IF STILL NOT WORKING

### Checklist:
- [ ] Internet connection working (test Google)
- [ ] OneCompiler.com loads in browser
- [ ] Browser console shows what error
- [ ] Not behind proxy/VPN
- [ ] App is running locally (F5)
- [ ] Using correct port (localhost:5000 or 7295)
- [ ] Tried hard refresh (Ctrl+Shift+R)
- [ ] Waited 30 seconds and tried again

### Collect Debug Info:
```
1. Open problem page
2. Press F12 ? Console
3. Click "Run Code"
4. Right-click console ? Save as...
5. Save the log
6. Share with support
```

---

## ?? CHECK ONECOMPILER STATUS

Go to: https://status.onecompiler.com

If it shows red indicators, OneCompiler might be down.

---

## ?? WORKAROUND SOLUTIONS

### Workaround 1: Use Test Page
```
Go to: /onecompiler-test.html
This tests direct API without backend
```

### Workaround 2: Try Different Network
```
- Switch to phone hotspot
- Try coffee shop WiFi
- Try different network
```

### Workaround 3: Use Localhost Only
```
If firewalled, you could:
1. Deploy local OneCompiler instance
2. Configure to use local instead
3. This requires Docker/setup
```

---

## ?? FINAL RECOMMENDATION

**The system is designed to work even with network issues:**

1. ? **Tries direct API first** (Fast)
2. ? **Falls back to backend** (Reliable)
3. ? **Shows clear errors** (Helpful)

**Most likely your system WILL work** after the update because of the automatic backend fallback.

**Try now:**
1. Refresh the page (F5)
2. Click "Run Code"
3. Wait 3-5 seconds
4. Check console (F12) for status

---

## ?? SUPPORT

If you're still stuck:
1. Check browser console (F12)
2. Note any error messages
3. Run the test page: `/onecompiler-test.html`
4. Provide console logs
5. Let me know what you see

---

**Status: Code has been updated with better error handling. Try again now!** ??

