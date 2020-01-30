using Microsoft.EntityFrameworkCore;
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


        /// <summary>
        /// Returns a specific page of clothing items
        /// sorted by ItemId in ascending order
        /// </summary>
        /// <param name="pageNum">The Page</param>
        /// <param name="pageSize">Number of clothing per page</param>
        /// <returns></returns>
        public async static Task<List<Clothing>> GetClothingByPage(StoreContext context, int pageNum, int pageSize)
        {

            // If you wanted page 1, we wouldn't skip
            // any rows, so we must offset by 1
            const int Pageoffset = 1;
            //LINQ Method Syntax
            List<Clothing> clothes =
                await context.Clothing
                             .OrderBy(c => c.ItemId) //Have to pass in a function <>
                             .Skip(pageSize * (pageNum - Pageoffset)) //Must do skip then take
                             .Take(pageSize)
                             .ToListAsync();

            return clothes;

            //LINQ Query Syntax
            //List<Clothing> clothes2 =
            //    await (from c in context.Clothing
            //           select c)
            //                .OrderBy(c => c.ItemId) //Have to pass in a function <>
            //                .Skip(pageSize * (pageNum - Pageoffset)) //Must do skip then take
            //                .Take(pageSize)
            //                .ToListAsync();

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
