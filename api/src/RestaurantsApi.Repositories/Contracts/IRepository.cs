// IRepository.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 2:35 PM
// Updated: 24-06-2021 2:35 PM

using System.Collections.Generic;
using MongoDB.Driver;

namespace RestaurantsApi.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository
    {
        Task<T> GetItemAsync<T>(Expression<Func<T, bool>> dataFilters);

        Task SaveAsync<T>(T data);

        Task SaveAsync<T>(IEnumerable<T> data);

        Task<IQueryable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> dataFilters);

        Task<IQueryable<T>> GetItemsAsync<T>();

        Task DeleteAsync<T>(Expression<Func<T, bool>> dataFilters);

        IMongoCollection<T> GetCollection<T>();

        Task UpdateAsync<T>(Expression<Func<T, bool>> dataFilters, T data);
    }
}
