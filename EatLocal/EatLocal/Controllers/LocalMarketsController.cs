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
    public class LocalMarketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalMarketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LocalMarkets
        public async Task<IActionResult> Index()
        {
            //will filter search to only markets open this time of the year.
            var openSeason = _context.LocalMarkets.Where(m => m.SeasonClose > DateTime.Now && m.SeasonOpen < DateTime.Now);

            if (openSeason != null)
            {
                var day = DateTime.Now.DayOfWeek;
                string stringDay = day.ToString();
                var hour = DateTime.Now.Hour;
                var intHour = Convert.ToInt32(hour);

                if (stringDay == "Monday")
                {
                    var openNow = _context.LocalMarkets.Where(m => m.MondayStart < hour && m.MondayEnd < hour);
                    return View(openNow);
                }
                //and so on and so forth


                else
                {
                    return View(await _context.LocalMarkets.ToListAsync());

                }
            }
            return View(await _context.LocalMarkets.ToListAsync());

        }

        // GET: LocalMarkets/Details/5
        public async Task<IActionResult> Details(int? id)
              {
            if (id == null)
            {
                return NotFound();
            }

            var localMarkets = await _context.LocalMarkets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (localMarkets == null)
            {
                return NotFound();
            }

            return View(localMarkets);
        }

        // GET: LocalMarkets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocalMarkets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,SeasonOpen,SeasonClose,Link,Bio,StreetAddress,CityStateZip,MondayStart,MondayEnd,TuesdayStart,TuesdayEnd,WednesayStart,WednesdayEnd,ThursdayStart,ThursdayEnd,FridayStart,FridayEnd,SaturdayStart,SaturdayEnd,SundayStart,SundayEnd")] LocalMarkets localMarkets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localMarkets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(localMarkets);
        }

        // GET: LocalMarkets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localMarkets = await _context.LocalMarkets.FindAsync(id);
            if (localMarkets == null)
            {
                return NotFound();
            }
            return View(localMarkets);
        }

        // POST: LocalMarkets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,SeasonOpen,SeasonClose,Link,Bio,StreetAddress,CityStateZip,MondayStart,MondayEnd,TuesdayStart,TuesdayEnd,WednesayStart,WednesdayEnd,ThursdayStart,ThursdayEnd,FridayStart,FridayEnd,SaturdayStart,SaturdayEnd,SundayStart,SundayEnd")] LocalMarkets localMarkets)
        {
            if (id != localMarkets.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localMarkets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalMarketsExists(localMarkets.ID))
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
            return View(localMarkets);
        }

        // GET: LocalMarkets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localMarkets = await _context.LocalMarkets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (localMarkets == null)
            {
                return NotFound();
            }

            return View(localMarkets);
        }


        // POST: LocalMarkets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var localMarkets = await _context.LocalMarkets.FindAsync(id);
            _context.LocalMarkets.Remove(localMarkets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Weather()
        {
            return View();
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

                var FoodByMonth = _context.LocalFood.Where(m => m.StartDate.Month <= month && m.EndDate.Month >= month);


                return View("Index", FoodByMonth);
            }
            else
            {
                return View("Index");

            }

        }

        public async Task<IActionResult> Map(int? id)
        {
            {
                
                LocalMarkets localMarket = _context.LocalMarkets.Find(id);
                if (localMarket == null)
                {
                    return NotFound();
                }
                ViewBag.SteetAddress = localMarket.StreetAddress;
                ViewBag.CityStateZip = localMarket.CityStateZip;
                return View(localMarket);
            }
        }
    

    private bool LocalMarketsExists(int id)
        {
            return _context.LocalMarkets.Any(e => e.ID == id);
        }
    }
}
