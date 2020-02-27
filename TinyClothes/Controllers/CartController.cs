using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _http;
        private readonly StoreContext _context;

        public CartController(StoreContext context, IHttpContextAccessor http)
        {
            _http = http;
            _context = context;
        }
        // Display all products in cart
        public IActionResult Index()
        {
            return View();
        }


        // Add a single product to the shopping cart
        public async Task<IActionResult> Add(int id, string prevUrl)
        {
            Clothing c = await ClothingDb.GetClothingById(id, _context);

            if(c != null)
            {
                CartHelper.Add(c, _http);
            };

            return Redirect(prevUrl);
        }

        // Summary/Checkout page
        public IActionResult Checkout()
        {
            return View();
        }
    }
}