using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightBooking.Entities
{
    [Table("Flights")]
    public class Flight
    {
        [Key]
        public Guid Id { get; set; }
        public Route Route { get; set; }

        public Aircraft Aircraft { get; set; }

        public DateTime FlightTime { get; set; }
        public int AvailableSeatCount { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}
