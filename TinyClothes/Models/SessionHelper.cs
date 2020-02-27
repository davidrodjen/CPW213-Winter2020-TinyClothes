using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public static class SessionHelper
    {
        //Field to store the "Id" key
        private const string IdKey = "Id";

        //Field for username
        private const string UsernameKey = "Username";


        public static void CreateUserSession
            (int id, string username, IHttpContextAccessor http)
        {
            http.HttpContext.Session.SetInt32(key: IdKey, value: id); //explict example showing what is passed in
            http.HttpContext.Session.SetString(UsernameKey, username);
        }

        /// <summary>
        /// Returns true if the user is logged in
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static bool IsUserLoggedIn
            (IHttpContextAccessor http)
        {
            int? memberId = http.HttpContext.Session.GetInt32(IdKey);
            return memberId.HasValue; //hasvalue is a bool that checks if it has a value
        }


        /// <summary>
        /// Logs user out
        /// </summary>
        public static void DestroyUserSession
            (IHttpContextAccessor http)
        {
            http.HttpContext.Session.Clear();
        }
    }
}
