using System.Xml.Linq;

namespace ProjectPfe.Services.libs
{
    public class DictionnaireIntegration
    {
        private static Dictionary<string, List<string>> Dictionnaire = new Dictionary<string, List<string>>()
        {

            { "sex",remplirListSex() },
            {  "prixunitaire",remplirListPrixUnitaire() },
            {  "adress",remplirListAdress() }

        };

        private static List<string> remplirListSex()
        {
            List<string> list = new List<string>();
            list.Add("sex");
            list.Add("sexe");
            list.Add("sxe");
            list.Add("sx");
            
            return list;
            
        }

        private static List<string> remplirListPrixUnitaire()
        {
            List<string> list = new List<string>();
            list.Add("prixunitaire");
            list.Add("unitaireprix");
            list.Add("priceunitary");
            list.Add("prunitaire");

            return list;

        }

        private static List<string> remplirListAdress()
        {
            List<string> list = new List<string>();
            list.Add("adresse");
            list.Add("adress");
            list.Add("address");
            list.Add("adres");

            return list;

        }

        public static String? getSex(XElement xdoc)
        {
            foreach (var element in Dictionnaire)
            {
                if (element.Key == "sex")
                {
                    foreach(var item in element.Value)
                    {
                        if (xdoc.Attribute(item) != null)
                        {
                            return xdoc.Attribute(item).Value;
                        }
                        if (xdoc.Element (item) != null)
                        {
                            return xdoc.Element(item).Value;
                        }
                    }
                }
               
            }
            return null;
        }
        public static String? getPrixUnitaire(XElement xdoc)
        {
            foreach (var element in Dictionnaire)
            {
                if (element.Key == "prixunitaire")
                {
                    foreach(var item in element.Value)
                    {
                        if (xdoc.Attribute(item) != null)
                        {
                            return ( xdoc.Attribute(item).Value);
                        }
                        if (xdoc.Element(item) != null)
                        {
                            return (xdoc.Element(item).Value);
                        }
                    }
                }
               
            }
            return null;
        }

        public static String? getAdresse(XElement xdoc)
        {
            foreach (var element in Dictionnaire)
            {
                if (element.Key == "adress")
                {
                    foreach (var item in element.Value)
                    {
                        if (xdoc.Attribute(item) != null)
                        {
                            return (xdoc.Attribute(item).Value);
                        }
                        if (xdoc.Element(item) != null)
                        {
                            return (xdoc.Element(item).Value);
                        }
                    }
                }

            }
            return null;
        }
    }
}
