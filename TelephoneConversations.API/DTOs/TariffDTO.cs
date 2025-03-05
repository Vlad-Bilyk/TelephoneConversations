using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.API.DTOs
{
    public class TariffDTO
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "TariffID має бути більше 0.")]
        public int TariffID { get; set; }

        public int CityID { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "DayPrice не може бути від’ємним.")]
        public decimal DayPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "NightPrice не може бути від’ємним.")]
        public decimal NightPrice { get; set; }
    }
}
