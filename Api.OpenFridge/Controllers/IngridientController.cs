using Azure.Core;
using DataAccess;
using DataAccess.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Api.OpenFridge.Controllers
{
    [Route("api/ingridient")]
    [ApiController]
    public class IngridientController : Controller
    {
        private readonly DataContext _ctx;

        public IngridientController(DataContext ctx)
        {
            this._ctx = ctx;
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Ingridient>>> Get(string ingridientName)
        {
            List<Ingridient> ingridients = await _ctx.Ingridients.Where(r => r.IngridientName.Contains(ingridientName)).ToListAsync();
            if (ingridients == null)
            {
                return BadRequest("Ingridient not Found");
            }
            return ingridients;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Ingridient>>> Get()
        {
            List<Ingridient> ingridients = await _ctx.Ingridients.ToListAsync();
            if (ingridients == null)
            {
                return BadRequest("Ingridient not Found");
            }
            return ingridients;
        }

        [HttpPost]
        public async Task<ActionResult> PostRecipe(Ingridient ingridient)
        {
            Ingridient? ingridientInDB = _ctx.Ingridients.FirstOrDefault(ing => ing.IngridientName == ingridient.IngridientName);
            if(ingridientInDB != null)
            {
                return BadRequest("Ingridient already in DB");
            }

            this._ctx.Ingridients.Add(ingridient);
            await this._ctx.SaveChangesAsync();

            return Ok(await _ctx.Recipes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult> PutRecipe(Ingridient request)
        {
            Ingridient? ingridientToUpdate = _ctx.Ingridients.FirstOrDefault(ing => ing.IngridientName == request.IngridientName);
            if (ingridientToUpdate == null)
            {
                return BadRequest("Ingridient not Found");
            }

            ingridientToUpdate.IngridientName = request.IngridientName;
            ingridientToUpdate.IsVegetarian = request.IsVegetarian;

            await _ctx.SaveChangesAsync();

            return Ok(ingridientToUpdate);
        }

        [HttpDelete]
        public async Task<ActionResult> DelteIngridient(string ingridientName)
        {

            Ingridient? ingridientToDelete = _ctx.Ingridients.FirstOrDefault(ing => ing.IngridientName == ingridientName);


            if (ingridientToDelete == null)
            {
                return BadRequest("Ingridient not Found");
            }

            _ctx.Ingridients.Remove(ingridientToDelete);
            await _ctx.SaveChangesAsync();

            return Ok("Ingridient deleted");
        }
    }
}
