using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.API.DTOs
{
    public class CallDTO
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "CallID має бути більше 0.")]
        public int CallID { get; set; }

        public int SubscriberID { get; set; }

        public int CityID { get; set; }

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
        public decimal BaseCost { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Discount має бути від 0 до 100.")]
        public decimal Discount { get; set; } = 0;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "CostWithDiscount не може бути від’ємним.")]
        public decimal CostWithDiscount { get; set; }
    }
}
