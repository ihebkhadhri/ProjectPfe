using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ProjectPfe.Models
{
    public class Categorie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public String? libelle { get; set; }
        public String? description { get; set; }
    }
}
