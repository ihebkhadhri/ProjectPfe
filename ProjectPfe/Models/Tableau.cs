using ConnexionMongo.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPfe.Models
{
    public class Tableau
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public String? libelle { get; set; }
        public Integration? integration { get; set; }
        public List<Ligne>? lignes { get; set; }
        
    }
}
