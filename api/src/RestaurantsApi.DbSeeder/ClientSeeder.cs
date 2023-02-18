// ClientSeeder.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 4:36 PM
// Updated: 24-06-2021 4:36 PM

namespace RestaurantsApi.DbSeeder
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IdentityServer4;
    using IdentityServer4.Models;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.Repositories.Contracts;

    public class ClientSeeder
    {
        private readonly IRepository _repository;
        private readonly ILogger<ClientSeeder> _logger;

        public ClientSeeder(ILogger<ClientSeeder> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task SeedAsync()
        {
            await _repository.DeleteAsync<Client>(c => true);
            await _repository.SaveAsync(Get());
        }

        private static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "spa",
                    ClientName = "spa",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string> { "api1.read", "api1.write" },
                    RequireClientSecret = false,
                },
            };
        }
    }
}
