using Microsoft.AspNetCore.Mvc;
using ProjectPfe.Models;
using ProjectPfe.Services;

namespace ProjectPfe.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : Controller
    {
        private readonly TemplateWordService templateWordService;
        private readonly CategorieService categorieService;

        public TemplateController(TemplateWordService _templateWordService, CategorieService _categorieService) {
            templateWordService = _templateWordService;
            categorieService = _categorieService; 
        }

        [HttpGet(Name = "AllTemplatesByCategorie")]
        [Route("AllTemplatesByCategorie")]
        public List<TemplateWord> AllTemplatesByCategorie()
        {
            return templateWordService.Get();
        }

        [HttpPost(Name = "AddTemplate")]
        [Route("AddTemplate")]
        public List<TemplateWord> AddTemplate()
        {
          Categorie c=  categorieService.Get().FirstOrDefault();

            TemplateWord templateWord = new TemplateWord();
            templateWord.Creation_Date=DateTime.Now;
            templateWord.Nom = "a";
            templateWord.categorie = c;
            templateWordService.Create(templateWord);

            
            return templateWordService.Get();
        }
    }
}
