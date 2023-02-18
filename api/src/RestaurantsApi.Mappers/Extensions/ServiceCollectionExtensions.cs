// ServiceCollectionExtensions.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 11:59 PM
// Updated: 05-07-2021 11:59 PM

namespace RestaurantsApi.Mappers.Extensions
{
    using System;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMapperProfiles(this IServiceCollection services, Type startupType)
        {
            services.AddAutoMapper(startupType);

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ReviewDbModelToReviewViewModel>();
                cfg.AddProfile<UserDbModelToUserViewModel>();
                cfg.AddProfile<RestaurantDbModelToRestaurantViewModel>();
            });
            // only during development, validate your mappings; remove it before release
            config.AssertConfigurationIsValid();

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
