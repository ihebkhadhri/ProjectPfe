using ConnexionMongo.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPfe.Models
{
    public class Paragraphe
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public String? libelle { get; set; }
        public Integration? integration { get; set; }
        public int? order { get; set; }
        public List<Sous_paragraphe>? Sous_paragraphe { get; set; }

    }
}
