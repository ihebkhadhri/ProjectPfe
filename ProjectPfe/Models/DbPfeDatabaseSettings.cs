namespace ConnexionMongo.Models
{
    public class DbPfeDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string IntegrationCollectionName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
    }
}

