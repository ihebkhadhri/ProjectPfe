﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPfe.Models
{
    public class Sous_paragraphe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public String libelle { get; set; }
        public Paragraphe paragraphe { get; set; }
        
    }
}
