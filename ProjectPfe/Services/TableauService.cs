using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;

namespace ProjectPfe.Services
{
    public class TableauService
    {
        private readonly IMongoCollection<Tableau> _TableauCollection;

        public TableauService(
           IOptions<DbPfeDatabaseSettings> TableauStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                TableauStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                TableauStoreDatabaseSettings.Value.DatabaseName);

            _TableauCollection = mongoDatabase.GetCollection<Tableau>(
                TableauStoreDatabaseSettings.Value.TableauCollectionName);
        }

        public List<Tableau> Get() =>
             _TableauCollection.Find(_ => true).ToList();

        public Tableau Get(string id) =>
             _TableauCollection.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Tableau newTableau) =>
             _TableauCollection.InsertOne(newTableau);

        public void Update(string id, Tableau updatedTableau) =>
             _TableauCollection.ReplaceOne(x => x.Id == id, updatedTableau);

        public void Remove(string id) =>
             _TableauCollection.DeleteOne(x => x.Id == id);
    }
}

