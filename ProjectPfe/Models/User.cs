using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectPfe.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public String? DisplayName { get; set; }
        public String? Username { get; set; }
        public String? Password { get; set; }
        public String? Email { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public UserRole? UserRole { get; set; }
        public Boolean? validate { get; set; }
    }
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Administrateur, Utilisateur,Particulier
           
    }

}
