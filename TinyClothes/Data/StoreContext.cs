using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    public class StoreContext : DbContext //Right click solution > click manage nuget, install microsoft.entity.frameworkcore
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }
        /// <summary>
        /// Add a DbSet for each Entity that needs
        /// to be tracked by the DB
        /// https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-3.1
        /// </summary>
        public DbSet<Clothing> Clothing { get; set; } //property names can be the same as class names in C#
    }
}
