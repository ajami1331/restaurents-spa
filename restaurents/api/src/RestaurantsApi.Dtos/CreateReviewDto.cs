// CreateReviewDto.cs
// Authors: Araf Al-Jami
// Created: 27-06-2021 6:03 PM
// Updated: 27-06-2021 6:03 PM

namespace RestaurantsApi.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewDto
    {

        [Required]
        public string Comment { get; set; }

        [Required]
        public DateTime DateOfVisit { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public Guid RestaurantId { get; set; }
    }
}
