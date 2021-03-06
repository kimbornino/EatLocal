﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EatLocal.Data;
using EatLocal.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace EatLocal.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment he;
        private readonly UserManager<IdentityUser> _userManager;

        public RecipesController(ApplicationDbContext context, IHostingEnvironment e, UserManager<IdentityUser> userManager)
        {
            _context = context;
            he = e;
            _userManager = userManager;

        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recipe.Include(r => r.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.ApplicationUser)
                .FirstOrDefaultAsync(m => m.RecipeID == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {

            ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeID,Name,Category,Ingreients,Directions,Servings,NutritionalInfo,Image,ApplicationUserId")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", recipe.ApplicationUserId);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", recipe.ApplicationUserId);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecipeID,Name,Category,Ingreients,Directions,Servings,NutritionalInfo,Image,ApplicationUserId")] Recipe recipe)
        {
            if (id != recipe.RecipeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeID))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", recipe.ApplicationUserId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.ApplicationUser)
                .FirstOrDefaultAsync(m => m.RecipeID == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Tag(int? id)
        {
            var tagModel = new TagModel();
            tagModel.FoodList = new List<SelectListItem>();
           var foodList =  _context.LocalFood.ToList();
            foreach (var item in foodList)
            {
                tagModel.FoodList.Add(new SelectListItem { Text = item.FoodName, Value = item.FoodID.ToString()});
            }
            return View(tagModel);
        }

        [HttpPost]
        public IActionResult Tag(TagModel tag, int id)
        {
            if (tag != null)
            {
                LocalFoodRecipe recipe = new LocalFoodRecipe();

                recipe.RecipeID = id;
                recipe.LocalFoodID = tag.FoodId;
                
                _context.LocalFoodRecipe.Add(recipe);
                _context.SaveChanges();
               
            }
            return RedirectToAction("Index");
        }
        public IActionResult UploadImage(IFormFile pic, int? id)
        {
            if (pic == null)
            {
                return View();
            }

            if (pic != null)
            {
                var fullPath = Path.Combine(he.WebRootPath, Path.GetFileName(pic.FileName));
                var fileName = pic.FileName;
                pic.CopyTo(new FileStream(fullPath, FileMode.Create));

                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var recipe = _context.Recipe.Where(m => m.ApplicationUserId == userid).FirstOrDefault();

                recipe.Image = fileName;
                _context.Update(recipe);
                _context.SaveChangesAsync();

                ViewBag.ProfileImage = recipe.Image;
                ViewData["FileLocation"] = "/" + Path.GetFileName(pic.FileName);
            }

            return View();
        }
        public IActionResult RecipeByIngredient()
        {
            var tagModel = new TagModel();
            tagModel.FoodList = new List<SelectListItem>();
            var foodList = _context.LocalFood.ToList();
            foreach (var item in foodList)
            {
                tagModel.FoodList.Add(new SelectListItem { Text = item.FoodName, Value = item.FoodID.ToString() });
            }
            return View(tagModel);
        }
        [HttpPost]
        public IActionResult RecipeByIngredient(TagModel tag)
        {
            var foundMatches = _context.LocalFoodRecipe.Where(m => m.LocalFoodID == tag.FoodId);
            var recipes = foundMatches.Join(_context.Recipe, recipe => recipe.RecipeID, moreRecipe => moreRecipe.RecipeID, (localFoodRecipe, recipeObject) => recipeObject);
               

            return View("FilteredRecipeIndex", recipes); 
                }
        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.RecipeID == id);
        }
    }
}
