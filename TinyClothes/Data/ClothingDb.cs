using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    /// <summary>
    /// Contains DB helper methods
    /// for <see cref="Modles.Clothing""/>
    /// </summary>
    public static class ClothingDb
    {
        public static List<Clothing> GetAllClothing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a clothing object ot the database
        /// Returns the object with the Id populated
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Clothing Add(Clothing c, StoreContext context)
        {
            context.Add(c); //prepares INSERT query
            context.SaveChanges(); //execute INSERT query

            return c;
        }
    }
}
