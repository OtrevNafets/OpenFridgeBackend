using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        public string? RecipeName { get; set; }

        public virtual List<Ingridient>? Ingridients { get; set; }

        public RecipeType recipeType { get; set; }

        public string? Author { get; set; }

        public string? Description { get; set; }
    }
}