using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ConnexionMongo.Models
{
    public class Integration
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public double? Age { get; set; }
        public string? Nationalite { get; set; }
    }
}
