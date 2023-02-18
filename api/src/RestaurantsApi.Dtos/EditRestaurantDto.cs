// EditRestaurantDto.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 6:53 PM
// Updated: 05-07-2021 6:53 PM

namespace RestaurantsApi.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditRestaurantDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}
