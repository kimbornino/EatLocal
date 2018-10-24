using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EatLocal.Data;
using EatLocal.Models;
using System.Security.Claims;

namespace EatLocal.Controllers
{
    public class DailyMealPlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DailyMealPlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DailyMealPlans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DailyMealPlan.Include(d => d.ApplicationUser).Include(d => d.Recipe);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DailyMealPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyMealPlan = await _context.DailyMealPlan
                .Include(d => d.ApplicationUser)
                .Include(d => d.Recipe)
                .FirstOrDefaultAsync(m => m.MealPlanID == id);
            if (dailyMealPlan == null)
            {
                return NotFound();
            }

            return View(dailyMealPlan);
        }

        // GET: DailyMealPlans/Create
        public IActionResult Create()
        {
            //ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "RecipeID", "RecipeID");
            return View();
        }

        // POST: DailyMealPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MealPlanID,Date,DayOfWeek,RecipeID,ApplicationUserId")] DailyMealPlan dailyMealPlan)
        {
 
            dailyMealPlan.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                _context.Add(dailyMealPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", dailyMealPlan.ApplicationUserId);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "RecipeID", "RecipeID", dailyMealPlan.RecipeID);
            return View(dailyMealPlan);
        }

        // GET: DailyMealPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyMealPlan = await _context.DailyMealPlan.FindAsync(id);
            if (dailyMealPlan == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", dailyMealPlan.ApplicationUserId);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "RecipeID", "RecipeID", dailyMealPlan.RecipeID);
            return View(dailyMealPlan);
        }

        // POST: DailyMealPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MealPlanID,Date,DayOfWeek,RecipeID,ApplicationUserId")] DailyMealPlan dailyMealPlan)
        {
            if (id != dailyMealPlan.MealPlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyMealPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyMealPlanExists(dailyMealPlan.MealPlanID))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", dailyMealPlan.ApplicationUserId);
            ViewData["RecipeID"] = new SelectList(_context.Recipe, "RecipeID", "RecipeID", dailyMealPlan.RecipeID);
            return View(dailyMealPlan);
        }

        // GET: DailyMealPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyMealPlan = await _context.DailyMealPlan
                .Include(d => d.ApplicationUser)
                .Include(d => d.Recipe)
                .FirstOrDefaultAsync(m => m.MealPlanID == id);
            if (dailyMealPlan == null)
            {
                return NotFound();
            }

            return View(dailyMealPlan);
        }

        // POST: DailyMealPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyMealPlan = await _context.DailyMealPlan.FindAsync(id);
            _context.DailyMealPlan.Remove(dailyMealPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //get
        public IActionResult SaveRecipe(int? id)
        {
            
            return View("Index");
        }

        [HttpPost]
        public IActionResult SaveRecipe(int? id, Recipe recipe)
        {
            if (id != null)
            {
                var recipeid = _context.Recipe.Where(m => m.RecipeID == recipe.RecipeID);
                var dailyMealPlan = _context.DailyMealPlan.Where(m => m.MealPlanID == id).FirstOrDefault();
         
                _context.Update(recipeid);
                _context.SaveChangesAsync();
            }
            return View();
        }

        private bool DailyMealPlanExists(int id)
        {
            return _context.DailyMealPlan.Any(e => e.MealPlanID == id);
        }
    }
}
