using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPfe.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public String DisplayName { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public UserRole UserRole { get; set; }
    }

    public enum UserRole
    {
        Administrateur, Utilisateur,Particulier
    }
}
