using MongoDB.Driver;
using UrlShortAPI.Models;

namespace UrlShortAPI.Services;

public class UrlService
{
    private readonly IMongoCollection<Url> _urls;

    public UrlService(IMongoDatabase database)
    {
        _urls = database.GetCollection<Url>("Urls");
    }

    public UrlService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("MongoDb"));
        var database = client.GetDatabase("UrlShortenerDB");
        _urls = database.GetCollection<Url>("Urls");
    }

    public async Task<List<Url>> GetAsync() =>
        await _urls.Find(_ => true).ToListAsync();

    public async Task<Url?> GetByShortUrlAsync(string shortUrl) =>
        await _urls.Find(u => u.NewUrl == shortUrl).FirstOrDefaultAsync();

    public async Task CreateAsync(Url newUrl) =>
        await _urls.InsertOneAsync(newUrl);

    private static string GenerateRandomShortCode(int length = 6)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public async Task<string> GenerateUniqueShortCodeAsync()
    {
        string code;
        do
        {
            code = GenerateRandomShortCode();
        }
        while (await _urls.Find(u => u.NewUrl == code).AnyAsync());

        return code;
    }
    public async Task<Url?> GetByRedirectUrlAsync(string redirectUrl)
    {
        return await _urls.Find(u => u.RedirectUrl == redirectUrl).FirstOrDefaultAsync();
    }

}
