// Startup.cs
// Authors: Araf Al-Jami
// Created: 23-06-2021 2:16 AM
// Updated: 23-06-2021 6:25 PM

namespace RestaurantsApi.WebService
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using IdentityModel;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using IdentityServer4.Validation;
    using Is4.Demo.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using RestaurantsApi.IdentityServer;
    using RestaurantsApi.Mappers.Extensions;
    using RestaurantsApi.Models;
    using RestaurantsApi.Repositories;
    using RestaurantsApi.Repositories.Configs;
    using RestaurantsApi.Repositories.Contracts;
    using RestaurantsApi.Services;
    using RestaurantsApi.Services.Abstractions;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMapperProfiles(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantsApi.WebService", Version = "v1" });
            });

            services.AddMemoryCache();
            services.AddHealthChecks();

            services.Configure<MongoDbConfig>(this.Configuration.GetSection(nameof(MongoDbConfig)));
            services.AddScoped<IRepository, MongoRepository>();

            services.AddTransient<IUserClaimsFactory, UserClaimsFactory>();

            services.AddTransient<ICorsPolicyService, InMemoryCorsPolicyService>();
            services.AddScoped<IPasswordHasher<User>, GenericBCryptHasher<User>>();

            services.AddScoped<IClientStore, ClientStore>();
            // services.AddScoped<IPersistedGrantStore, PersistedGrantStore>();
            services.AddScoped<IResourceStore, ResourceStore>();
            services.AddHttpContextAccessor();

            IIdentityServerBuilder builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryPersistedGrants()
                .AddProfileService<ProfileService>()
                .AddAppAuthRedirectUriValidator();

            builder.AddResourceOwnerValidator<UserResourceOwnerPasswordValidator>();
            services.AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            builder.Services.AddSingleton<ICustomTokenRequestValidator, OfflineAccessRequestedValidator>();

            builder.AddDeveloperSigningCredential(true);

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.Audience = "api1";
                    options.Authority = "https://localhost:5001";
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                    };
                });

            MongoClassMaps.RegisterClassMaps();
            services.AddScoped<IUserContextProvider, UserContextProvider>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantsApi.WebService v1"));
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder
                .SetIsOriginAllowed(origins => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Add(JwtClaimTypes.PreferredUserName, JwtClaimTypes.PreferredUserName);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Add(JwtClaimTypes.Name, JwtClaimTypes.Name);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseIdentityServer();
        }
    }
}
