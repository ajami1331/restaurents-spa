// <copyright file="PersistedGrantStore.cs" company="Shohoz">
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

    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly IRepository _mongoRepository;

        public PersistedGrantStore(IRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            return _mongoRepository.SaveAsync<PersistedGrant>(grant);
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            return _mongoRepository.GetItemAsync<PersistedGrant>(u => u.Key.Equals(key));
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            return (await _mongoRepository.GetItemsAsync<PersistedGrant>()).ToList();
        }

        public Task RemoveAsync(string key)
        {
            return _mongoRepository.DeleteAsync<PersistedGrant>(u => u.Key.Equals(key));
        }

        public Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            string clientId = filter.ClientId;
            string subjectId = filter.SubjectId;
            string type = filter.Type;
            return _mongoRepository.DeleteAsync<PersistedGrant>(u => u.SubjectId.Equals(subjectId)
                                                                     && u.ClientId.Equals(clientId)
                                                                     && u.Type.Equals(type));
        }
    }
}
