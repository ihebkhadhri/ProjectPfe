using ConnexionMongo.Models;
using System.Xml;
using System.Xml.Serialization;

namespace ProjectPfe.Services
{
    public class GenerateXml
    {
        public void generate(Integration integration)
        {
            //Decalre a new XMLDocument object
            XmlDocument doc = new XmlDocument();

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            //create the root element
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //string.Empty makes cleaner code
            XmlElement elementRacine = doc.CreateElement(string.Empty, "Integrations", string.Empty);
            doc.AppendChild(elementRacine);


           

                XmlElement elementintegration = doc.CreateElement(string.Empty, "Integration", string.Empty);

                elementRacine.AppendChild(elementintegration);


                XmlElement elementNom = doc.CreateElement(string.Empty, "Nom", string.Empty);
                XmlText textNom = doc.CreateTextNode(integration.Nom);
                affecter(elementintegration, elementNom, textNom);

                XmlElement elementPrenom = doc.CreateElement(string.Empty, "Prenom", string.Empty);
                XmlText textPrenom = doc.CreateTextNode(integration.Prenom);
                affecter(elementintegration, elementPrenom, textPrenom);

                XmlElement elementAge = doc.CreateElement(string.Empty, "Age", string.Empty);
                XmlText textAge = doc.CreateTextNode(integration.Age.ToString());
                affecter(elementintegration, elementAge, textAge);

                XmlElement elementNationalite = doc.CreateElement(string.Empty, "Nationalite", string.Empty);
                XmlText textNationalite = doc.CreateTextNode(integration.Nationalite);
                affecter(elementintegration, elementNationalite, textNationalite);

            



            doc.Save(Directory.GetCurrentDirectory() + "//documentgenrate.xml");
            doc.Save(Console.Out);
        }

        public void affecter(XmlElement parent, XmlElement fils,  XmlText text)
        {

            parent.AppendChild(fils);
            fils.AppendChild(text);
        }

        //public string ToXML()
        //{
        //    using (var stringwriter = new System.IO.StringWriter())
        //    {
        //        var serializer = new XmlSerializer(this.GetType());
        //        serializer.Serialize(stringwriter, this);
        //        return stringwriter.ToString();
        //    }
        //}



        //Output: Convertion Integration class to Xml string
        public static Integration LoadFromXMLString(string xmlText)
        {
            using (var stringReader = new System.IO.StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(Integration));
                return serializer.Deserialize(stringReader) as Integration;
            }
        }
    }
}
