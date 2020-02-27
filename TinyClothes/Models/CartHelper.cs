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
        public static void Add(Clothing c, IHttpContextAccessor http)
        {
            //convert object to text
            string data = JsonConvert.SerializeObject(c);

            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(14),
                IsEssential = true,
                Secure = true
            };

            http.HttpContext.Response.Cookies.Append("CartCookie", data);

            //CookieOptions options = new CookieOptions();
            //options.Expires
        }

        public static int GetCount(IHttpContextAccessor http)
        {
            throw new NotImplementedException();
        }

        public static List<Clothing> GetAllClothes(IHttpContextAccessor http)
        {
            throw new NotImplementedException();
        }
    }
}
