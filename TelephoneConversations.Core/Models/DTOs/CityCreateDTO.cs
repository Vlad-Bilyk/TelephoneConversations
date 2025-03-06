using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.Core.Models.DTOs
{
    public class CityCreateDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "CityName може містити не більше 100 символів.")]
        public string CityName { get; set; }
    }
}
