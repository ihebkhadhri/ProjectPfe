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



        public IntegrationController(InputXmlService _inputXmlService, GridFsStockTemplate _gridFsStockTemplate, TemplateWordService _templateService, IntegrationService _integrationService, TitreService _titreService, SousTitreService _sousTitreService, ParagrapheService _paragrapheService, SousParagrapheService _sousParagrapheService, TableauService _tableauService, LigneService _ligneService, ColonneService _colonneService)
        {
            integrationService = _integrationService;
            gridFsStockTemplate = _gridFsStockTemplate;
            titreService = _titreService;
            paragrapheService = _paragrapheService;
            sousparagrapheService = _sousParagrapheService;
            tableauService = _tableauService;
            ligneService = _ligneService;
            colonneService = _colonneService;

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
        [Route("AddIntegration")]
        public String AddIntegration([FromForm] IFormFile file)
        {
            List<Integration> integrations = new List<Integration>();


            var objectIdFile = gridFsStockTemplate.UploadFileXml(file);
            Input input = new Input();
            input.IdChunks = objectIdFile.ToString();
            input.Filename = file.FileName;
            inputXmlService.Create(input);
            var doc = gridFsStockTemplate.openfile(objectIdFile);

            StreamReader sr = new StreamReader(doc, Encoding.Default);


            XDocument xDocument = XDocument.Load(sr);


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
                integrationService.Create(integration);

                ImportElement.AddTitreToIntegration(xdoc, integration, titreService, soustitreservice);

                ImportElement.AddParagrapheToIntegration(xdoc, integration, paragrapheService, sousparagrapheService);
                ImportElement.AddTableToIntegration(xdoc, integration, tableauService, ligneService, colonneService);


                integrationService.Update(integration.Id, integration);

                integrations.Add(integration);
                GenerateXml generateXml = new GenerateXml();
                generateXml.generate(integrations);

                return integration.Id;


            }

            return null;
        }






        [HttpPut(Name = "UpdateIntegration")]
        [Route("UpdateIntegration")]
        public List<Integration> UpdateIntegration()
        {
            XDocument coordinates = XDocument.Load(@"D:\testUpdate.xml");

            List<Integration> integrations = new List<Integration>();

            foreach (var coordinate in coordinates.Descendants("integration"))
            {

                Integration integration = integrationService.Get(coordinate.Attribute("id").Value);

                integration.Nom = coordinate.Attribute("nom").Value;
                integration.Prenom = coordinate.Element("prenom").Value;
                integration.Nationalite = coordinate.Element("nationalite").Value;
                integration.Age = (coordinate.Element("age").Value);

                integrationService.Update(integration.Id, integration);
                integrations.Add(integration);



            }

            GenerateXml generateXml = new GenerateXml();
            generateXml.generate(integrations);

            return integrations;
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
        public Integration EcrireTemplate( String idTemplateChoisi, Integration integration)
        {
            TemplateWord template = templateService.Get(idTemplateChoisi);

           
            List<Integration> integrations = new List<Integration>();
            integrations.Add(integration);
            GenerateWord generateWord = new GenerateWord();
            generateWord.GeneratWord(integrations, template, gridFsStockTemplate, integrationService);
            return integration;
        }

        

    }



}
