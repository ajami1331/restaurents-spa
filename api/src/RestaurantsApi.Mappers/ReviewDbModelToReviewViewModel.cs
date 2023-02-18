// ReviewToReviewViewModel.cs
// Authors: Araf Al-Jami
// Created: 06-07-2021 12:02 AM
// Updated: 06-07-2021 12:02 AM

namespace RestaurantsApi.Mappers
{
    using AutoMapper;
    using RestaurantsApi.Models;
    using RestaurantsApi.ViewModels;

    public class ReviewDbModelToReviewViewModel: Profile
    {
        public ReviewDbModelToReviewViewModel()
        {
            this.CreateMap<Review, ReviewViewModel>();
        }
    }
}
