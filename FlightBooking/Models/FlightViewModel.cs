using FlightBooking.Entities;
using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Models
{
    public class FlightViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Route is required for a flight.")]
        public Entities.Route Route { get; set; }

        [Required(ErrorMessage = "Aircraft is required for a flight.")]
        public Aircraft Aircraft { get; set; }

        [Required(ErrorMessage = "Please specify a time for this flight.")]
        public DateTime FlightTime { get; set; }
    }
    public class CreateFlightViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Route is required for a flight.")]
        public Guid RouteId { get; set; }

        [Required(ErrorMessage = "Aircraft is required for a flight.")]
        public Guid AircraftId { get; set; }

        [Required(ErrorMessage = "Please specify a time for this flight.")]
        public DateTime FlightTime { get; set; }
    }

}
