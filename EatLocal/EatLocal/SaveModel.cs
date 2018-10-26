using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatLocal
{
    public class SaveModel
    {

            public int RecipeId { get; set; }

            public List<SelectListItem> RecipeList { get; set; }

            public int DailyMealPlan { get; set; }
        
    }

}

