// RestaurantDbModelToRestaurantViewModel.cs
// Authors: Araf Al-Jami
// Created: 06-07-2021 1:31 AM
// Updated: 06-07-2021 1:31 AM

namespace RestaurantsApi.Mappers
{
    using AutoMapper;
    using RestaurantsApi.Models;
    using RestaurantsApi.ViewModels;

    public class RestaurantDbModelToRestaurantViewModel : Profile
    {
        public RestaurantDbModelToRestaurantViewModel()
        {
            this.CreateMap<Restaurant, RestaurantViewModel>();
        }
    }
}
