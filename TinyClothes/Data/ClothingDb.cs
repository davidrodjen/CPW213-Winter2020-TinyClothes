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
        public static async Task<Clothing> Add(Clothing c, StoreContext context) //must wrap method with task type
        {
            await context.AddAsync(c); //prepares INSERT query //await is required to run something Async
            await context.SaveChangesAsync(); //execute INSERT query

            return c;
        }
    }
}
