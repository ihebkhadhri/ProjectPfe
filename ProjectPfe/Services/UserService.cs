using ConnexionMongo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectPfe.Models;


namespace ProjectPfe.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _UsersCollection;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly UserManager<User> _userManager;

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
             _UsersCollection.Find(x => x.Username == email && x.Password == password).FirstOrDefault();

        public void Create(User newUser) =>
             _UsersCollection.InsertOne(newUser);

        public void Update(string id, User updatedUser) =>
             _UsersCollection.ReplaceOne(x => x.Id == id, updatedUser);

        public void Remove(string id) =>
             _UsersCollection.DeleteOne(x => x.Id == id);

        //public async Task<string> AddRoleAsync(AddRoleModel model)
        //{
        //    var user = await _userManager.FindByIdAsync(model.UserId);

        //    if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
        //        return "Invalid user ID or Role";

        //    if (await _userManager.IsInRoleAsync(user, model.Role))
        //        return "User already assigned to this role";

        //    var result = await _userManager.AddToRoleAsync(user, model.Role);

        //    return result.Succeeded ? string.Empty : "Sonething went wrong";

        //}
    }
}

