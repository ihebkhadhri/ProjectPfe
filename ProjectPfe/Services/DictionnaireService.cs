using ConnexionMongo.Models;
using System.Xml.Linq;

namespace ProjectPfe.Services
{
	public class DictionnaireService
	{
		public static void validate(XElement xdoc, Integration integration)
		{
			List<String> Noms = new List<string>() { "name", "nomm", "nnom", "noom" };
			List<String> Prenoms = new List<string>() { "lastname", "prenomm", "prenom" };
			List<String> Dates = new List<string>() { "daate", "datenaissance", "dt" };
			var dictionnaire = new Dictionary<string, List<string>>(){
			{"Nom",Noms},
			{"Prenom",Prenoms},
			{"Date",Dates}
		};


			foreach (var kvp in dictionnaire)
			{
				if (kvp.Key == "Nom")
				{

					foreach (var item in kvp.Value)
					{
						if (xdoc.Attribute(item) != null)
						{
							integration.Nom = xdoc.Attribute(item).Value;
						} if (xdoc.Element(item) != null)
                        {
							integration.Nom = xdoc.Element(item).Value;
						}

					}
				}

					if (kvp.Key == "Prenom")
					{

						foreach (var item in kvp.Value)
						{
						if (xdoc.Attribute(item) != null)
						{
							integration.Prenom = xdoc.Attribute(item).Value;
						}
						if (xdoc.Element(item) != null)
						{
							integration.Prenom = xdoc.Element(item).Value;
						}

					}
					}

					if (kvp.Key == "Date")
					{

						foreach (var item in kvp.Value)
						{
						if (xdoc.Attribute(item) != null)
						{
							integration.DateNaissance = xdoc.Attribute(item).Value;
						}
						if (xdoc.Element(item) != null)
						{
							integration.DateNaissance =  xdoc.Element(item).Value;
						}

					}
					}





				





			}
		}
	}
}
