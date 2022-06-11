using Microsoft.AspNetCore.Mvc;
using ProjectPfe.Models;
using ProjectPfe.Services;
using ProjectPfe.Services.libs;

namespace ProjectPfe.Controllers
{
//    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : Controller
    {
        private readonly TemplateWordService templateWordService;
        private readonly CategorieService categorieService;
        private readonly GridFsStockTemplate gridFsStockTemplate;

        public TemplateController(TemplateWordService _templateWordService, CategorieService _categorieService, GridFsStockTemplate _gridFsStockTemplate) {
            templateWordService = _templateWordService;
            categorieService = _categorieService;
            gridFsStockTemplate = _gridFsStockTemplate; 
        }

        [HttpGet(Name = "AllTemplatesByCategorie")]
        [Route("AllTemplatesByCategorie")]
        public List<String> AllTemplatesByCategorie()
        {
            List<String> allTemplatespdf = new List<String>();
            var listemplates = templateWordService.Get();
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
        [Route("AddTemplate")]
        public List<TemplateWord> AddTemplate([FromForm] IFormFile[] files)
        {

            Categorie c = categorieService.Get().FirstOrDefault();

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
    }
}
