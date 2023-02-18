// CreateUserDto.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 9:48 PM
// Updated: 05-07-2021 9:48 PM

namespace RestaurantsApi.Dtos
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CreateUserDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
