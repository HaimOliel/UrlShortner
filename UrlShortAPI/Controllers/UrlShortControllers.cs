using Microsoft.AspNetCore.Mvc;
using UrlShortAPI.Models;
using UrlShortAPI.Services;
namespace MyApi.Controllers
{
    [ApiController]
    [Route("/")]
    public class ShortController : ControllerBase
    {
        private readonly UrlService _urlService;

        public ShortController(UrlService urlService)
        {
            _urlService = urlService;
        }
        [HttpGet("{newUrl}")]
        public async Task<IActionResult> GetUrlByNewUrl(string newUrl)
        {
            var url = await _urlService.GetByShortUrlAsync(newUrl);
            if (url == null)
                return NotFound();

            return Redirect(url.RedirectUrl);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUrl([FromBody] Url newUrl)
        {
            // Check if the redirect URL already exists
            var existing = await _urlService.GetByRedirectUrlAsync(newUrl.RedirectUrl);
            if (existing != null)
            {
                return Ok(existing); // Return existing short URL
            }

            // Generate a unique short code
            newUrl.NewUrl = await _urlService.GenerateUniqueShortCodeAsync();

            // Save to DB
            await _urlService.CreateAsync(newUrl);

            return CreatedAtAction(nameof(GetUrlByNewUrl), new { newUrl = newUrl.NewUrl }, newUrl);
        }

    }
}