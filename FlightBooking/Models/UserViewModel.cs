using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "user";
    }

    public class CreateUserViewModel
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
        [Required(ErrorMessage = "Confirm the password.")]
        [MinLength(3, ErrorMessage = "Minimum password length is 3 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match.")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "User's full name is required.")]
        [MinLength(5, ErrorMessage = "User's full name must be at least 5 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Role is required for authorization.")]
        [StringLength(50)]
        public string Role { get; set; } = "user";
    }
    public class EditUserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "User's full name is required.")]
        [MinLength(5, ErrorMessage = "User's full name must be at least 5 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Role is required for authorization.")]
        [StringLength(50)]
        public string Role { get; set; } = "user";
    }

    public class ChangePasswordUserViewModel
    {
        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(3, ErrorMessage = "Minimum password length is 3 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm the password.")]
        [MinLength(3, ErrorMessage = "Minimum password length is 3 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match.")]
        public string RePassword { get; set; }
    }
}
