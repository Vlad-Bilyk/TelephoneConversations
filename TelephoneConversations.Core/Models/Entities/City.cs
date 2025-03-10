﻿using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.Core.Models.Entities
{
    public class City
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "CityID має бути більше 0.")]
        public int CityID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "CityName може містити не більше 100 символів.")]
        public string CityName { get; set; }

        public Tariff Tariff { get; set; }
        public ICollection<Call> Calls { get; set; }
    }
}
