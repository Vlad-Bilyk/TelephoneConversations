using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.Core.Models
{
    public class Call
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "CallID має бути більше 0.")]
        public int CallID { get; set; }

        public int SubscriberID { get; set; }
        public Subscriber Subscriber { get; set; }

        public int CityID { get; set; }
        public City City { get; set; }

        [Required]
        public DateTime CallDate { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Duration не може бути від’ємним.")]
        public int Duration { get; set; }  // in seconds

        [Required]
        [RegularExpression("^(день|ніч)$", ErrorMessage = "TimeOfDay має бути 'день' або 'ніч'.")]
        public string TimeOfDay { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "BaseCost не може бути від’ємним.")]
        [Precision(10, 2)]
        public decimal BaseCost { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Discount має бути від 0 до 100.")]
        [Precision(10, 2)]
        public decimal Discount { get; set; } = 0;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "CostWithDiscount не може бути від’ємним.")]
        [Precision(10, 2)]
        public decimal CostWithDiscount { get; set; }
    }
}
