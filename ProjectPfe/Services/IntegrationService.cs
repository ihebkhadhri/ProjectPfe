using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;

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

        public List<Integration> Getbyuser(string userid) => 
             _IntegrationsCollection.Find(x => x.UserImport.Id == userid && x.pdfid!=null).ToList();

        public List<Integration> Getarchivebystep1byuser(string userid) =>
             _IntegrationsCollection.Find(x => x.UserImport.Id == userid && x.etatIntegration== EtatIntegration.Etape1).ToList();

       

        public List<Integration> GetArchiveStep2ByUser() =>
             _IntegrationsCollection.Find(x=>x.UserImport.Id==UserConnected.user.Id && x.etatIntegration !=null && x.etatIntegration==EtatIntegration.Etape2 ).ToList();


        public List<Integration> GetArchiveStep3GroupCorilus() =>
             _IntegrationsCollection.Find(x => x.UserImport.UserRole  == UserRole.Utilisateur && x.etatIntegration != null && x.etatIntegration == EtatIntegration.Etap3).ToList();

        public List<Integration> GetIntegrationStep1() =>
           _IntegrationsCollection.Find(x => x.UserImport.UserRole==UserRole.Utilisateur && x.etatIntegration!=null && x.etatIntegration == EtatIntegration.Etape1).ToList();

        public List<Integration> GetArchiveStep3ByUser() =>
           _IntegrationsCollection.Find(x => x.UserImport.Id == UserConnected.user.Id && x.etatIntegration != null && x.etatIntegration == EtatIntegration.Etap3).ToList();
    }
}

