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
        public IActionResult ShowAll()
        {
            // Just a placeholder ...
            List<Clothing> clothes =
                    new List<Clothing>();
            return View(clothes);
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
    }
}