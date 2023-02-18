// RestaurantService.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 7:42 PM
// Updated: 24-06-2021 7:42 PM

namespace RestaurantsApi.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.Common;
    using RestaurantsApi.Dtos;
    using RestaurantsApi.IdentityServer;
    using RestaurantsApi.Models;
    using RestaurantsApi.Repositories.Contracts;
    using RestaurantsApi.Services.Abstractions;
    using RestaurantsApi.ViewModels;

    public class RestaurantService : IRestaurantService
    {
        private readonly ILogger<RestaurantService> logger;
        private readonly IRepository repository;
        private readonly IReviewService reviewService;
        private readonly IUserContextProvider userContextProvider;
        private readonly IMapper mapper;

        public RestaurantService(ILogger<RestaurantService> logger, IRepository repository, IUserContextProvider userContextProvider, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.userContextProvider = userContextProvider;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantViewModel>> GetAllRestaurantsAsync()
        {
             IQueryable<Restaurant> restaurant = await this.repository.GetItemsAsync<Restaurant>();
             IEnumerable<RestaurantViewModel> result = restaurant
                 .OrderByDescending(r => r.AverageRating)
                 .Select(r => this.mapper.Map<RestaurantViewModel>(r));
             return result;
        }

        public async Task<RestaurantViewModel> CreateRestaurantAsync(CreateRestaurantDto dto)
        {
            UserContext userContext = this.userContextProvider.GetUserContext();
            Guid id = Guid.NewGuid();
            Restaurant restaurant = new Restaurant()
            {
                Id = id,
                Name = dto.Name,
                CreatedAt = DateTime.Now,
                CreatedBy = userContext.Id,
                RolesAllowedToRead = new[] { Constants.UserRole, Constants.AdminRole, Constants.RestaurantOwnerRole + "_" + id },
                RolesAllowedToWrite = new[] { Constants.AdminRole, Constants.RestaurantOwnerRole + "_" + id },
                IdsAllowedToRead = new[] { userContext.Id },
                IdsAllowedToWrite = new[] { userContext.Id },
            };
            User user = await this.repository.GetItemAsync<User>(u => u.Id.Equals(userContext.Id));
            user.Roles = user.Roles.Concat(new[] { Constants.RestaurantOwnerRole + "_" + id }).Distinct().ToArray();
            await this.repository.UpdateAsync(u => u.Id.Equals(userContext.Id), user);
            await this.repository.SaveAsync(restaurant);
            return this.mapper.Map<RestaurantViewModel>(restaurant);
        }

        public async Task<RestaurantViewModel> GetRestaurantAsync(Guid id)
        {
            Restaurant restaurant = await this.repository.GetItemAsync<Restaurant>(r => r.Id.Equals(id));
            return this.mapper.Map<RestaurantViewModel>(restaurant);
        }

        public async Task<PageResponse<RestaurantViewModel>> GetAllRestaurantsAsync(int pageSize, int pageNumber, double lowerBound, double upperBound)
        {
            UserContext userContext = this.userContextProvider.GetUserContext();
            IQueryable<Restaurant> restaurant = (await this.repository.GetItemsAsync<Restaurant>())
                .Where(r => r.AverageRating >= lowerBound &&
                    r.AverageRating <= upperBound &&
                    (r.RolesAllowedToRead.Any(rr => userContext.Roles.Contains(rr))
                     ||
                     r.IdsAllowedToRead.Contains(userContext.Id))
                )
                .OrderByDescending(r => r.AverageRating);
            int skip = pageSize * pageNumber;
            IEnumerable<RestaurantViewModel> result = restaurant
                .Skip(skip)
                .Take(pageSize)
                .ToArray()
                .Select(r => this.mapper.Map<RestaurantViewModel>(r));
            int totalCount = restaurant.Count();
            return new PageResponse<RestaurantViewModel>()
            {
                Data = result,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalCount = totalCount,
                PageCount = (totalCount / pageSize) + (totalCount % pageSize == 0 ? 0 : 1),
            };
        }

        public async Task<string> GetRestaurantNameAsync(Guid id)
        {
            Restaurant restaurant = await this.repository.GetItemAsync<Restaurant>(r => r.Id.Equals(id));
            return restaurant?.Name;
        }

        public async Task EditRestaurantAsync(EditRestaurantDto editRestaurantDto)
        {
            Restaurant restaurant = await this.repository.GetItemAsync<Restaurant>(r => r.Id.Equals(editRestaurantDto.Id));
            restaurant.Name = editRestaurantDto.Name;
            await this.repository.UpdateAsync(r => r.Id.Equals(editRestaurantDto.Id), restaurant);
        }

        public async Task DeleteRestaurantAsync(Guid id)
        {
            await this.repository.DeleteAsync<Restaurant>(r => r.Id.Equals(id));
            await this.repository.DeleteAsync<Review>(r => r.RestaurantId.Equals(id));
        }
    }
}
