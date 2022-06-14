using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;

namespace ProjectPfe.Services
{
    public class CategorieService
    {
        private readonly IMongoCollection<Categorie> _CategoriesCollection;
        public CategorieService(
           IOptions<DbPfeDatabaseSettings> CategorieStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                CategorieStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                CategorieStoreDatabaseSettings.Value.DatabaseName);

            _CategoriesCollection = mongoDatabase.GetCollection<Categorie>(
                CategorieStoreDatabaseSettings.Value.CategorieCollectionName);
        }

        public List<Categorie> Get() =>
             _CategoriesCollection.Find(_ => true).ToList();

        public Categorie Get(string id) =>
             _CategoriesCollection.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Categorie newCategorie) =>
             _CategoriesCollection.InsertOne(newCategorie);

        public void Update(string id, Categorie updatedCategorie) =>
             _CategoriesCollection.ReplaceOne(x => x.Id == id, updatedCategorie);

        public void Remove(string id) =>
             _CategoriesCollection.DeleteOne(x => x.Id == id);
    }
}

