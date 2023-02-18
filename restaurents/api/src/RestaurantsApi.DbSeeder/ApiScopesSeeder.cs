// ApiScopesSeeder.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 4:36 PM
// Updated: 24-06-2021 4:36 PM

namespace RestaurantsApi.DbSeeder
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IdentityServer4.Models;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.Repositories.Contracts;

    public class ApiScopeSeeder
    {
        private readonly IRepository _repository;
        private readonly ILogger<ApiScopeSeeder> _logger;

        public ApiScopeSeeder(ILogger<ApiScopeSeeder> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task SeedAsync()
        {
            await _repository.DeleteAsync<ApiScope>(c => true);
            await _repository.SaveAsync(Get());
        }

        private static IEnumerable<ApiScope> Get()
        {
            return new[]
            {
                new ApiScope("api1.read", "Read Access to API #1"),
                new ApiScope("api1.write", "Write Access to API #1"),
            };
        }
    }
}
