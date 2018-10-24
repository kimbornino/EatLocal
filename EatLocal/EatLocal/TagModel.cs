using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatLocal
{
    public class TagModel
    {
        public int FoodId { get; set; }

        public List<SelectListItem>  FoodList { get; set; }

        public int RecipeId { get; set; }
    }
}
