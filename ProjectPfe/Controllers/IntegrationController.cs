using ConnexionMongo.Models;
using ConnexionMongo.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using ProjectPfe.Models;
using ProjectPfe.Services;
using ProjectPfe.Services.libs;
using System.Xml.Linq;
using System.Text;
using MongoDB.Driver.GridFS;
using System.Xml;

namespace ProjectPfe.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class IntegrationController : ControllerBase
    {


        private IntegrationService integrationService;
        private CategorieService categorieService;

        private readonly TitreService titreService;
        private readonly SousTitreService soustitreservice;
        private readonly ParagrapheService paragrapheService;
        private readonly SousParagrapheService sousparagrapheService;
        private readonly TableauService tableauService;
        private readonly LigneService ligneService;
        private readonly ColonneService colonneService;


        private readonly TemplateWordService templateService;
        private readonly GridFsStockTemplate gridFsStockTemplate;
        private readonly InputXmlService inputXmlService;



        public IntegrationController(CategorieService _categorieService, InputXmlService _inputXmlService, GridFsStockTemplate _gridFsStockTemplate, TemplateWordService _templateService, IntegrationService _integrationService, TitreService _titreService, SousTitreService _sousTitreService, ParagrapheService _paragrapheService, SousParagrapheService _sousParagrapheService, TableauService _tableauService, LigneService _ligneService, ColonneService _colonneService)
        {
            integrationService = _integrationService;
            gridFsStockTemplate = _gridFsStockTemplate;
            titreService = _titreService;
            paragrapheService = _paragrapheService;
            sousparagrapheService = _sousParagrapheService;
            tableauService = _tableauService;
            ligneService = _ligneService;
            colonneService = _colonneService;
            categorieService = _categorieService;

            templateService = _templateService;
            soustitreservice = _sousTitreService;
            inputXmlService = _inputXmlService;

        }


        [HttpGet(Name = "AllIntegrations")]
        [Route("AllIntegrations")]
        public List<Integration> AllIntegrations()
        {

            return integrationService.Get();
        }


        [HttpPost(Name = "AddIntegration")]
        [Route("AddIntegration/{idcategorie}")]
        public String AddIntegration([FromForm] IFormFile file, string idcategorie)
        {
            
            var objectIdFile = gridFsStockTemplate.UploadFileXml(file);
            Input input = new Input();
            input.IdChunks = objectIdFile.ToString();
            input.Filename = file.FileName;
            inputXmlService.Create(input);
            Categorie c = categorieService.Get(idcategorie);

            return addintegrationcommunuse(objectIdFile, c);


        }






       
        


        [HttpDelete(Name = "DeleteIntegration")]
        [Route("DeleteIntegration/{id}")]
        public String deleteIntegration(string id)
        {
            integrationService.Remove(id);
            return "Deleted";
        }


        [HttpPut(Name = "GetIntegration")]
        [Route("GetIntegration/{idIntegration}")]
        public Integration GetIntegration(string idIntegration)
        {
            Integration integration = integrationService.Get(idIntegration);



            return integration;

        }


        [HttpGet(Name = "GetFinalPdf")]
        [Route("GetFinalPdf/{idIntegration}")]
        public String GetFinalPdf(String idIntegration)
        {

            return gridFsStockTemplate.DownloadFileByName(idIntegration);

        }

        [HttpPut]
        [Route("EcrireTemplate/{idTemplateChoisi}")]
        public Integration EcrireTemplate(String idTemplateChoisi, Integration integration)
        {
            TemplateWord template = templateService.Get(idTemplateChoisi);


            List<Integration> integrations = new List<Integration>();
            integrations.Add(integration);
            GenerateWord generateWord = new GenerateWord();
            generateWord.GeneratWord(integrations, template, gridFsStockTemplate, integrationService);
            return integration;
        }

        [HttpPost(Name = "AddIntegrationbyidxmlfile")]
        [Route("AddIntegrationbyidxmlfile/{id}/{idcategorie}")]
        public String AddIntegrationbyidxmlfile(string id,string idcategorie )
        {

            var objectIdFile =new ObjectId( id);
            Categorie c = categorieService.Get(idcategorie);

            return addintegrationcommunuse(objectIdFile,c);

            

        }


#region privatefunction

        private string addintegrationcommunuse(ObjectId objectIdFile, Categorie categorie) {

          

            var doc = gridFsStockTemplate.openfile(objectIdFile);

            StreamReader sr = new StreamReader(doc, Encoding.Default);


            XDocument xDocument = XDocument.Load(sr);
            ImportElement.lowerElementsAttributes(xDocument);

            foreach (var xdoc in xDocument.Descendants("integration"))
            {
                Integration integration = new Integration();
                integration.inputfileid = objectIdFile.ToString();

                DictionnaireService.validate(xdoc, integration);


                integration.Nationalite = xdoc.Element("nationalite").Value;
                integration.Age = (xdoc.Element("age").Value);


                integration.Titres = new List<Titre>();
                integration.Paragraphes = new List<Paragraphe>();
                integration.Tableaux = new List<Tableau>();

                integration.Sex = DictionnaireIntegration.getSex(xdoc);
                integration.PrixUnitaire = DictionnaireIntegration.getPrixUnitaire(xdoc);
                integration.Adresse = DictionnaireIntegration.getAdresse(xdoc);
                integration.Created = DateTime.Now;
                integration.UserImport = UserConnected.user;
                integration.etatIntegration = EtatIntegration.Etape1;
                integration.statutIntegration = StatutIntegration.NonTermine;
                integration.categorie=categorie;
                integrationService.Create(integration);

                ImportElement.AddTitreToIntegration(xdoc, integration, titreService, soustitreservice);

                ImportElement.AddParagrapheToIntegration(xdoc, integration, paragrapheService, sousparagrapheService);
                ImportElement.AddTableToIntegration(xdoc, integration, tableauService, ligneService, colonneService);


                integrationService.Update(integration.Id, integration);


                GenerateXml generateXml = new GenerateXml();
                generateXml.generate(integration);

                return integration.Id;


            }
            return null;
        }
        [HttpGet(Name = "IntegrationStep1")]
        [Route("IntegrationStep1")]
        public List<Integration> IntegrationStep1()
        {
            List<Integration> integrations = integrationService.GetIntegrationStep1();
            return integrations;

        }

        #endregion


    }



}
