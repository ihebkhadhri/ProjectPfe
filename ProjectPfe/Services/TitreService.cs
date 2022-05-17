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
    public class TitreService
    {

        private readonly IMongoCollection<Titre> _TitresCollection;

        public TitreService(
            IOptions<DbPfeDatabaseSettings> TitreStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                TitreStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                TitreStoreDatabaseSettings.Value.DatabaseName);

            _TitresCollection = mongoDatabase.GetCollection<Titre>(
                TitreStoreDatabaseSettings.Value.TitreCollectionName);
        }

        public List<Titre> Get() =>
             _TitresCollection.Find(_ => true).ToList();

        public Titre Get(string id) =>
             _TitresCollection.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Titre newTitre) =>
             _TitresCollection.InsertOne(newTitre);

        public void Update(string id, Titre updatedTitre) =>
             _TitresCollection.ReplaceOne(x => x.Id == id, updatedTitre);

        public void Remove(string id) =>
             _TitresCollection.DeleteOne(x => x.Id == id);
    }

}
