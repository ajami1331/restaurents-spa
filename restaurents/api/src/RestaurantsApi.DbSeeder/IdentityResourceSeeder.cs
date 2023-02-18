// ClientSeeder.cs
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

    public class IdentityResourceSeeder
    {
        private readonly IRepository _repository;
        private readonly ILogger<IdentityResourceSeeder> _logger;

        public IdentityResourceSeeder(ILogger<IdentityResourceSeeder> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task SeedAsync()
        {
            await _repository.DeleteAsync<IdentityResource>(c => true);
            await _repository.SaveAsync(Get());
        }

        private static IEnumerable<IdentityResource> Get()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"},
                },
            };
        }
    }
}
