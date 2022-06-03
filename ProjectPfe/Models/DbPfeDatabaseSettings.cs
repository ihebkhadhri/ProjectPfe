namespace ConnexionMongo.Models
{
    public class DbPfeDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string IntegrationCollectionName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
        public string CategorieCollectionName { get; set; } = null!;
        public string TableauCollectionName { get; set; } = null!;
        public string LigneCollectionName { get; set; } = null!;
        public string ColonneCollectionName { get; set; } = null!;
        public string TitreCollectionName { get; set; } = null!;
        public string Sous_TitreCollectionName { get; set; } = null!;
        public string ParagrapheCollectionName { get; set; } = null!;
        public string Sous_ParagrapheCollectionName { get; set; } = null!;
        public string TemplateWordCollectionName { get; set; } = null!;
        public string InputCollectionName { get; set; } = null!;
        public string OutputCollectionName { get; set; } = null!;
    }
}

