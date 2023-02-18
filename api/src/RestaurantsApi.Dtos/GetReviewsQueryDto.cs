// GetReviewsQueryDto.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 4:31 PM
// Updated: 05-07-2021 4:31 PM

namespace RestaurantsApi.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GetReviewsQueryDto
    {
        [Range(0, Int32.MaxValue)]
        public int PageSize { get; set; } = 10;

        [Range(0, Int32.MaxValue)]
        public int PageNumber { get; set; } = 0;

    }
}
