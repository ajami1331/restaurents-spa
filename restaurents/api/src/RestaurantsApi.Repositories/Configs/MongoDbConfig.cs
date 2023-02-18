// MongoDbConfig.cs
// Authors: Araf Al-Jami
// Created: 23-06-2021 6:24 PM
// Updated: 23-06-2021 6:25 PM

namespace RestaurantsApi.Repositories.Configs
{
    public class MongoDbConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
