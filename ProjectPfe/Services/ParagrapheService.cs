using ProjectPfe.Models;
using ConnexionMongo.Models;
using ConnexionMongo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using ProjectPfe.Models;
using ProjectPfe.Services;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ProjectPfe.Services
{
    public class ParagrapheService
    {
        private readonly IMongoCollection<Paragraphe> _ParagraphesCollection;

        public ParagrapheService(
            IOptions<DbPfeDatabaseSettings> ParagrapheStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ParagrapheStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ParagrapheStoreDatabaseSettings.Value.DatabaseName);

            _ParagraphesCollection = mongoDatabase.GetCollection<Paragraphe>(
                ParagrapheStoreDatabaseSettings.Value.ParagrapheCollectionName);
        }

        public List<Paragraphe> Get() =>
             _ParagraphesCollection.Find(_ => true).ToList();

        public Paragraphe Get(string id) =>
             _ParagraphesCollection.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Paragraphe newParagraphe) =>
             _ParagraphesCollection.InsertOne(newParagraphe);

        public void Update(string id, Paragraphe updatedParagraphe) =>
             _ParagraphesCollection.ReplaceOne(x => x.Id == id, updatedParagraphe);

        public void Remove(string id) =>
             _ParagraphesCollection.DeleteOne(x => x.Id == id);
    }
}

