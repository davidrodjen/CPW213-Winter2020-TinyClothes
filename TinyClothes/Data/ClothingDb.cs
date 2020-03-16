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
        /// Returns the total number of Clothing items
        /// </summary>
        /// <returns></returns>
        public async static Task<int> GetNumClothing(StoreContext context)
        {
            return await context.Clothing.CountAsync();

            // Alternative with query syntax
            //return await (from c in context.Clothing select c).CountAsync();
        }

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
        /// Returns a single clothing item or
        /// null if there is no match
        /// </summary>
        /// <param name="id">Id of the clothing item</param>
        /// <param name="context">Database context</param>
        /// <returns></returns>
        public static async Task<Clothing> GetClothingById(int id, StoreContext context) //changed from internal to public
        {
            Clothing c = await (from clothing in context.Clothing
                                where clothing.ItemId == id
                                select clothing).SingleOrDefaultAsync(); //will return as one object rather than a list

            return c;
        }

        public static async Task<Clothing> Edit(Clothing c, StoreContext context) //changed from internal to public
        {
            await context.AddAsync(c);
            context.Entry(c).State = EntityState.Modified; //Telling framwork it is already in database, we are just modifying it
            await context.SaveChangesAsync();
            return c;
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


        public async static Task Delete(Clothing c, StoreContext context)
        { 
            await context.AddAsync(c);
            context.Entry(c).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public static async Task<SearchCriteria> BuildSearchQuery(SearchCriteria search, StoreContext context)
        {
            // Prepare query - SELECT * FROM Clothes
            // Does not get sent to DB
            IQueryable<Clothing> allClothes = from c in context.Clothing
                                              select c;
            // WHERE Price > MinPrice
            if (search.MinPrice.HasValue)
            {
                allClothes = from c in allClothes
                             where c.Price >= search.MinPrice
                             select c;
            }

            // WHERE Price < MaxPrice
            if (search.MaxPrice.HasValue)
            {
                allClothes = from c in allClothes
                             where c.Price <= search.MaxPrice
                             select c;
            }

            // If we are searching from something
            if (!string.IsNullOrWhiteSpace(search.Size))
            {
                allClothes = from c in allClothes
                             where c.Size == search.Size
                             select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Type))
            {
                allClothes = from c in allClothes
                             where c.Type == search.Type
                             select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Title))
            {
                allClothes = from c in allClothes
                             where c.Title.Contains(search.Title)
                             select c;
            }

            search.Results = await allClothes.ToListAsync();
            return search;
        }
    }
}
