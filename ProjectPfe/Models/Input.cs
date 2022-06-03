using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPfe.Models
{
    public class Input
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Filename { get; set; }
        public string? Metadata { get; set; }
        public string? Length { get; set; }
        public String? IdChunks { get; set; }
    }
}
