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


        [HttpDelete(Name = "GetById")]
        [Route("GetById/{id}")]
        public IActionResult GetById(string id)
        {
            var categorie = categorieService.Get(id);

            return Ok(categorie);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(categorieService.Get());
        }

        [HttpPost(Name = "Post")]
        [Route("Post")]
        public IActionResult Post(Categorie categorie)
        {
            
            categorieService.Create(categorie);
            return CreatedAtAction(nameof(GetById), new { id = categorie.Id }, categorie);
        }

        [HttpPost(Name = "Update")]
        [Route("Update/{id}")]
        public IActionResult Update(string id, Categorie updatedCategorie)
        {
            var categorie = categorieService.Get(id);

            if (categorie == null)
            {
                return NotFound();
            }

             categorieService.Update(id, updatedCategorie);

            return NoContent();
        }

        [HttpDelete(Name = "Delete")]
        [Route("Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var categorie =  categorieService.Get(id);
            if (categorie == null)
            {
                return NotFound();
            }
            categorieService.Remove(categorie.Id);
            return Ok($"categorie with Id = {id} deleted");
        }
    }
}
