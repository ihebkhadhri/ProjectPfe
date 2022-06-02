using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;

namespace ProjectPfe.Services
{
    public class SousParagrapheService
    {

        private readonly IMongoCollection<Sous_paragraphe> _SousParagraphesCollection;

        public SousParagrapheService(
            IOptions<DbPfeDatabaseSettings> SousParagrapheStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                SousParagrapheStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SousParagrapheStoreDatabaseSettings.Value.DatabaseName);

            _SousParagraphesCollection = mongoDatabase.GetCollection<Sous_paragraphe>(
                SousParagrapheStoreDatabaseSettings.Value.Sous_ParagrapheCollectionName);
        }

        public List<Sous_paragraphe> Get() =>
             _SousParagraphesCollection.Find(_ => true).ToList();

        public Sous_paragraphe Get(string id) =>
             _SousParagraphesCollection.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Sous_paragraphe newSousParagraphe) =>
             _SousParagraphesCollection.InsertOne(newSousParagraphe);

        public void Update(string id, Sous_paragraphe updatedSousParagraphe) =>
             _SousParagraphesCollection.ReplaceOne(x => x.Id == id, updatedSousParagraphe);

        public void Remove(string id) =>
             _SousParagraphesCollection.DeleteOne(x => x.Id == id);
    }
}

