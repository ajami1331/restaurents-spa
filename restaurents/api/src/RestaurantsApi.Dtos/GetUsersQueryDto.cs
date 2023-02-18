// GetUsersQueryDto.cs
// Authors: Araf Al-Jami
// Created: 06-07-2021 1:37 AM
// Updated: 06-07-2021 1:37 AM

namespace RestaurantsApi.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GetUsersQueryDto
    {
        [Range(0, Int32.MaxValue)]
        public int PageSize { get; set; } = 10;

        [Range(0, Int32.MaxValue)]
        public int PageNumber { get; set; } = 0;
    }
}
