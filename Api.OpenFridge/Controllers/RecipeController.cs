using DataAccess;
using DataAccess.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Api.OpenFridge.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly DataContext _ctx;

        public RecipeController(DataContext ctx)
        {
            this._ctx = ctx;
        }

        [HttpGet]
        public async Task<ActionResult<Recipe>> Get(string recipeName)
        {
            Recipe? recipe = _ctx.Recipes.Include("Ingridients").Where(f => f.RecipeName == recipeName).FirstOrDefault();
            if (recipe == null)
            {
                return BadRequest("Recipe not Found");
            }
            return recipe;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Recipe>>> Get()
        {
            List<Recipe> recipes = await _ctx.Recipes.Include("Ingridients").ToListAsync();
            if (recipes == null)
            {
                return BadRequest("Recipe not Found");
            }
            return recipes;
        }

        [HttpPost]
        public async Task<ActionResult> PostRecipe(Recipe recipe)
        {
            this._ctx.Recipes.Add(recipe);
            await this._ctx.SaveChangesAsync();

            return Ok(await _ctx.Recipes.ToListAsync());
        }

        [HttpPost("addIngridientToRecipe")]
        public async Task<ActionResult> PostAddIngridientToRecipe(string recipeName , Ingridient ingridient)
        {
            Recipe? recipe = _ctx.Recipes.Include("Ingridients").FirstOrDefault(recipe => recipe.RecipeName == recipeName);
            recipe.Ingridients.Add(ingridient);

            await this._ctx.SaveChangesAsync();

            return Ok(await _ctx.Recipes.ToListAsync());
        }

        [HttpDelete("removeIngridientFromRecipe")]
        public async Task<ActionResult> DeleteRecipeIngridient(string recipeName, string ingridientName)
        {
            Recipe? recipeToUpdate = _ctx.Recipes.Include("Ingridients").FirstOrDefault(recipe => recipe.RecipeName == recipeName);
            Ingridient? ingridientToDelete = recipeToUpdate.Ingridients.FirstOrDefault(ing => ing.IngridientName == ingridientName);

            if (ingridientToDelete == null)
            {
                return BadRequest("Recipe not Found");
            }

            _ctx.Ingridients.Remove(ingridientToDelete);
            await _ctx.SaveChangesAsync();

            return Ok(recipeToUpdate);
        }

        [HttpDelete]
        public async Task<ActionResult> DelteRecipe(string recipeName)
        {

            Recipe? recipeToDelete = _ctx.Recipes.Include("Ingridients").Where(f => f.RecipeName == recipeName).FirstOrDefault();


            if (recipeToDelete == null)
            {
                return BadRequest("Recipe not Found");
            }

            foreach(Ingridient ingridient in recipeToDelete.Ingridients)
            {
                _ctx.Ingridients.Remove(ingridient);
                await _ctx.SaveChangesAsync();
            }

            _ctx.Recipes.Remove(recipeToDelete);
            await _ctx.SaveChangesAsync();

            return Ok("Recipe deleted");
        }
    }
}
