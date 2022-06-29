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
        public string? Age { get; set; }
        public string? Nationalite { get; set; }
        public string? Sex { get; set; }
        public String? PrixUnitaire { get; set; }
        public String? Adresse { get; set; }
        public DateTime? Created { get; set; }
        public string? rtfid { get; set; }
        public string? pdfid { get; set; }
        public string? inputfileid { get; set; }
        public Categorie categorie { get; set; }
        public EtatIntegration? etatIntegration { get; set; }
        public StatutIntegration? statutIntegration { get; set; }

        public String? fileName { get; set; }

         public List<Titre>? Titres { get; set; }
        public List<Tableau>? Tableaux { get; set; }
        public List<Paragraphe>? Paragraphes { get; set; }
        public User? UserImport { get; set; }
        public TemplateWord? template { get; set; }

    }

    public  enum EtatIntegration
    {
        Etape1,Etape2,Etap3
    }

    public enum StatutIntegration
    {
        NonTermine, Termine
    }
}
