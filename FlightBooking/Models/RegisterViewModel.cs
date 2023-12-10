using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(3, ErrorMessage = "Minimum password length is 3 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm your password.")]
        [MinLength(3, ErrorMessage = "Minimum password length is 3 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match.")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Your full name is required.")]
        [MinLength(5, ErrorMessage = "Your name must be at least 5 characters.")]
        public string FullName { get; set; }

    }
}
