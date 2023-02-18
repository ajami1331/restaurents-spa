// GetRestaurantsQueryDto.cs
// Authors: Araf Al-Jami
// Created: 04-07-2021 2:54 AM
// Updated: 04-07-2021 2:54 AM

namespace RestaurantsApi.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GetRestaurantsQueryDto
    {
        [Range(0, Int32.MaxValue)]
        public int PageSize { get; set; } = 10;

        [Range(0, Int32.MaxValue)]
        public int PageNumber { get; set; } = 0;

        [Range(0, 5)]
        public double LowerBound { get; set; } = 0;

        [Range(0, 5)]
        public double UpperBound { get; set; } = 5;
    }
}
