using ConnexionMongo.Models;
using ProjectPfe.Models;

using System.Diagnostics;

using Aspose.Words;
using ProjectPfe.Services.libs;
using MongoDB.Bson;

namespace ProjectPfe.Services
{
    public class GenerateWord
    {
        public void GeneratWord(List<Integration> integrations, TemplateWord templateClass, GridFsStockTemplate gridFsStockTemplate)
        {
          

            foreach (Integration integration in integrations)
            {

               ObjectId idrtf= gridFsStockTemplate.createRapport(templateClass.FileRtfId,integration);
              Stream file=  gridFsStockTemplate.GetFile(idrtf);
                convertToPdf3(file, integration.Id, gridFsStockTemplate);


            }


             

            void convertToPdf3(Stream filetoconvert, String idIntegration, GridFsStockTemplate gridFsStockTemplate)
            {
                Spire.Doc.Document doc = new Spire.Doc.Document(filetoconvert);
                doc.SaveToFile(Directory.GetCurrentDirectory() + "/" + idIntegration + ".pdf", Spire.Doc.FileFormat.PDF);
               
              doc.Dispose();

                using (Stream fileStream = new FileStream(Directory.GetCurrentDirectory() + "/" + idIntegration + ".pdf", FileMode.Open, FileAccess.Read))
                {
                    gridFsStockTemplate.AddFile(fileStream, idIntegration + ".pdf");
                }

                File.Delete(Directory.GetCurrentDirectory()+"/" + idIntegration + ".pdf");

            }
        }
    }
}
