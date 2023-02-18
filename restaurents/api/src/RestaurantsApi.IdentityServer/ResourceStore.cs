// <copyright file="ResourceStore.cs" company="Shohoz">
// Copyright © 2015-2020 Shohoz. All Rights Reserved.
// </copyright>

namespace RestaurantsApi.IdentityServer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityServer4.Models;
    using IdentityServer4.Stores;
    using RestaurantsApi.Repositories.Contracts;

    public class ResourceStore : IResourceStore
    {
        private readonly IRepository _mongoRepository;

        public ResourceStore(IRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return await _mongoRepository.GetItemsAsync<IdentityResource>(u => scopeNames.Contains(u.Name));
        }

        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            return await _mongoRepository.GetItemsAsync<ApiScope>(u => scopeNames.Contains(u.Name));
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return await _mongoRepository.GetItemsAsync<ApiResource>(u => u.Scopes.Any(s => scopeNames.Contains(s)));
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            return await _mongoRepository.GetItemsAsync<ApiResource>(u => apiResourceNames.Contains(u.Name));
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            IReadOnlyList<IdentityResource> identityResource = (await _mongoRepository.GetItemsAsync<IdentityResource>()).ToList();
            IReadOnlyList<ApiResource> apiResource = (await _mongoRepository.GetItemsAsync<ApiResource>()).ToList();
            IReadOnlyList<ApiScope> apiScopes = (await _mongoRepository.GetItemsAsync<ApiScope>()).ToList();
            return new Resources(identityResource, apiResource, apiScopes);
        }
    }
}
