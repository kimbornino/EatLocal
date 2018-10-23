using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EatLocal.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        [Display(Name = "Full Ingredient List")]
        public string Ingreients { get; set; }

        public string Directions { get; set; }

        public string Servings { get; set; }

        [Display(Name = "Nutritional Information")]
        public string NutritionalInfo { get; set; }

        public string Image { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<LocalFood> LocalFoods { get; set; }
    }
}
