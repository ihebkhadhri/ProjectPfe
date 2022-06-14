using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;

namespace ProjectPfe.Services
{
    public class TemplateWordService
    {
        private readonly IMongoCollection<TemplateWord> _TemplateWordsCollection;

        public TemplateWordService(
            IOptions<DbPfeDatabaseSettings> TemplateWordStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                TemplateWordStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                TemplateWordStoreDatabaseSettings.Value.DatabaseName);

            _TemplateWordsCollection = mongoDatabase.GetCollection<TemplateWord>(
                TemplateWordStoreDatabaseSettings.Value.TemplateWordCollectionName);
        }

        public List<TemplateWord> Get() =>
             _TemplateWordsCollection.Find(_ => true).ToList();

        public TemplateWord Get(string id) =>
             _TemplateWordsCollection.Find(x => x.Id == id).FirstOrDefault();

        public List<TemplateWord> GetByCategorieId(string id) =>
            _TemplateWordsCollection.Find(x => x.categorie.Id == id).ToList();

        public void Create(TemplateWord newTemplateWord) =>
             _TemplateWordsCollection.InsertOne(newTemplateWord);

        public void Update(string id, TemplateWord updatedTemplateWord) =>
             _TemplateWordsCollection.ReplaceOne(x => x.Id == id, updatedTemplateWord);

        public void Remove(string id) =>
             _TemplateWordsCollection.DeleteOne(x => x.Id == id);
    }
}

