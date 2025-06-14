using UrlShortAPI.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Get MongoDB connection string from environment or fallback to localhost
var mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTIONSTRING")
                         ?? "mongodb://0.0.0.0:27017/UrlShortenerDb";

// Initialize MongoDB client and register it as a singleton
var mongoClient = new MongoClient(mongoConnectionString);
var mongoDatabase = mongoClient.GetDatabase("UrlShortenerDb");

// Register UrlService and pass in the database
builder.Services.AddSingleton(new UrlService(mongoDatabase));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Urls.Add("http://0.0.0.0:80");
app.Run();
