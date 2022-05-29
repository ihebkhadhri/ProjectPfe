using ConnexionMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace ProjectPfe.Services.libs

{
    public class GridFsStockTemplate
    {
        

        public GridFsStockTemplate(
            IOptions<DbPfeDatabaseSettings> DatabaseSettings)
        {
            var mongoClient = new MongoClient(
                DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DatabaseSettings.Value.DatabaseName);

            var fs = new GridFSBucket(mongoDatabase);
        }
    }
}
