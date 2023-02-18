// EditReviewDto.cs
// Authors: Araf Al-Jami
// Created: 06-07-2021 12:24 AM
// Updated: 06-07-2021 12:24 AM

namespace RestaurantsApi.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditReviewDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public DateTime DateOfVisit { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
