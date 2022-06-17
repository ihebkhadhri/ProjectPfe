using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPfe.Models
{
    public class Ligne
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public String? libelle { get; set; }
        public List<Colonne>? colonnes { get; set; }
        public int? order { get; set; }
        [BsonIgnore]
        public Tableau? tableau { get; set; }
    }
}
