using ConnexionMongo.Models;
using ProjectPfe.Models;

using System.Diagnostics;

using Aspose.Words;

namespace ProjectPfe.Services
{
    public class GenerateWord
    {
        public void GeneratWord(List<Integration> integrations, TemplateWord templateClass)
        {
           String original= @"D:\pfe\Front\mon-app\src\Templates Word_pdf\" + templateClass.Nom + ".rtf";

            
            foreach(Integration integration in integrations)
            {
                string template = @"D:\pfe\Front\mon-app\src\Templates Word_pdf\" + integration.Id + ".rtf";

                createnewFile(original, template);

                string docFile = template;
                string templateContent = System.IO.File.ReadAllText(template);
                string userContent = templateContent.Replace("##Nom##", integration.Nom).Replace("##Nationalite##",integration.Nationalite)
                    .Replace("##prenom##", integration.Prenom).Replace("##Age##", integration.Age.ToString());
                System.IO.File.WriteAllText(docFile, userContent);
                // ConvertPdf(docFile); 
                convertToPdf3(template, integration.Id);
            }
             void ConvertPdf(string docFile)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.UseShellExecute = false;
                info.CreateNoWindow = true;
                info.FileName = "soffice.exe";
                info.Arguments = "--headless --convert-to pdf \"" + docFile + "\" outdir D:\\pfe\\Front\\mon-app\\src\\Templates Word_pdf";
                Process process = new Process();
                process.StartInfo = info;
                process.Start();
                process.WaitForExit();
            }


            void createnewFile(string racine, string dest){
                string sourceFile = racine;
                string destinationFile = dest;
                try
                {
                    File.Copy(sourceFile, destinationFile, true);
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }
            }

            String convertToPdf2(String filetoconvert,String idIntegration) {

                var doc = new Document(filetoconvert);
                    // Save as PDF
                    doc.Save(@"D:\pfe\Front\mon-app\src\Templates Word_pdf\" + idIntegration + ".pdf");

                return null;
            }

            void convertToPdf3(String filetoconvert, String idIntegration)
            {
                Spire.Doc.Document doc = new Spire.Doc.Document(filetoconvert);
                doc.SaveToFile(@"D:\pfe\Front\mon-app\src\Templates Word_pdf\" + idIntegration + ".pdf", Spire.Doc.FileFormat.PDF);
                doc.Dispose();
            }
        }
    }
}
