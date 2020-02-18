using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{

    /// <summary>
    /// A single user account
    /// </summary>
    public class Account
    {
        [Key] //Id at the end always notes it as a primary key, but add key to be explicit
        public int AccountId { get; set; }



        /// <summary>
        /// First and last name
        /// </summary>
        [Required]
        [StringLength(60)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.Password)] // <input type="password">
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public String Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

    }


    public class RegisterViewModel
    {
        [Required]
        [StringLength(60)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Compare confirm password to password
        /// </summary>
        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }


    public class LoginViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
