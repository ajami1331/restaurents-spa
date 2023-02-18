// <copyright file="ClientStore.cs" company="Shohoz">
// Copyright © 2015-2020 Shohoz. All Rights Reserved.
// </copyright>

namespace RestaurantsApi.IdentityServer
{
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityServer4.Models;
    using IdentityServer4.Stores;
    using RestaurantsApi.Repositories.Contracts;

    public class ClientStore : IClientStore
    {
        private readonly IRepository _mongoRepository;

        public ClientStore(IRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return _mongoRepository.GetItemAsync<Client>(u => u.ClientId.Equals(clientId));
        }
    }
}
