using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EatLocal.Data;
using EatLocal.Models;

namespace EatLocal.Controllers
{
    public class MessageBoardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessageBoardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MessageBoards
        public async Task<IActionResult> Index()
        {
            return View(await _context.MessageBoard.ToListAsync());
        }

        // GET: MessageBoards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageBoard = await _context.MessageBoard
                .FirstOrDefaultAsync(m => m.MessageBoardID == id);
            if (messageBoard == null)
            {
                return NotFound();
            }

            return View(messageBoard);
        }

        // GET: MessageBoards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageBoards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageBoardID,Date,Topic,Message,Name")] MessageBoard messageBoard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messageBoard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messageBoard);
        }

        // GET: MessageBoards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageBoard = await _context.MessageBoard.FindAsync(id);
            if (messageBoard == null)
            {
                return NotFound();
            }
            return View(messageBoard);
        }

        // POST: MessageBoards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageBoardID,Date,Topic,Message,Name")] MessageBoard messageBoard)
        {
            if (id != messageBoard.MessageBoardID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageBoard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageBoardExists(messageBoard.MessageBoardID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(messageBoard);
        }

        // GET: MessageBoards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageBoard = await _context.MessageBoard
                .FirstOrDefaultAsync(m => m.MessageBoardID == id);
            if (messageBoard == null)
            {
                return NotFound();
            }

            return View(messageBoard);
        }

        // POST: MessageBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messageBoard = await _context.MessageBoard.FindAsync(id);
            _context.MessageBoard.Remove(messageBoard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageBoardExists(int id)
        {
            return _context.MessageBoard.Any(e => e.MessageBoardID == id);
        }
    }
}
