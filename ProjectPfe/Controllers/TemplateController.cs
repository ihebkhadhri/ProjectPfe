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

            TemplateWord templateWord2 = new TemplateWord();
            templateWord2.Creation_Date = DateTime.Now;
            templateWord2.Nom = "b";
            templateWord2.categorie = c;
            templateWordService.Create(templateWord2);

            TemplateWord templateWord3 = new TemplateWord();
            templateWord3.Creation_Date = DateTime.Now;
            templateWord3.Nom = "c";
            templateWord3.categorie = c;
            templateWordService.Create(templateWord3);

            return templateWordService.Get();
        }
    }
}
