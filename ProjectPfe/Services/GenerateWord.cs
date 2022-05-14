using ConnexionMongo.Models;
using System.Diagnostics;
namespace ProjectPfe.Services
{
    public class GenerateWord
    {
        public void GeneratWord(List<Integration> integrations)
        {
            string template = @"C:\Users\21655\Desktop\templateTest.rtf";
            foreach(Integration integration in integrations)
            {
                string docFile = template.Replace("templateTest.rtf", integration.Id + ".rtf");
                string templateContent = System.IO.File.ReadAllText(template);
                string userContent = templateContent.Replace("##Nom##", integration.Nom).Replace("##Nationalite##",integration.Nationalite)
                    .Replace("##prenom##", integration.Prenom).Replace("##Age##", integration.Age.ToString());
                System.IO.File.WriteAllText(docFile, userContent);
                ConvertPdf(docFile);
            }
             void ConvertPdf(string docFile)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.UseShellExecute = false;
                info.CreateNoWindow = true;
                info.FileName = "soffice.exe";
                info.Arguments = "--headless --convert-to pdf \"" + docFile + "\" outdir C:\\Users\\21655\\Desktop\\";
                Process process = new Process();
                process.StartInfo = info;
                process.Start();
                process.WaitForExit();
            }
        }
    }
}
