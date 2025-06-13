namespace UrlShortAPI.Models
{
    public class Url
    {
        public int Id { get; set; }  // Optional for POST
        public required string NewUrl { get; set; }
        public required string RedirectUrl { get; set; }
    }
}
