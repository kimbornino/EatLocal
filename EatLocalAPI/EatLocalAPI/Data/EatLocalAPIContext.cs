using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EatLocalAPI.Models
{
    public class EatLocalAPIContext : DbContext
    {
        public EatLocalAPIContext (DbContextOptions<EatLocalAPIContext> options)
            : base(options)
        {
        }

        public DbSet<EatLocalAPI.Models.Markets> Markets { get; set; }
    }
}
