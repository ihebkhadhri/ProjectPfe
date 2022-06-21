using ConnexionMongo.Models;
using ProjectPfe.Models;

using System.Diagnostics;

using Aspose.Words;
using ProjectPfe.Services.libs;
using MongoDB.Bson;
using ConnexionMongo.Services;

namespace ProjectPfe.Services
{
    public class GenerateWord
    {
        public void GeneratWord(List<Integration> integrations, TemplateWord templateClass, GridFsStockTemplate gridFsStockTemplate, IntegrationService integrationService)
        {
          

            foreach (Integration integration in integrations)
            {

               ObjectId idrtf= gridFsStockTemplate.createRapport(templateClass.FileRtfId,integration);
                integration.rtfid = idrtf.ToString();
                

              Stream file=  gridFsStockTemplate.GetFile(idrtf);
                integration.pdfid = convertToPdf3(file, integration.Id, gridFsStockTemplate);
                integration.template = templateClass;
                integrationService.Update(integration.Id, integration);

            }


             

            string  convertToPdf3(Stream filetoconvert, String idIntegration, GridFsStockTemplate gridFsStockTemplate)
            {
                Spire.Doc.Document doc = new Spire.Doc.Document(filetoconvert);
                doc.SaveToFile(Directory.GetCurrentDirectory() + "/" + idIntegration + ".pdf", Spire.Doc.FileFormat.PDF);
               
              doc.Dispose();
                string idfile = "";

                using (Stream fileStream = new FileStream(Directory.GetCurrentDirectory() + "/" + idIntegration + ".pdf", FileMode.Open, FileAccess.Read))
                {
                   idfile= gridFsStockTemplate.AddFile(fileStream, idIntegration + ".pdf").ToString();
                    
                }

                File.Delete(Directory.GetCurrentDirectory()+"/" + idIntegration + ".pdf");
                doc.Close();
                return idfile;
            }
           

        }
    }
}
