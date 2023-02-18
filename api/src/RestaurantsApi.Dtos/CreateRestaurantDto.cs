// CreateRestaurantDto.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 7:52 PM
// Updated: 24-06-2021 7:52 PM

namespace RestaurantsApi.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateRestaurantDto
    {

        [Required]
        public string Name { get; set; }
    }
}
