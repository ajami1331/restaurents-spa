// EditUserDto.cs
// Authors: Araf Al-Jami
// Created: 06-07-2021 1:47 AM
// Updated: 06-07-2021 1:47 AM

namespace RestaurantsApi.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditUserDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [MaxLength(1)]
        public string[] Roles { get; set; }
    }
}
