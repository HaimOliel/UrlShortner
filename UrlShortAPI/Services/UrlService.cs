using MongoDB.Driver;
using UrlShortAPI.Models;

namespace UrlShortAPI.Services;

public class UrlService
{
    private readonly IMongoCollection<Url> _urls;

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
}
