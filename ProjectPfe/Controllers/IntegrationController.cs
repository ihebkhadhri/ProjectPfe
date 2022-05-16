using ConnexionMongo.Models;
using ConnexionMongo.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using ProjectPfe.Models;
using ProjectPfe.Services;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ProjectPfe.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class IntegrationController : ControllerBase
    {


        private readonly IntegrationService integrationService;
        private readonly TitreService titreService;
        private readonly ParagrapheService paragrapheService;



        public IntegrationController(IntegrationService _integrationService, TitreService _titreService, ParagrapheService _paragrapheService) { integrationService = _integrationService;
            titreService = _titreService;
            paragrapheService = _paragrapheService;
                }


        [HttpGet(Name = "AllIntegrations")]
        [Route("AllIntegrations")]
        public List<Integration> AllIntegrations()
        {
            return integrationService.Get();
        }


        [HttpPost(Name = "AddIntegration")]
        [Route("AddIntegration")]
        public List<Integration> AddIntegration()
        {
            XDocument coordinates = XDocument.Load(@"C:\Users\lulus\Documents\Stage\pfe\ProjectPfe\test.xml");

            List<Integration> integrations = new List<Integration>();

            foreach (var coordinate in coordinates.Descendants("integration"))
            {
                Integration integration = new Integration();
                integration.Nom = coordinate.Attribute("nom").Value;

                integration.Prenom = coordinate.Element("prenom").Value;
                integration.Nationalite = coordinate.Element("nationalite").Value;
                integration.Age = double.Parse(coordinate.Element("age").Value);
                integration.Titres = new List<Titre>();
                integration.Paragraphes = new List<Paragraphe>();

                foreach (var titre in coordinate.Descendants("Titres"))
                {
                   Titre t=new Titre();
                    t.libelle = titre.Element("Titre").Value;
                    titreService.Create(t);
                    integration.Titres.Add(t); 

                }
                  
                foreach (var paragraphe in coordinate.Descendants("Paragraphes"))
                {
                    Paragraphe t = new  Paragraphe();
                    t.libelle = paragraphe.Element("Paragraphe").Value;
                    paragrapheService.Create(t);
                    integration.Paragraphes.Add(t);

                }




                integrationService.Create(integration);
                integrations.Add(integration);



            }

            GenerateXml generateXml = new GenerateXml();
            generateXml.generate(integrations);

            return integrations;
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
                integration.Age = double.Parse(coordinate.Element("age").Value);

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
        public List<Integration> GetIntegrations(string idIntegration)
        {
          Integration integration=  integrationService.Get( idIntegration);
            
            List<Integration> integrations = new List<Integration>();
            integrations.Add(integration);
            GenerateWord generateWord = new GenerateWord();
            generateWord.GeneratWord(integrations);
            return integrations;

        }
        
    }
}
