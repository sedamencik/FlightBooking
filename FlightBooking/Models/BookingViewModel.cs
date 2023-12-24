using FlightBooking.Entities;

namespace FlightBooking.Models
{
    public class SeatViewModel
    {
        public int SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
    public class BookingViewModel
    {
        public Flight Flight { get; set; }
        public List<SeatViewModel> SeatLayout { get; set; }
    }
}
