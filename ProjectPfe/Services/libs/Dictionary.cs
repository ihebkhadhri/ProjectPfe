//using ConnexionMongo.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Text;
//using System.Xml.Linq;

//namespace ProjectPfe.Services.libs
//{
//    public class Dictionary
//    {
//        private readonly GridFsStockTemplate gridFsStockTemplate;
//        private readonly InputXmlService inputXmlService;
//        public string AgeDic([FromForm] IFormFile file)
//        {

//            List<Integration> integrations = new List<Integration>();

//            var objectIdFile = gridFsStockTemplate.UploadFileXml(file);

//            var doc = gridFsStockTemplate.openfile(objectIdFile);

//            StreamReader sr = new StreamReader(doc, Encoding.Default);


//            XDocument xDocument = XDocument.Load(sr);
//            foreach (var xdoc in xDocument.Descendants("integration"))
//            {
//                XElement po = XElement.Load(sr);
//                IEnumerable<XElement> childElements = from el in po.Elements() select el;
//            }

                
//            Dictionary<String, List<string>> dic = new Dictionary<string, List<string>>();
//            List<string> li1 = new List<string>();
//            var n = xDocument.Element("nom").Value;
//            li1.Add(n);
//            dic["nom"] = li1;
//            foreach (string i in li1)
//                if (i.Contains("ag") ^ i.Contains("Ag"))
//                {
//                    Integration integration = new Integration();
//                    integration.Nom = dic["nom"];
//                }

//            return null;

//        }
//    }
//}
