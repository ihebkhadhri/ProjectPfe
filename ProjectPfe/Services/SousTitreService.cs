using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;

namespace ProjectPfe.Services
{
    public class SousTitreService
    {
        private readonly IMongoCollection<Sous_titre> _SoustitreCollection;

        public SousTitreService(
            IOptions<DbPfeDatabaseSettings> SoustitrestoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                SoustitrestoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SoustitrestoreDatabaseSettings.Value.DatabaseName);

            _SoustitreCollection = mongoDatabase.GetCollection<Sous_titre>(
                SoustitrestoreDatabaseSettings.Value.Sous_TitreCollectionName);
        }

        public List<Sous_titre> Get() =>
             _SoustitreCollection.Find(_ => true).ToList();

        public Sous_titre Get(string id) =>
             _SoustitreCollection.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Sous_titre newSousTitre) =>
             _SoustitreCollection.InsertOne(newSousTitre);

        public void Update(string id, Sous_titre updatedSousTitre) =>
             _SoustitreCollection.ReplaceOne(x => x.Id == id, updatedSousTitre);

        public void Remove(string id) =>
             _SoustitreCollection.DeleteOne(x => x.Id == id);
    }
    }

