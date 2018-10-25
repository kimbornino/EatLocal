using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatLocalAPI.Models
{
    public class EatLocalContext: DbContext
    {
        public EatLocalContext(DbContextOptions<EatLocalContext> options)
    : base(options)
        {
        }

        public DbSet<Markets> Markets { get; set; }
    }
}
