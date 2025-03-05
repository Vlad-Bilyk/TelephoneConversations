using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.API.DTOs
{
    public class TariffCreateDTO
    {
        [Required]
        public int CityID { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "DayPrice не може бути від’ємним.")]
        public decimal DayPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "NightPrice не може бути від’ємним.")]
        public decimal NightPrice { get; set; }
    }
}
