using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EatLocalAPI.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketsController : ControllerBase
    {
        private readonly EatLocalContext _context;

        public MarketsController(EatLocalContext context)
        {
            _context = context;

            if (_context.Markets.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Markets.Add(new Markets { Name = "Market1" });
                _context.SaveChanges();
            }
        }
    }
}