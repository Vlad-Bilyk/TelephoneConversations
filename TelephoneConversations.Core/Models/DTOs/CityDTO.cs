using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.Core.Models.DTOs
{
    public class CityDTO
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "CityID має бути більше 0.")]
        public int CityID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "CityName може містити не більше 100 символів.")]
        public string CityName { get; set; }
    }
}
