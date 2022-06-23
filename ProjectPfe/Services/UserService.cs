using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;

namespace ProjectPfe.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _UsersCollection;

        public UserService(
            IOptions<DbPfeDatabaseSettings> UserStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                UserStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                UserStoreDatabaseSettings.Value.DatabaseName);

            _UsersCollection = mongoDatabase.GetCollection<User>(
                UserStoreDatabaseSettings.Value.UserCollectionName);
        }

        public List<User> Get() =>
             _UsersCollection.Find(_ => true).ToList();

        public User Get(string id) =>
             _UsersCollection.Find(x => x.Id == id).FirstOrDefault();

        public User GetByEmailPassword(string email, string password) =>
             _UsersCollection.Find(x => x.Username == email && x.Password == password && x.validate==true).FirstOrDefault();

        public void Create(User newUser) =>
             _UsersCollection.InsertOne(newUser);

        public void Update(string id, User updatedUser) =>
             _UsersCollection.ReplaceOne(x => x.Id == id, updatedUser);

        public void Remove(string id) =>
             _UsersCollection.DeleteOne(x => x.Id == id);

        public List<User> Getgroup() =>
             _UsersCollection.Find(x => x.UserRole == UserRole.Utilisateur && x.validate==false).ToList();

        
    }
}

