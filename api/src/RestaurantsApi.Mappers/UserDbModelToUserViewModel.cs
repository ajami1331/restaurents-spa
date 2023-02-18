// UserDbModelToUserViewModel.cs
// Authors: Araf Al-Jami
// Created: 06-07-2021 1:29 AM
// Updated: 06-07-2021 1:29 AM

namespace RestaurantsApi.Mappers
{
    using AutoMapper;
    using RestaurantsApi.Models;
    using RestaurantsApi.ViewModels;

    public class UserDbModelToUserViewModel: Profile
    {
        public UserDbModelToUserViewModel()
        {

            this.CreateMap<User, UserViewModel>();
        }
    }
}
