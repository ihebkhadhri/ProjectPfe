using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPfe.Models
{
    public class TemplateWord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Nom { get; set; }
        public DateTime? Creation_Date { get; set; }
        public User? user_create { get; set; }
        public DateTime? Date_Suprression { get; set; }
        public Categorie? categorie { get; set; }
        public String? FilePdfId { get; set; }
        public String? FileRtfId { get; set; }
    }
}
