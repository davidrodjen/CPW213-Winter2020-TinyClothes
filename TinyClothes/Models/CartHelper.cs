using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// Helper Class to manage users
    /// shopping cart data using cookies
    /// </summary>
    public static class CartHelper
    {
        private const string CartCookie = "CartCookie";

        public static void Add(Clothing c, IHttpContextAccessor http)
        {
            List<Clothing> clothes = GetAllClothes(http);
            clothes.Add(c);

            //convert object to text
            string data = JsonConvert.SerializeObject(clothes);

            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(14),
                IsEssential = true,
                Secure = true
            };

            http.HttpContext.Response.Cookies.Append(CartCookie, data, options);

            //CookieOptions options = new CookieOptions();
            //options.Expires
        }

        public static int GetCount(IHttpContextAccessor http)
        {
            List<Clothing> allClothes = GetAllClothes(http);
            return allClothes.Count;
        }


        /// <summary>
        /// Returns all clothing currently stored in the users cookie.
        /// if no items are present an empty list is returned.
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static List<Clothing> GetAllClothes(IHttpContextAccessor http)
        {
            string data = http.HttpContext.Request.Cookies[CartCookie];
            if (string.IsNullOrWhiteSpace(data))
            {
                return new List<Clothing>();
            }

            return JsonConvert.DeserializeObject<List<Clothing>>(data);
        }
    }
}
