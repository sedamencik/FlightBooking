using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightBooking.Entities
{
    [Table("Flights")]
    public class Flight
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Route Route { get; set; }

        [Required]
        public Aircraft Aircraft { get; set; }

        public DateTime FlightTime { get; set; }
    }
}
