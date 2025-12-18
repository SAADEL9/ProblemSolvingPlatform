# ?? TROUBLESHOOTING: DNS Error - Cannot Connect to OneCompiler API

## ? Error You're Seeing

```
Error: No such host is known. (api.onecompiler.com:443)
```

## ?? What This Means

Your **server** (backend/ASP.NET Core) cannot resolve the DNS for `api.onecompiler.com`. This is a **network connectivity issue on the server side**, not a frontend problem.

---

## ? SOLUTIONS (Try in Order)

### Solution 1: Check Internet Connection on Server

**Windows:**
1. Open Command Prompt (cmd)
2. Type: `ping api.onecompiler.com`
3. You should see responses from the server
4. If not: You don't have internet from your machine

**If ping fails:**
- Check WiFi/Ethernet connection
- Check if behind a corporate firewall
- Try: `ping google.com` to verify internet works

---

### Solution 2: Check DNS Resolution

**In Command Prompt:**
```
nslookup api.onecompiler.com
```

**You should see:**
```
Server:  ...
Address:  ...

Non-authoritative answer:
Name:    api.onecompiler.com
Address:  1.2.3.4
```

**If it fails:**
- Your DNS is not resolving correctly
- Try changing DNS:
  - Settings ? Network & Internet ? DNS settings
  - Use Google DNS: 8.8.8.8

---

### Solution 3: Firewall/Proxy Issues

**If on Corporate Network:**
1. Check if proxy is blocking api.onecompiler.com
2. Contact IT department
3. May need proxy settings in ASP.NET Core

**Configure Proxy in Program.cs:**
```csharp
builder.Services.AddHttpClient()
    .ConfigureHttpClient(client => {
        // Add proxy if needed
        var proxy = new System.Net.WebProxy("your-proxy-url:port");
        HttpClientHandler handler = new HttpClientHandler();
        handler.Proxy = proxy;
    });
```

---

### Solution 4: Try Direct Connection Test

**Open OneCompiler in Browser:**
1. Go to: https://api.onecompiler.com/api/v1/code/exec
2. If you see a 405 error (Method Not Allowed), API is reachable ?
3. If you get connection error, your network is blocking it ?

---

### Solution 5: Use Alternative API (Temporary)

If OneCompiler is blocked, try using a different code execution service:

**Option A: Judge0 API**
```csharp
var response = await client.PostAsync("https://judge0-ce.p.rapidapi.com/submissions", content);
```

**Option B: Piston API (Free, No Key)**
```csharp
var response = await client.PostAsync("https://emkc.org/api/v2/piston/execute", content);
```

**Option C: Compile.online API**
```csharp
var response = await client.PostAsync("https://api.compile.online/api/v1/execute", content);
```

---

### Solution 6: Check if OneCompiler API is Down

**Visit:**
- https://status.onecompiler.com
- https://onecompiler.com (test their IDE directly)

If their site is down, their API is down too.

---

## ??? QUICK FIX - Use Local/Browser Alternative

Since server-side API call is failing, use **browser-to-API** approach instead:

Update `Views/Problemes/Details.cshtml` to call OneCompiler API **directly from browser** (instead of through backend):

```javascript
// Change this:
const response = await fetch('@Url.Action("ExecuteCode", "Problemes")', {

// To this:
const response = await fetch('https://api.onecompiler.com/api/v1/code/exec', {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json',
    },
    body: JSON.stringify(payload),
    mode: 'cors'
});
```

This **bypasses your server** and goes directly to OneCompiler from the browser.

---

## ? QUICK FIX - Implement Now

Let me create an updated version that calls OneCompiler directly from the browser:

1. **Pros:**
   - Works regardless of server network issues
   - Faster (no server hop)
   - Same CORS as before

2. **Cons:**
   - Requires browser to have internet
   - API key exposed (if needed)

---

## ?? DIAGNOSTIC CHECKLIST

- [ ] Can ping google.com from command prompt?
- [ ] Can ping api.onecompiler.com from command prompt?
- [ ] Can nslookup api.onecompiler.com?
- [ ] Can access https://onecompiler.com in browser?
- [ ] Are you on corporate WiFi/network?
- [ ] Is Windows Defender/Antivirus blocking it?
- [ ] Is VPN connected (and blocking OneCompiler)?

---

## ?? IMMEDIATE ACTION

**Try this test in browser console:**

```javascript
fetch('https://api.onecompiler.com/api/v1/code/exec', {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify({
        language: 'python3',
        code: 'print("test")',
        stdin: ''
    })
}).then(r => r.json()).then(data => console.log(data))
```

**If this works in browser console:** The API is reachable from your machine, but your ASP.NET Core backend can't reach it. **Use browser-side API call instead.**

**If this fails:** Your network is blocking OneCompiler API.

---

## ?? SOLUTION I IMPLEMENTED

I updated the error handling to give you better messages. When you see the error now, it will tell you:

```
Error: Network error: Cannot connect to OneCompiler API. 
Check your internet connection or firewall settings.

Details: [specific error]
```

---

## ?? NEXT STEPS

1. **Try the browser console test above**
2. **Run the diagnostic checklist**
3. **Let me know results**
4. **I can implement browser-side API call if needed**

---

## ?? IF YOU WANT BROWSER-SIDE API CALL

Just say "make the API call directly from browser" and I'll update the code to bypass the backend API call and go direct to OneCompiler.

This is actually **better** because:
- No server network issues
- Faster execution
- Simpler debugging
- Works in more network environments

---

## REFERENCE

- OneCompiler API Docs: https://onecompiler.com/apis/code-execution
- OneCompiler Status: https://status.onecompiler.com
- Test DNS: `nslookup api.onecompiler.com`
- Test Connectivity: `ping api.onecompiler.com`

