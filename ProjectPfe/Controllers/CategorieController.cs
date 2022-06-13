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
        public async Task<IActionResult> GetById(string id)
        {
            var categorie = await categorieService.GetByIdAsync(id);

            return Ok(categorie);

        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await categorieService.GetAllAsync());
        }

        [HttpPost(Name = "Post")]
        [Route("Post")]
        public async Task<IActionResult> Post(Categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await categorieService.CreateAsync(categorie);
            return Ok(categorie.Id);
        }

        [HttpPost(Name = "Update")]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(string id, Categorie updatedCategorie)
        {
            var categorie = categorieService.GetByIdAsync(id);

            if (categorie == null)
            {
                return NotFound();
            }

            await categorieService.UpdateAsync(id, updatedCategorie);

            return NoContent();
        }

        [HttpDelete(Name = "Delete")]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var categorie = await categorieService.GetByIdAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }
            await categorieService.DeleteAsync(categorie.Id);
            return Ok($"categorie with Id = {id} deleted");
        }
    }
}
