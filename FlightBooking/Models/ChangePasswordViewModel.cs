using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Your old password is required for security.")]
        [StringLength(50)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Your new password is required.")]
        [StringLength(50)]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
        [Required(ErrorMessage = "You need to confirm your new password.")]
        [StringLength(50)]
        public string NewRePassword { get; set; }
    }

}
