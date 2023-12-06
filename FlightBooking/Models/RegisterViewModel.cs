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
        [MinLength(8, ErrorMessage = "Minimum password length is 8 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm your password.")]
        [MinLength(8, ErrorMessage = "Minimum password length is 8 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match.")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Your first name is required.")]
        [MinLength(2, ErrorMessage = "Your name must be at least 2 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Your surname is required.")]
        [MinLength(2, ErrorMessage = "Your name must be at least 2 characters.")]
        public string Surname { get; set; }

    }
}
