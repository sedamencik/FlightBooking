using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FlightBooking.Models
{
    public class Flight
    {
        [Required(ErrorMessage = "Ad alanı zorunlu")]
        [Display(Name = "Öğrenci Ad")]
        public string OgrAd { get; set; }

        [Display(Name = "Öğrenci Soyad")]
        [Required(ErrorMessage = "Soyad alanı zorunlu")]
        public string OgrSoyad { get; set; }

        [Display(Name = "Öğrenci No")]
        [Required(ErrorMessage = "No alanı zorunlu")]
        public string OgrNo { get; set; }

        [Display(Name = "Öğrenci Yaş")]
        [Required(ErrorMessage = "Yaş alanı zorunlu")]
        public int OgrYas { get; set; }
    }
}
