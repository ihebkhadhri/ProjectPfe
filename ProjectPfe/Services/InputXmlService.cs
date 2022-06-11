using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;

namespace ProjectPfe.Services
{
    public class InputXmlService
    {
        private readonly IMongoCollection<Input> _InputCollection;

        public InputXmlService(
            IOptions<DbPfeDatabaseSettings> InputXmlStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                InputXmlStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                InputXmlStoreDatabaseSettings.Value.DatabaseName);

            _InputCollection = mongoDatabase.GetCollection<Input>(
                InputXmlStoreDatabaseSettings.Value.InputCollectionName);
        }

        public List<Input> Get() =>
             _InputCollection.Find(_ => true).ToList();

        public Input Get(string id) =>
             _InputCollection.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Input newInput) =>
             _InputCollection.InsertOne(newInput);

        public void Update(string id, Input updatedInput) =>
             _InputCollection.ReplaceOne(x => x.Id == id, updatedInput);

        public void Remove(string id) =>
             _InputCollection.DeleteOne(x => x.Id == id);
    }
}

