// MongoRepository.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 2:36 PM
// Updated: 24-06-2021 2:36 PM

namespace RestaurantsApi.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using RestaurantsApi.Repositories.Configs;
    using RestaurantsApi.Repositories.Contracts;

    public class MongoRepository : IRepository
    {
        private readonly MongoDbConfig _mongoDbConfig;
        private readonly ILogger<MongoRepository> _logger;
        private IMongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        private readonly IMemoryCache _memoryCache;

        public MongoRepository(ILogger<MongoRepository> logger, IOptions<MongoDbConfig> mongoDbConfig, IMemoryCache memoryCache)
        {
            _logger = logger;
            _mongoDbConfig = mongoDbConfig.Value;
            _memoryCache = memoryCache;
            this.Initialize();
        }

        public Task<T> GetItemAsync<T>(Expression<Func<T, bool>> dataFilters)
        {
            return _mongoDatabase.GetCollection<T>(typeof (T).Name + "s").Find<T>(dataFilters, (FindOptions) null).SingleOrDefaultAsync<T, T>();
        }

        public Task SaveAsync<T>(T data)
        {
            return _mongoDatabase.GetCollection<T>(typeof (T).Name + "s").InsertOneAsync(data, (InsertOneOptions) null, new CancellationToken());
        }

        public Task SaveAsync<T>(IEnumerable<T> data)
        {
            return _mongoDatabase.GetCollection<T>(typeof (T).Name + "s").InsertManyAsync(data);
        }

        public async Task<IQueryable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> dataFilters)
        {
            return await Task.Run((Func<IQueryable<T>>) (() =>
                    _mongoDatabase.GetCollection<T>(typeof(T).Name + "s").AsQueryable<T>().Where<T>(dataFilters)))
                .ConfigureAwait(false);
        }

        public async Task<IQueryable<T>> GetItemsAsync<T>()
        {
            return await Task.Run((Func<IQueryable<T>>)(() => _mongoDatabase.GetCollection<T>(typeof(T).Name + "s").AsQueryable<T>())).ConfigureAwait(false);
        }

        public Task DeleteAsync<T>(Expression<Func<T, bool>> dataFilters)
        {
            return _mongoDatabase.GetCollection<T>(typeof (T).Name + "s").DeleteManyAsync((FilterDefinition<T>) dataFilters);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _mongoDatabase.GetCollection<T>(typeof(T).Name + "s");
        }

        public Task UpdateAsync<T>(Expression<Func<T, bool>> dataFilters, T data)
        {
            return _mongoDatabase.GetCollection<T>(typeof (T).Name + "s").ReplaceOneAsync((FilterDefinition<T>) dataFilters, data);
        }

        private void Initialize()
        {
            _mongoClient = this.GetMongoClient(_mongoDbConfig.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(_mongoDbConfig.DatabaseName);
        }

        private IMongoClient GetMongoClient(string connectionString)
        {
            if (_memoryCache.TryGetValue<MongoClient>((object) ("MongoCS-" + connectionString), out MongoClient mongoClient))
                return mongoClient;
            mongoClient = new MongoClient(connectionString);
            this.CacheMongoClient(mongoClient, connectionString);
            return mongoClient;
        }

        private void CacheMongoClient(MongoClient mongoClient, string connectionString)
        {
            _memoryCache.Set<MongoClient>((object) ("MongoCS-" + connectionString), mongoClient);
        }
    }
}
