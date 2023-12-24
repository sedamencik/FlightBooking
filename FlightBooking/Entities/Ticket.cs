using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightBooking.Entities
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int TicketNumber { get; set; }

        public bool IsAvailable { get; set; } = true;

        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }

    }
}
