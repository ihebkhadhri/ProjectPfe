using ConnexionMongo.Models;
using ProjectPfe.Models;
using System.Xml.Linq;

namespace ProjectPfe.Services.libs
{
    public class ImportElement

    {
        
        public static void AddTitreToIntegration(XElement coordinate ,Integration integration,TitreService
            titreService, SousTitreService soustitreService)
        {
            
            foreach (var titre in coordinate.Descendants("Titre"))
            {
                Titre t = new Titre();
                t.libelle = titre.Attribute("libelle").Value;
                t.integration = integration;
                titreService.Create(t);
                integration.Titres.Add(t);
                t.Sous_titres = new List<Sous_titre>();

                AddSousTitreToIntegration(titre, t, soustitreService);

            }
        }


        public static void AddSousTitreToIntegration(XElement titrecoordinate, Titre titre, SousTitreService soustitreService)
        {

            foreach (var soustitre in titrecoordinate.Descendants("Soustitre"))
            {    
                Sous_titre t = new Sous_titre();
                t.libelle = soustitre.Attribute("libelle").Value;
                t.titre = titre;
                soustitreService.Create(t);
                titre.Sous_titres.Add(t);

            }
        }
        public static void AddParagrapheToIntegration(XElement coordinate, Integration integration, ParagrapheService paragrapheService, SousParagrapheService sousparagrapheservice)
        {

            foreach (var paragraphe in coordinate.Descendants("Paragraphe"))
            {
                Paragraphe t = new Paragraphe();
                t.libelle = paragraphe.Attribute("libelle").Value;
                paragrapheService.Create(t);
                integration.Paragraphes.Add(t);
                t.Sous_paragraphe = new List<Sous_paragraphe>();
                AddSousParagrapheToIntegration(paragraphe, t, sousparagrapheservice);

            }
        }



        public static void AddSousParagrapheToIntegration(XElement paragraphecoordinate, Paragraphe paragraphe, SousParagrapheService sousparagrapheservice)
        {

            foreach (var sousparagraphe in paragraphecoordinate.Descendants("SousParagraphe"))
            {
                Sous_paragraphe t = new Sous_paragraphe();
                t.libelle = sousparagraphe.Attribute("libelle").Value;
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

            foreach (var ligne in tableaucoordinate.Descendants("Ligne"))
            {
                Ligne t = new Ligne();
               
                if(ligne.Attribute("libelle")!=null)
                    t.libelle = ligne.Attribute("libelle").Value;
                
                t.tableau = tableau;
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
