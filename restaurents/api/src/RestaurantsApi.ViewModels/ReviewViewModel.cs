// ReviewViewModel.cs
// Authors: Araf Al-Jami
// Created: 27-06-2021 5:42 PM
// Updated: 27-06-2021 5:42 PM

namespace RestaurantsApi.ViewModels
{
    using System;

    public class ReviewViewModel
    {
        public Guid Id { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public string Reply { get; set; }

        public string ReviewerName { get; set; }

        public DateTime DateOfVisit { get; set; }

        public Guid RestaurantId { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
