using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EatLocal.Models;

namespace EatLocal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EatLocal.Models.Recipe> Recipe { get; set; }
        public DbSet<EatLocal.Models.LocalFood> LocalFood { get; set; }
        public DbSet<EatLocal.Models.MessageBoard> MessageBoard { get; set; }
        public DbSet<EatLocal.Models.DailyMealPlan> DailyMealPlan { get; set; }
        public DbSet<EatLocal.Models.LocalFoodRecipe> LocalFoodRecipe { get; set; }
        public DbSet<EatLocal.Models.LocalMarkets> LocalMarkets { get; set; }
        //do I need to add application user here?
    }
}
