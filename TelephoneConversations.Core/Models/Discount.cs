﻿using System.ComponentModel.DataAnnotations;

namespace TelephoneConversations.Core.Models
{
    
    public class Discount
    {
        [Range(1, int.MaxValue, ErrorMessage = "DiscountID має бути більше 0.")]
        public int DiscountID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "TariffID має бути більше 0.")]
        public int TariffID { get; set; }
        public Tariff Tariff { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "DurationMin не може бути від’ємним.")]
        public int DurationMin { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "DurationMax не може бути від’ємним.")]
        public int DurationMax { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "DiscountRate має бути від 0 до 100.")]
        public decimal DiscountRate { get; set; }
    }
}
