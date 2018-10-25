using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EatLocalAPI.Models;

namespace TodoApi.Controllers
{
    [Route("api/Markets")]
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
        [HttpGet]
        public ActionResult<List<Markets>> GetAll()
        {
            return _context.Markets.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<Markets> GetById(long id)
        {
            var item = _context.Markets.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpPost]
        public IActionResult Create(Markets item)
        {
            _context.Markets.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.ID }, item);
        }
    }
}