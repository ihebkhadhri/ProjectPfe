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

        public TemplateController(TemplateWordService _templateWordService) => templateWordService = _templateWordService;

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
            TemplateWord templateWord = new TemplateWord();
            templateWord.Creation_Date=DateTime.Now;
            templateWord.Nom = "c";
            templateWordService.Create(templateWord);
            return templateWordService.Get();
        }
    }
}
