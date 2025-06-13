using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrlShortAPI.Models
{
    public class Url
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("newUrl")]
        public string? NewUrl { get; set; }

        [BsonElement("redirectUrl")]
        public required string RedirectUrl { get; set; }
    }
}
