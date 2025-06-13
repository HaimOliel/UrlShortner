using Microsoft.AspNetCore.Mvc;
using UrlShortAPI.Models;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("")]
    public class ShortController : ControllerBase
    {
        private static List<Url> urlList = new();

        [HttpGet("{name}")]
        public IActionResult GetUrlByNewUrl(string name)
        {
            // if (name == "haim")
            //     return Redirect("https://google.com");
            // else
            //     return Redirect("https://youtube.com");

            var url = urlList.FirstOrDefault(u => u.NewUrl == name);
            if (url == null)
            {
                return NotFound();
            }

            return Redirect(url.RedirectUrl);
        }

        [HttpPost]
        public IActionResult CreateUrl([FromBody] Url newUrl)
        {
            newUrl.Id = urlList.Count + 1;
            urlList.Add(newUrl);
            return CreatedAtAction(nameof(GetUrlByNewUrl), new { name = newUrl.NewUrl }, newUrl);
        }
    }
}