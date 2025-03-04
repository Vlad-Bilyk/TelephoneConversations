using System.ComponentModel.DataAnnotations;
using TelephoneConversations.Core.Models;

namespace TelephoneConversations.API.DTOs
{
    public class SubscriberCreateDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "CompanyName може містити не більше 100 символів.")]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "TelephonePoint може містити не більше 20 символів.")]
        public string TelephonePoint { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "IPN має містити рівно 10 символів.")]
        public string IPN { get; set; }

        [Required]
        [MaxLength(29, ErrorMessage = "BankAccount може містити не більше 29 символів.")]
        public string BankAccount { get; set; }
    }
}
