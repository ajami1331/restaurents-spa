// UserService.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 9:37 PM
// Updated: 05-07-2021 9:37 PM

namespace RestaurantsApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DnsClient.Internal;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.Common;
    using RestaurantsApi.Dtos;
    using RestaurantsApi.IdentityServer;
    using RestaurantsApi.Models;
    using RestaurantsApi.Repositories.Contracts;
    using RestaurantsApi.Services.Abstractions;
    using RestaurantsApi.ViewModels;

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;
        private readonly IRepository repository;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IMapper mapper;
        private readonly IUserContextProvider userContextProvider;

        public UserService(
            ILogger<UserService> logger,
            IRepository repository,
            IPasswordHasher<User> passwordHasher,
            IMapper mapper,
            IUserContextProvider userContextProvider)
        {
            this.logger = logger;
            this.repository = repository;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;
            this.userContextProvider = userContextProvider;
        }

        public async Task<ServiceResponse<UserViewModel>> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (await this.repository.GetItemAsync<User>(u => u.UserName.Equals(createUserDto.UserName)) != null)
            {
                return new ServiceResponse<UserViewModel>("Username already taken");
            }

            Guid id = Guid.NewGuid();
            User user = new User()
            {
                CreatedAt = DateTime.Now,
                CreatedBy = id,
                Id = id,
                UserName = createUserDto.UserName,
                DisplayName = createUserDto.DisplayName,
                IdsAllowedToRead = new[] { id },
                IdsAllowedToWrite = new[] { id },
                IsActive = true,
                PasswordHash = createUserDto.Password,
                Roles = new[] { Constants.UserRole },
                RolesAllowedToRead = new[] { Constants.AdminRole },
                RolesAllowedToWrite = new[] { Constants.AdminRole },
            };
            user.PasswordHash = this.passwordHasher.HashPassword(user, createUserDto.Password);
            await this.repository.SaveAsync(user);
            UserViewModel userViewModel = this.mapper.Map<UserViewModel>(user);
            return new ServiceResponse<UserViewModel>(userViewModel);
        }

        public async Task<PageResponse<UserViewModel>> GetUsersAsync(int pageSize, int pageNumber)
        {
            UserContext userContext = this.userContextProvider.GetUserContext();
            IQueryable<User> users = (await this.repository.GetItemsAsync<User>())
                .Where(r =>
                            (r.RolesAllowedToRead.Any(rr => userContext.Roles.Contains(rr))
                             ||
                             r.IdsAllowedToRead.Contains(userContext.Id))
                );
            int skip = pageSize * pageNumber;
            IEnumerable<UserViewModel> result = users
                .Skip(skip)
                .Take(pageSize)
                .ToArray()
                .Select(r => this.mapper.Map<UserViewModel>(r));
            int totalCount = users.Count();
            return new PageResponse<UserViewModel>()
            {
                Data = result,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalCount = totalCount,
                PageCount = (totalCount / pageSize) + (totalCount % pageSize == 0 ? 0 : 1),
            };
        }

        public async Task<UserViewModel> GetUserAsync(Guid id)
        {
            UserContext userContext = this.userContextProvider.GetUserContext();
            User user = (await this.repository.GetItemsAsync<User>())
                .FirstOrDefault(r => r.Id.Equals(id) &&
                    (r.RolesAllowedToRead.Any(rr => userContext.Roles.Contains(rr))
                     ||
                     r.IdsAllowedToRead.Contains(userContext.Id))
                );
            return this.mapper.Map<UserViewModel>(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            UserContext userContext = this.userContextProvider.GetUserContext();
            await this.repository.DeleteAsync<User>(r => r.Id.Equals(id) &&
                                                                     (r.RolesAllowedToRead.Any(rr => userContext.Roles.Contains(rr))
                                                                      ||
                                                                      r.IdsAllowedToRead.Contains(userContext.Id)));

        }

        public async Task EditUserAsync(EditUserDto editUserDto)
        {
            User user = await this.repository.GetItemAsync<User>(r => r.Id.Equals(editUserDto.Id));
            user.UserName = editUserDto.UserName;
            user.DisplayName = editUserDto.DisplayName;
            user.Roles = editUserDto.Roles;
            await this.repository.UpdateAsync(r => r.Id.Equals(editUserDto.Id), user);
        }
    }
}
