using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;

namespace ProjectPfe.Services
{
    public class LigneService
    {


        private readonly IMongoCollection<Ligne> _LigneCollection;

        public LigneService(
            IOptions<DbPfeDatabaseSettings> LigneStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                LigneStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                LigneStoreDatabaseSettings.Value.DatabaseName);

            _LigneCollection = mongoDatabase.GetCollection<Ligne>(
                LigneStoreDatabaseSettings.Value.LigneCollectionName);
        }

        public List<Ligne> Get() =>
             _LigneCollection.Find(_ => true).ToList();

        public Ligne Get(string id) =>
             _LigneCollection.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Ligne newLigne) =>
             _LigneCollection.InsertOne(newLigne);

        public void Update(string id, Ligne updatedLigne) =>
             _LigneCollection.ReplaceOne(x => x.Id == id, updatedLigne);

        public void Remove(string id) =>
             _LigneCollection.DeleteOne(x => x.Id == id);
    }
}

