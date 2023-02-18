// RestaurantViewModel.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 7:45 PM
// Updated: 24-06-2021 7:45 PM

namespace RestaurantsApi.ViewModels
{
    using System;

    public class RestaurantViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double AverageRating { get; set; }

        public long ReviewCount { get; set; }
    }
}
