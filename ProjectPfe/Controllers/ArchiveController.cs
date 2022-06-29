using ConnexionMongo.Models;
using ConnexionMongo.Services;
using Microsoft.AspNetCore.Mvc;
using ProjectPfe.Models;
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

        //back


        [HttpGet(Name = "AllArchives")]
        [Route("AllArchives")]
        public List<Integration> AllArchives()
        {

            List<Integration> integrations = integrationService.Get();

            return integrations;

        }


        [HttpDelete(Name = "Deletearchive")]
        [Route("Deletearchive/{idintegration}/")]
        public void Deletearchive(string idintegration)
        {
            var integration = integrationService.Get(idintegration);
            gridFsStockTemplate.removeFile(integration.rtfid);
            gridFsStockTemplate.removeFile(integration.pdfid);
            gridFsStockTemplate.removeFile(integration.inputfileid);

            integrationService.Remove(idintegration);



        }

        [HttpGet(Name = "Downloadarchive")]
        [Route("Downloadarchive/{idintegration}/")]
        public string Downloadarchive(string idintegration)
        {
            var integration = integrationService.Get(idintegration);
            return gridFsStockTemplate.findarchive(integration.pdfid);



        }
        [HttpGet(Name = "Downloadinput")]
        [Route("Downloadinput/{idintegration}/")]
        public string Downloadinput(string idintegration)
        {
            var integration = integrationService.Get(idintegration);
            return gridFsStockTemplate.findainput(integration.inputfileid);


        }


        [HttpGet(Name = "AllArchivesStep3Admin")]
        [Route("AllArchivesStep3Admin")]
        public List<Integration> AllArchivesStep3Admin()
        {

            List<Integration> integrations = integrationService.GetArchiveStep3GroupCorilus();

            return integrations;

        }

        //front


        [HttpGet(Name = "AllArchivesbyuser")]
        [Route("AllArchivesbyuser")]
        public List<Integration> AllArchivesbyuser()
        {

            List<Integration> integrations = integrationService.Getbyuser(UserConnected.user.Id);

            return integrations;

        }

        [HttpGet(Name = "AllArchivesbyuserstep1")]
        [Route("AllArchivesbyuserstep1")]
        public List<Integration> AllArchivesbyuserstep1()
        {

            List<Integration> integrations = integrationService.Getarchivebystep1byuser(UserConnected.user.Id);

            return integrations;

        }


        [HttpGet(Name = "MesArchivesPdf")]
        [Route("MesArchivesPdf/{step}")]
        public List<String> MesArchivesPdf(int step)
        {
            List<String> allarchives = new List<String>();
            List<Integration> integrations = new List<Integration>();
            if (step == 2)
            {
                integrations=integrationService.GetArchiveStep2ByUser();
            }
            else
            {
                integrations= integrationService.Getbyuser(UserConnected.user.Id);
            }
            foreach (var t in integrations)
            {
                allarchives.Add(gridFsStockTemplate.DownloadFile(t.pdfid));
            }

            return allarchives;

        }

        [HttpGet(Name = "MesArchivesPdfFiltrer")]
        [Route("MesArchivesPdfFiltrer/{filtresearch}/{step}")]
        public List<String> MesArchivesPdfFiltrer(String filtresearch, int step)
        {
            List<String> allarchives = new List<String>();
            List<Integration> integrations = new List<Integration>();
            if (step == 2)
            {
                integrations= integrationService.GetArchiveStep2ByUser();
            }
            else
            {
                integrations= integrationService.Getbyuser(UserConnected.user.Id);
            }
            String _filtresearch = filtresearch.ToLower();

            if (_filtresearch == "")
            {
                foreach (var t in integrations)
                {
                    allarchives.Add(gridFsStockTemplate.DownloadFile(t.pdfid));
                }

                return allarchives;
            }



            foreach (var t in integrations)
            {
                var titres = t.Titres.Where(tr => tr.libelle.ToLower().Contains(_filtresearch)).ToList();
                var paragrapges = t.Paragraphes.Where(tr => tr.libelle.ToLower().Contains(_filtresearch)).ToList();

                var soustitres = t.Titres.Where(tr => tr.Sous_titres.Where(st => st.libelle.ToLower().Contains(_filtresearch)).ToList().Count > 0).ToList();
                var sousparagraphe = t.Paragraphes.Where(tr => tr.Sous_paragraphe.Where(st => st.libelle.ToLower().Contains(_filtresearch)).ToList().Count > 0).ToList();


                if (soustitres.Count > 0 || sousparagraphe.Count > 0 || titres.Count > 0 || paragrapges.Count > 0 || t.Adresse.ToLower().Contains(_filtresearch) || t.Age.ToLower().Contains(_filtresearch) || t.DateNaissance.ToLower().Contains(_filtresearch) || t.Nationalite.ToLower().Contains(_filtresearch) || t.Nom.ToLower().Contains(_filtresearch) || t.Prenom.ToLower().Contains(_filtresearch) || t.PrixUnitaire.ToLower().Contains(_filtresearch) || t.Sex.ToLower().Contains(_filtresearch))
                {


                    allarchives.Add(gridFsStockTemplate.DownloadFile(t.pdfid));

                }

            }

            return allarchives;

        }


        [HttpGet(Name = "getidinputfilefromintegration")]
        [Route("getidinputfilefromintegration/{idintegration}/")]
        public string getidinputfilefromintegration(string idintegration)
        {
            var integration = integrationService.Get(idintegration);
            return integration.inputfileid;


        }

        //front 
        [HttpGet(Name = "AllArchivesStep2")]
        [Route("AllArchivesStep2")]
        public List<Integration> AllArchivesStep2()
        {

            List<Integration> integrations = integrationService.GetArchiveStep2ByUser();

            return integrations;

        }
        [HttpGet(Name = "AllArchivesStep3")]
        [Route("AllArchivesStep3")]
        public List<Integration> AllArchivesStep3()
        {

            List<Integration> integrations = integrationService.GetArchiveStep3ByUser();

            return integrations;

        }


    }
}
