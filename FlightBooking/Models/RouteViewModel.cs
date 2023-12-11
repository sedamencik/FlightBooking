using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Models
{
    public class RouteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Route's starting point is required.")]
        [MinLength(3, ErrorMessage = "Minimum password length is 3 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        public string StartPoint { get; set; }

        [Required(ErrorMessage = "Route's ending point is required.")]
        [MinLength(3, ErrorMessage = "Minimum password length is 3 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        public string EndPoint { get; set; }
    }
}
