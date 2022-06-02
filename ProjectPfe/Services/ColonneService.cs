using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;

namespace ProjectPfe.Services
{
    public class ColonneService
    {

        private readonly IMongoCollection<Colonne> _ColonneCollection;

        public ColonneService(
            IOptions<DbPfeDatabaseSettings> ColonneStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ColonneStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ColonneStoreDatabaseSettings.Value.DatabaseName);

            _ColonneCollection = mongoDatabase.GetCollection<Colonne>(
                ColonneStoreDatabaseSettings.Value.ColonneCollectionName);
        }

        public List<Colonne> Get() =>
             _ColonneCollection.Find(_ => true).ToList();

        public Colonne Get(string id) =>
             _ColonneCollection.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Colonne newTitre) =>
             _ColonneCollection.InsertOne(newTitre);

        public void Update(string id, Colonne updatedColonne) =>
             _ColonneCollection.ReplaceOne(x => x.Id == id, updatedColonne);

        public void Remove(string id) =>
             _ColonneCollection.DeleteOne(x => x.Id == id);
    }
}

