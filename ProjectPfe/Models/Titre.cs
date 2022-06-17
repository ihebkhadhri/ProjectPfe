using ConnexionMongo.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPfe.Models
{
    public class Titre
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public String? libelle { get; set; }
        public int? order { get; set; }

        [BsonIgnore]
        public Integration? integration { get; set; }
        public List<Sous_titre>? Sous_titres { get; set; }

    }
}
