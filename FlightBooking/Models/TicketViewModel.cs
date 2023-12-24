using FlightBooking.Entities;
using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Models
{
    public class TicketViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public int TicketNumber { get; set; }
        public User User { get; set; }

        public Flight Flight { get; set; }
    }
    public class CreateTicketViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int TicketNumber { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid FlightId { get; set; }
    }
}
