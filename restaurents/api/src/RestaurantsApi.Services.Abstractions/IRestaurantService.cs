// IRestaurantService.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 7:42 PM
// Updated: 24-06-2021 7:42 PM

using System;

namespace RestaurantsApi.Services.Abstractions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RestaurantsApi.Dtos;
    using RestaurantsApi.ViewModels;

    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantViewModel>> GetAllRestaurantsAsync();

        Task<RestaurantViewModel> CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto);

        Task<RestaurantViewModel> GetRestaurantAsync(Guid id);

        Task<PageResponse<RestaurantViewModel>> GetAllRestaurantsAsync(int pageSize, int pageNumber, double lowerBound, double upperBound);

        Task<string> GetRestaurantNameAsync(Guid id);

        Task EditRestaurantAsync(EditRestaurantDto editRestaurantDto);

        Task DeleteRestaurantAsync(Guid id);
    }
}
