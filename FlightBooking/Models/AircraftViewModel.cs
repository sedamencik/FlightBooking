using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Models
{
    public class AircraftViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Aircraft name is required.")]
        [MinLength(3, ErrorMessage = "Minimum password length is 3 characters.")]
        [MaxLength(20, ErrorMessage = "Maximum password lenght is 20 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Seat count is required.")]
        public int SeatCount { get; set; }
    }
}
