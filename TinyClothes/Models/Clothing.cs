using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// Represents a single clothing
    /// item
    /// </summary>
    public class Clothing
    {
        /// <summary>
        /// The unique id for the clothing item
        /// </summary>
        [Key] // Set as PK
        public int ItemId { get; set; }

        /// <summary>
        /// The size of the clothing (Small, Medium, Large)
        /// </summary>
        [Required(ErrorMessage = "Size is required")]
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing, shirt, pants,
        /// etc
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The color of the clothing item
        /// </summary>
        [Required]
        public string Color { get; set; }

        /// <summary>
        /// Retail price of the item
        /// </summary>
        [Range(0.0, 300.0)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        /// <summary>
        /// The display title of the clothing item
        /// </summary>
        [Required]
        [StringLength(35)]
        //Sample Regex, great for validation
        //[RegularExpression("^([A-Za-z0-9])+$")]
        public string Title { get; set; }

        /// <summary>
        /// Description of the clothing
        /// </summary>
        [Required]
        [StringLength(800)]
        public string Description { get; set; }
    }
}
