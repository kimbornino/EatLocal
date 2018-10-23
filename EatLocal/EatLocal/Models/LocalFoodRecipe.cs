using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EatLocal.Models
{
    public class LocalFoodRecipe
    {
        [Key]
        public int LocalFoodRecipeID { get; set; }

        [ForeignKey("Ingredient")]
        public int LocalFoodID { get; set; }
        public LocalFood LocalFoods { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
    }
}
