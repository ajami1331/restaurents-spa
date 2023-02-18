// ApiResourcesSeeder.cs
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

    public class ApiResourceSeeder
    {
        private readonly IRepository _repository;
        private readonly ILogger<ApiResourceSeeder> _logger;

        public ApiResourceSeeder(ILogger<ApiResourceSeeder> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task SeedAsync()
        {
            await _repository.DeleteAsync<ApiResource>(c => true);
            await _repository.SaveAsync(Get());
        }

        private static IEnumerable<ApiResource> Get()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "api1",
                    DisplayName = "API #1",
                    Description = "Allow the application to access API #1 on your behalf",
                    Scopes = new List<string> {"api1.read", "api1.write"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"},
                },
            };
        }
    }
}
