namespace RestaurantsApi.DbSeeder
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.IdentityServer;
    using RestaurantsApi.Models;
    using RestaurantsApi.Repositories;
    using RestaurantsApi.Repositories.Configs;
    using RestaurantsApi.Repositories.Contracts;

    class Program
    {
        private static IConfigurationRoot config;

        static async Task Main(string[] args)
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            ServiceCollection services = new ServiceCollection();

            ConfigureServices(services, config);

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            await Seed(serviceProvider).ConfigureAwait(false);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine("RunTime " + elapsedTime);

            Environment.Exit(0);
        }

        private static void ConfigureServices(ServiceCollection services, IConfigurationRoot config)
        {
            services.AddLogging(cfg => cfg.AddConsole());
            services.AddMemoryCache();
            services.Configure<MongoDbConfig>(config.GetSection(nameof(MongoDbConfig)));
            services.AddScoped<IRepository, MongoRepository>();

            services.AddTransient<IUserClaimsFactory, UserClaimsFactory>();

            services.AddTransient<ICorsPolicyService, InMemoryCorsPolicyService>();
            services.AddScoped<IPasswordHasher<User>, GenericBCryptHasher<User>>();

            services.AddScoped<IClientStore, ClientStore>();
            services.AddScoped<IPersistedGrantStore, PersistedGrantStore>();
            services.AddScoped<IResourceStore, ResourceStore>();

            MongoClassMaps.RegisterClassMaps();
        }

        private static async Task Seed(ServiceProvider serviceProvider)
        {
            await SeedClients(serviceProvider);
            await SeedApiResources(serviceProvider);
            await SeedApiScope(serviceProvider);
            await SeedIdentityResource(serviceProvider);
            await SeedUsers(serviceProvider);
        }

        private static Task SeedIdentityResource(ServiceProvider serviceProvider)
        {
            IRepository repository = serviceProvider.GetRequiredService<IRepository>();
            ILogger<IdentityResourceSeeder> logger = serviceProvider.GetRequiredService<ILogger<IdentityResourceSeeder>>();
            IdentityResourceSeeder seeder = new IdentityResourceSeeder(logger, repository);
            return seeder.SeedAsync();
        }

        private static Task SeedUsers(ServiceProvider serviceProvider)
        {
            IRepository repository = serviceProvider.GetRequiredService<IRepository>();
            ILogger<UserSeeder> logger = serviceProvider.GetRequiredService<ILogger<UserSeeder>>();
            UserSeeder seeder = new UserSeeder(logger, repository);
            return seeder.SeedAsync();
        }

        private static Task SeedApiScope(ServiceProvider serviceProvider)
        {
            IRepository repository = serviceProvider.GetRequiredService<IRepository>();
            ILogger<ApiScopeSeeder> logger = serviceProvider.GetRequiredService<ILogger<ApiScopeSeeder>>();
            ApiScopeSeeder seeder = new ApiScopeSeeder(logger, repository);
            return seeder.SeedAsync();
        }

        private static Task SeedApiResources(ServiceProvider serviceProvider)
        {
            IRepository repository = serviceProvider.GetRequiredService<IRepository>();
            ILogger<ApiResourceSeeder> logger = serviceProvider.GetRequiredService<ILogger<ApiResourceSeeder>>();
            ApiResourceSeeder seeder = new ApiResourceSeeder(logger, repository);
            return seeder.SeedAsync();
        }

        private static Task SeedClients(ServiceProvider serviceProvider)
        {
            IRepository repository = serviceProvider.GetRequiredService<IRepository>();
            ILogger<ClientSeeder> logger = serviceProvider.GetRequiredService<ILogger<ClientSeeder>>();
            ClientSeeder seeder = new ClientSeeder(logger, repository);
            return seeder.SeedAsync();
        }
    }
}
