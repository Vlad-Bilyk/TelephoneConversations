using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.Core.Models
{
    public class Tariff
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "TariffID має бути більше 0.")]
        public int TariffID { get; set; }

        public int CityID { get; set; }

        public City City { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "DayPrice не може бути від’ємним.")]
        [Precision(10, 2)]
        public decimal DayPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "NightPrice не може бути від’ємним.")]
        [Precision(10, 2)]
        public decimal NightPrice { get; set; }

        public ICollection<Discount> Discounts { get; set; }
    }
}
