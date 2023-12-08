using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(50)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
        [Required]
        [StringLength(50)]
        public string NewRePassword { get; set; }
    }

}
