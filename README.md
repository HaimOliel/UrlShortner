
# UrlShortner

A simple URL shortening API built with **ASP.NET Core (.NET 8)**.

This project lets you:
- Create short codes for long URLs
- Redirect users from a short code to the original URL

---

## 🚀 API Endpoints

### 🔗 POST `http://localhost:8080`

Creates a new short URL.

#### Request
```json
{
  "redirectUrl": "https://www.example.com"
}
```

#### Response
```json
{
  "shortUrl": "abc123"
}
```

You can then use this short code to redirect.

---

### 🚀 GET `http://localhost:8080/{shortCode}`

Redirects to the original URL.

#### Example
```
GET /abc123
```

#### Behavior
- If the short code exists, it returns an **HTTP 302 redirect** to the original URL.
- If the short code does not exist, it returns **404 Not Found**.

---

## 📝 How to run

Make sure you have [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed.

```bash
git clone https://github.com/HaimOliel/UrlShortner.git
cd UrlShortner
dotnet run
```

The API will run on:
- `http://localhost:8080`

---

✅ That’s it. You now have a simple URL shortener API!