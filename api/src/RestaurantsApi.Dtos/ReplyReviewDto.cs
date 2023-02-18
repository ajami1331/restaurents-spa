// ReplyReviewDto.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 6:05 PM
// Updated: 05-07-2021 6:05 PM

namespace RestaurantsApi.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReplyReviewDto
    {
        [Required]
        public string Reply { get; set; }


        [Required]
        public Guid Id { get; set; }
    }
}
