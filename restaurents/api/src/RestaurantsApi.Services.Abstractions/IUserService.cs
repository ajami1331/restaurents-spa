// IUserService.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 9:37 PM
// Updated: 05-07-2021 9:37 PM

using System;

namespace RestaurantsApi.Services.Abstractions
{
    using System.Threading.Tasks;
    using RestaurantsApi.Dtos;
    using RestaurantsApi.ViewModels;

    public interface IUserService
    {
        Task<ServiceResponse<UserViewModel>> CreateUserAsync(CreateUserDto createUserDto);

        Task<PageResponse<UserViewModel>> GetUsersAsync(int pageSize, int pageNumber);

        Task<UserViewModel> GetUserAsync(Guid id);

        Task DeleteUserAsync(Guid id);

        Task EditUserAsync(EditUserDto editUserDto);
    }
}
