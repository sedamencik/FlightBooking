using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightBooking.Entities
{
    [Table("Routes")]
    public class Route
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string StartPoint { get; set; }

        [Required]
        [StringLength(50)]
        public string EndPoint { get; set; }
    }
}
