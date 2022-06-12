using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProjectPfe.Models;

namespace ConnexionMongo.Models
{
    public class Integration
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? DateNaissance { get; set; }
        public double? Age { get; set; }
        public string? Nationalite { get; set; }
        public string? Sex { get; set; }
        public String? PrixUnitaire { get; set; }
        public String? Adresse { get; set; }

         public List<Titre>Titres { get; set; }
        public List<Tableau> Tableaux { get; set; }
        public List<Paragraphe> Paragraphes { get; set; }

    }
}
