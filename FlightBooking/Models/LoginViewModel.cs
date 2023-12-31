﻿using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(3, ErrorMessage = "Minimum password length is 3 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
