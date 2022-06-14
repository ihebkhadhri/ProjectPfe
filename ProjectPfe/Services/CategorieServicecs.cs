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

        public async Task<List<Categorie>> GetAllAsync()
        {
            return await _CategoriesCollection.Find(c => true).ToListAsync();
        }

        public async Task<Categorie> GetByIdAsync(string id)
        {
            return await _CategoriesCollection.Find<Categorie>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateeAsync(Categorie categorie) =>
       await _CategoriesCollection.InsertOneAsync(categorie);

        public async Task<Categorie> CreateAsync(Categorie categorie)
        {
            await _CategoriesCollection.InsertOneAsync(categorie);
            return categorie;
        }
        public async Task UpdateAsync(string id, Categorie categorie)
        {
            await _CategoriesCollection.ReplaceOneAsync(c => c.Id == id, categorie);
        }
        public async Task DeleteAsync(string id)
        {
            await _CategoriesCollection.DeleteOneAsync(c => c.Id == id);
        }
    }
}

