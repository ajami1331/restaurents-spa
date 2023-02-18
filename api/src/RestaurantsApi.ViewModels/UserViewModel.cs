// UserViewModel.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 9:45 PM
// Updated: 05-07-2021 9:45 PM

namespace RestaurantsApi.ViewModels
{
    using System;

    public class UserViewModel
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string DisplayName { get; set; }

        public string UserName { get; set; }

        public string[] Roles { get; set; }

        public bool IsActive { get; set; }
    }
}
