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
    public class LocalFoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalFoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LocalFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocalFood.ToListAsync());
        }

        // GET: LocalFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localFood = await _context.LocalFood
                .FirstOrDefaultAsync(m => m.FoodID == id);
            if (localFood == null)
            {
                return NotFound();
            }

            return View(localFood);
        }

        // GET: LocalFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocalFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodID,FoodName,StartDate,EndDate,FoodImage,NutritionalInfo")] LocalFood localFood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(localFood);
        }

        // GET: LocalFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localFood = await _context.LocalFood.FindAsync(id);
            if (localFood == null)
            {
                return NotFound();
            }
            return View(localFood);
        }

        // POST: LocalFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodID,FoodName,StartDate,EndDate,FoodImage,NutritionalInfo")] LocalFood localFood)
        {
            if (id != localFood.FoodID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalFoodExists(localFood.FoodID))
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
            return View(localFood);
        }

        // GET: LocalFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localFood = await _context.LocalFood
                .FirstOrDefaultAsync(m => m.FoodID == id);
            if (localFood == null)
            {
                return NotFound();
            }

            return View(localFood);
        }

        // POST: LocalFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var localFood = await _context.LocalFood.FindAsync(id);
            _context.LocalFood.Remove(localFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Get
        public IActionResult SearchByMonth()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SearchByMonth(int month)
        {
            if (month != 0)
            {

                var FoodByMonth = _context.LocalFood.Where(m => m.StartDate.Month <= month && m.EndDate.Month <= month);


                return View("Index", FoodByMonth);
            }
            else
            {
                return View("Index");

            }

        }
        private bool LocalFoodExists(int id)
        {
            return _context.LocalFood.Any(e => e.FoodID == id);
        }
    }
}
