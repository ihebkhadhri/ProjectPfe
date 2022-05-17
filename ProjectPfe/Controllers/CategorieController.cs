using ConnexionMongo.Models;
using ConnexionMongo.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using ProjectPfe.Models;
using ProjectPfe.Services;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ProjectPfe.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[Controller]")]
    public class CategorieController : ControllerBase
    {
        private readonly CategorieService categorieService;
        public CategorieController(CategorieService _categorieService) => categorieService = _categorieService;

        [HttpPut(Name = "GetCategorie")]
        [Route("GetCategorie")]
        public List<Categorie> GetCategorie()
        {

            

            
           
            return categorieService.Get(); 

        }
    }
}
