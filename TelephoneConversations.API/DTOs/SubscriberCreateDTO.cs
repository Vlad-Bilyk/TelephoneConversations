using System.ComponentModel.DataAnnotations;
using TelephoneConversations.Core.Models;

namespace TelephoneConversations.API.DTOs
{
    public class SubscriberCreateDTO
    {
        public string CompanyName { get; set; }

        public string TelephonePoint { get; set; }

        public string IPN { get; set; }

        public string BankAccount { get; set; }
    }
}
