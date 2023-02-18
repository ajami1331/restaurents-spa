// ClientSeeder.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 4:36 PM
// Updated: 24-06-2021 4:36 PM

namespace RestaurantsApi.DbSeeder
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IdentityServer4.Models;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.Models;
    using RestaurantsApi.Repositories.Contracts;

    public class UserSeeder
    {
        private readonly IRepository _repository;
        private readonly ILogger<UserSeeder> _logger;

        public UserSeeder(ILogger<UserSeeder> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task SeedAsync()
        {
            await _repository.DeleteAsync<User>(c => true);
            await _repository.SaveAsync(Get());
        }

        private static IEnumerable<User> Get()
        {
            return new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("1qazZAQ!"),
                    Roles = new [] { "user" },
                    UserName = "user",
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("1qazZAQ!"),
                    Roles = new [] { "restaurant_owner" },
                    UserName = "restaurant_owner",
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("1qazZAQ!"),
                    Roles = new [] { "admin" },
                    UserName = "admin",
                },
            };
        }
    }
}
