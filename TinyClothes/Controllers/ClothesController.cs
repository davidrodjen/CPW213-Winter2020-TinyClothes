using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class ClothesController : Controller
    {
        private readonly StoreContext _context; //readonly, only constructor can edit

        public ClothesController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ShowAll(int? page)
        {

            const int PageSize = 2;
            // Null-coalescing operator ??
            int pageNumber = page ?? 1; //use page number, if not there use 1, C# Null coalescing Operator, look it up
            ViewData["CurrentPage"] = pageNumber;

            int maxPage = await GetMaxPage(PageSize);
            ViewData["MaxPage"] = maxPage;

            // Just a placeholder ...
            List<Clothing> clothes =
                   await ClothingDb.GetClothingByPage(_context, pageNum: pageNumber, pageSize: PageSize);
            return View(clothes);
        }

        private async Task<int> GetMaxPage(int PageSize)
        {
            int numProducts = await ClothingDb.GetNumClothing(_context);

            int maxPage = Convert.ToInt32(Math.Ceiling((double)numProducts / PageSize)); //need to cast a double for dividing of ints, then use Math.ceiling to go to next int up
            return maxPage;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Clothing c)
        {
            if (ModelState.IsValid)
            {
                await ClothingDb.Add(c, _context);
                // TempData last for one redirect
                TempData["Message"] = $"{c.Title} added successfully";
                return RedirectToAction("ShowAll");
            }

            //Return same view with validation messages
            return View(c);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Clothing c =
                await ClothingDb.GetClothingById(id, _context); //context is a field in the controller, hence _context

            if(c == null) // Clothing not in the DB
            {
                // RETURNS A HTTP 404 - nOT FOUND
                return NotFound();
                //Redirects to page
                //return RedirectToAction("ShowAll");
            }

            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Clothing c)
        {
            if (ModelState.IsValid)
            {
                await ClothingDb.Edit(c, _context);
                ViewData["Message"] =
                    c.Title + " updated successfully";
                return View(c); // You can take this one out if you want
            }

            return View(c);
        }
    }
}