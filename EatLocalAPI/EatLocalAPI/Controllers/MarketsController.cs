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

        [HttpGet("{id}", Name = "GetMarkets")]

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

            return CreatedAtRoute("GetMarkets", new { id = item.ID }, item);
        }
        //[HttpPut("{id}")]
        //public IActionResult Update(long id, Markets item)
        //{
        //    var markets = _context.Markets.Find(id);
        //    if (markets == null)
        //    {
        //        return NotFound();
        //    }

        //    markets.IsComplete = item.IsComplete;
        //    todo.Name = item.Name;

        //    _context.TodoItems.Update(todo);
        //    _context.SaveChanges();
        //    return NoContent();
        //}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var markets = _context.Markets.Find(id);
            if (markets == null)
            {
                return NotFound();
            }

            _context.Markets.Remove(markets);
            _context.SaveChanges();
            return NoContent();
        }
    }
}