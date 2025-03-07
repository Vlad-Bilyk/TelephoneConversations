using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.Core.Models.Entities
{
    public class Subscriber
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "SubscriberID має бути більше 0.")]
        public int SubscriberID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "CompanyName може містити не більше 100 символів.")]
        public string CompanyName { get; set; }

        [Required]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Номер телефону має бути у форматі +380XXXXXXXXX.")]
        public string TelephonePoint { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ІПН має містити рівно 10 цифр.")]
        public string IPN { get; set; }

        [Required]
        [StringLength(29, ErrorMessage = "Розрахунковий рахунок повинен містити не більше 29 символів.")]
        [RegularExpression(@"^UA\d{27}$", ErrorMessage = "Розрахунковий рахунок повинен починатися з 'UA' та містити 29 символів.")]
        public string BankAccount { get; set; }

        public ICollection<Call> Calls { get; set; }
    }
}
