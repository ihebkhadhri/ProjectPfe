using ConnexionMongo.Models;
using ProjectPfe.Models;
using System.Xml.Linq;

namespace ProjectPfe.Services.libs
{
    public class ImportElement

    {

        public static void lowerElementsAttributes(XDocument xDocument)
        {
            var xelement = xDocument.Descendants("integration").Elements().ToList();
            foreach (var element in xelement)
            {
                element.Name = element.Name.ToString().ToLower();
            }

            var xattributesOld = xDocument.Descendants("integration").Attributes().ToList();
            List<XAttribute> attributesLowers = new List<XAttribute>();   
           
            foreach (var attribute in xattributesOld)
            {
                XAttribute xAttribute=new XAttribute(attribute.Name.ToString().ToLower(), attribute.Value);
                attributesLowers.Add(xAttribute);

            }
            xDocument.Descendants("integration").Attributes().Remove();
            xattributesOld.Clear();

            foreach (var attribute in attributesLowers)
            {
                xattributesOld.Add(attribute);
            }
            xDocument.Descendants("integration").ElementAt(0).Add(xattributesOld);


        }

        public static void AddTitreToIntegration(XElement coordinate ,Integration integration,TitreService
            titreService, SousTitreService soustitreService)
        {
            //incrémenter les titres
            int i = 0;
            foreach (var titre in coordinate.Descendants("Titre"))
            {
                //ordre titre (incrémentation)
                i=i+1;

                Titre t = new Titre();
                t.libelle = titre.Attribute("libelle").Value;
                t.order = i;
                t.integration = integration;
                titreService.Create(t);
                integration.Titres.Add(t);
                t.Sous_titres = new List<Sous_titre>();

                AddSousTitreToIntegration(titre, t, soustitreService);

            }
        }


        public static void AddSousTitreToIntegration(XElement titrecoordinate, Titre titre, SousTitreService soustitreService)
        {
            //incrémenter les sous  titres
            int i = 0;
            foreach (var soustitre in titrecoordinate.Descendants("Soustitre"))
            {    
                i=i + 1;
                Sous_titre t = new Sous_titre();
                t.libelle = soustitre.Attribute("libelle").Value;
                t.order=i;
                t.titre = titre;
                soustitreService.Create(t);
                titre.Sous_titres.Add(t);

            }
        }
        public static void AddParagrapheToIntegration(XElement coordinate, Integration integration, ParagrapheService paragrapheService, SousParagrapheService sousparagrapheservice)
        {
            //incrémenter les sous  titres
            int i = 0;
            foreach (var paragraphe in coordinate.Descendants("Paragraphe"))
            {
                i= i + 1;
                Paragraphe t = new Paragraphe();
                t.libelle = paragraphe.Attribute("libelle").Value;
                t.order= i;
                paragrapheService.Create(t);
                integration.Paragraphes.Add(t);
                t.Sous_paragraphe = new List<Sous_paragraphe>();
                AddSousParagrapheToIntegration(paragraphe, t, sousparagrapheservice);

            }
        }



        public static void AddSousParagrapheToIntegration(XElement paragraphecoordinate, Paragraphe paragraphe, SousParagrapheService sousparagrapheservice)
        {
            int i= 0;
            foreach (var sousparagraphe in paragraphecoordinate.Descendants("SousParagraphe"))
            {
                i= i + 1;
                Sous_paragraphe t = new Sous_paragraphe();
                t.libelle = sousparagraphe.Attribute("libelle").Value;
                t.order = i;
                t.paragraphe = paragraphe;
                sousparagrapheservice.Create(t);
                paragraphe.Sous_paragraphe.Add(t);

            }
        }

        public static void AddTableToIntegration(XElement coordinate, Integration integration, TableauService tableauservice, LigneService ligneService, ColonneService colonneService)
        {

            foreach (var tableau in coordinate.Descendants("Tableau"))
            {
                Tableau t = new Tableau();
                t.libelle = tableau.Attribute("libelle").Value;
                tableauservice.Create(t);
                integration.Tableaux.Add(t);
                t.lignes = new List<Ligne>();
                AddLigneToTableau(tableau, t, ligneService, colonneService);

            }
        }

        public static void AddLigneToTableau(XElement tableaucoordinate, Tableau tableau, LigneService ligneService, ColonneService colonneService)
        {
            int i=0;
            foreach (var ligne in tableaucoordinate.Descendants("Ligne"))
            {
                i=  i+1;
                Ligne t = new Ligne();
               
                if(ligne.Attribute("libelle")!=null)
                    t.libelle = ligne.Attribute("libelle").Value;
                
                t.tableau = tableau;
                t.order = i;
                ligneService.Create(t);
                tableau.lignes.Add(t);
                t.colonnes = new List<Colonne>();
                AddColonneToLigne(ligne, t, colonneService);

            }
        }

        public static void AddColonneToLigne(XElement lignecoordinate, Ligne ligne, ColonneService colonneService)
        {

            foreach (var colonne in lignecoordinate.Descendants("Colonne"))
            {
                Colonne t = new Colonne();
                t.libelle = colonne.Attribute("libelle").Value;
                t.ligne = ligne;
                colonneService.Create(t);
                ligne.colonnes.Add(t);

            }
        }

    }
}
