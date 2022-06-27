using ConnexionMongo.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProjectPfe.Models;
using ProjectPfe.Services;
using ProjectPfe.Services.libs;

namespace ProjectPfe.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TemplateController : Controller
    {
        private readonly TemplateWordService templateWordService;
        private readonly CategorieService categorieService;
        private readonly GridFsStockTemplate gridFsStockTemplate;
        private readonly IntegrationService integrationService;

        public TemplateController(IntegrationService _integrationService,TemplateWordService _templateWordService, CategorieService _categorieService, GridFsStockTemplate _gridFsStockTemplate) {
            templateWordService = _templateWordService;
            categorieService = _categorieService;
            gridFsStockTemplate = _gridFsStockTemplate;
            integrationService = _integrationService;
        }

        [HttpGet(Name = "AllTemplatesByCategorie")]
        [Route("AllTemplatesByCategorie/{idcat}")]
        public List<String> AllTemplatesByCategorie(string idcat)
        {
            List<String> allTemplatespdf = new List<String>();
            var listemplates = templateWordService.GetByCategorieId(idcat);
            foreach(var t in listemplates)
            {
                allTemplatespdf.Add(t.Id+"-*-"+ gridFsStockTemplate.DownloadFile(t.FilePdfId));
            }
           
            return allTemplatespdf;

        }

        [HttpGet(Name = "AdminAllTemplatesByCategorie")]
        [Route("AdminAllTemplatesByCategorie")]
        public List<TemplateWord> AdminAllTemplatesByCategorie()
        {
            
            return templateWordService.Get();
           

        }

        [HttpPost(Name = "AddTemplate")]
        [Route("AddTemplate/{idcategorie}")]
        public List<TemplateWord> AddTemplate(string idcategorie, [FromForm] IFormFile[] files)
        {

            Categorie c = categorieService.Get(idcategorie);

            TemplateWord templateWord = new TemplateWord();
            templateWord.Creation_Date = DateTime.Now;
           
            templateWord.categorie = c;


            foreach (var file in files)
            {
               
                var objectIdFile = gridFsStockTemplate.UploadFile(file);

                templateWord.Nom = "Template_" + file.FileName.Replace(".pdf", "").Replace(".rtf","");

                if (file.FileName.Contains(".rtf"))
                templateWord.FileRtfId = objectIdFile.ToString();
                else
                    templateWord.FilePdfId = objectIdFile.ToString();

                
            }
            templateWordService.Create(templateWord);

            return templateWordService.Get();
        }

        [HttpPost(Name = "DownloadTemplate")]
        [Route("DownloadTemplate/{id}")]
        public string DownloadTemplate(String id)
        {
          TemplateWord template=  templateWordService.Get(id);

            var res= gridFsStockTemplate.DownloadFile(template.FilePdfId);

            return res;
            
        }

        [HttpPost(Name = "removeTemplate")]
        [Route("removeTemplate/{id}")]
        public void removeTemplate(String id)
        {
            TemplateWord template = templateWordService.Get(id);

             gridFsStockTemplate.removefile(new ObjectId( template.FilePdfId));
             gridFsStockTemplate.removefile(new ObjectId(template.FileRtfId));

            templateWordService.Remove(template.Id);

        }
        [HttpGet(Name = "DownloadTemplateMappee")]
        [Route("DownloadTemplateMappee/{idi}/")]
        public string DownloadTemplateMappee(string idi)
        {
            var integration = integrationService.Get(idi);
            return gridFsStockTemplate.findarchive(integration.rtfid);



        }
    }
}
