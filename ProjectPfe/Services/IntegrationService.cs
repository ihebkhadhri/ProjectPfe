using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ConnexionMongo.Services
{
    public class IntegrationService
    {
        private readonly IMongoCollection<Integration> _IntegrationsCollection;

        public IntegrationService(
            IOptions<DbPfeDatabaseSettings> IntegrationStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                IntegrationStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                IntegrationStoreDatabaseSettings.Value.DatabaseName);

            _IntegrationsCollection = mongoDatabase.GetCollection<Integration>(
                IntegrationStoreDatabaseSettings.Value.IntegrationCollectionName);
        }

        public  List<Integration> Get() =>
             _IntegrationsCollection.Find(_ => true).ToList();

        public  Integration Get(string id) =>
             _IntegrationsCollection.Find(x => x.Id == id).FirstOrDefault();

        public void  Create(Integration newIntegration) =>
             _IntegrationsCollection.InsertOne(newIntegration);

        public void  Update(string id, Integration updatedIntegration) =>
             _IntegrationsCollection.ReplaceOne(x => x.Id == id, updatedIntegration);

        public void  Remove(string id) =>
             _IntegrationsCollection.DeleteOne(x => x.Id == id);

        
    }
}

