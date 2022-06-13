using ConnexionMongo.Models;
using ConnexionMongo.Services;
using Microsoft.AspNetCore.Mvc;
using ProjectPfe.Services.libs;

namespace ProjectPfe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArchiveController : ControllerBase
    {

        private readonly IntegrationService integrationService;
        private readonly GridFsStockTemplate gridFsStockTemplate;
        public ArchiveController(IntegrationService _integrationService, GridFsStockTemplate _gridFsStockTemplate)
        {
            integrationService = _integrationService;
            gridFsStockTemplate = _gridFsStockTemplate;
            
        }


        [HttpGet(Name = "AllArchives")]
        [Route("AllArchives")]
        public List<Integration> AllArchives()
        {
           
            List<Integration>integrations=integrationService.Get();
            
            return integrations;

        }


        [HttpDelete(Name = "Deletearchive")]
        [Route("Deletearchive/{idintegration}/")]
        public void Deletearchive(string idintegration)
        {
           var integration =integrationService.Get(idintegration);
            gridFsStockTemplate.removeFile(integration.rtfid );
            gridFsStockTemplate.removeFile(integration.pdfid );
            gridFsStockTemplate.removeFile(integration.inputfileid );

            integrationService.Remove(idintegration);
            
           

        }

        [HttpGet(Name = "Downloadarchive")]
        [Route("Downloadarchive/{idintegration}/")]
        public string Downloadarchive(string idintegration)
        {
            var integration = integrationService.Get(idintegration);
           return gridFsStockTemplate.findarchive(integration.pdfid);
            


            



        }

    }
}
