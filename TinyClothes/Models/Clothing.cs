using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{   
    /// <summary>
    /// Represents a single clothing item
    /// </summary>
    public class Clothing
    {
        /// <summary>
        /// The unique id for the clothing item
        /// </summary>
        [Key] // Set as PK(Primary Key)
        public int ItemId { get; set; }

        /// <summary>
        /// The Size of the lcothing (Small, medium, large)
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Type of clothing, shirt, pants, etc
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The color of the clothing item
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Retail price of the item
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Display the title of the clothing
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of item
        /// </summary>
        public string Description { get; set; }

    }
}
