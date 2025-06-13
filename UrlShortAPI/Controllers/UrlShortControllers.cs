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
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUrlByNewUrl(string name)
        {
            var url = await _urlService.GetByShortUrlAsync(name);
            if (url == null)
                return NotFound();

            return Redirect(url.RedirectUrl);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUrl([FromBody] Url newUrl)
        {
            await _urlService.CreateAsync(newUrl);
            return CreatedAtAction(nameof(GetUrlByNewUrl), new { name = newUrl.NewUrl }, newUrl);
        }
    }
}