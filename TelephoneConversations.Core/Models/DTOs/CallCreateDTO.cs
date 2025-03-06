using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.Core.Models.DTOs
{
    public class CallCreateDTO
    {
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
    }
}
