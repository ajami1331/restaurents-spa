// MongoClassMaps.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 5:01 PM
// Updated: 24-06-2021 5:01 PM

namespace RestaurantsApi.IdentityServer
{
    using System.Security.Claims;
    using IdentityServer4.Models;
    using Microsoft.AspNetCore.Identity;
    using MongoDB.Bson.Serialization;
    using RestaurantsApi.Models;

    public static class MongoClassMaps
    {
        public static void RegisterClassMaps()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Client)))
            {
                BsonClassMap.RegisterClassMap<Client>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(client => client.ClientId);
                    cm.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Resource)))
            {
                BsonClassMap.RegisterClassMap<Resource>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(client => client.Name);
                    cm.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(ApiResource)))
            {
                BsonClassMap.RegisterClassMap<ApiResource>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(IdentityResource)))
            {
                BsonClassMap.RegisterClassMap<IdentityResource>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(PersistedGrant)))
            {
                BsonClassMap.RegisterClassMap<PersistedGrant>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(client => client.Key);
                    cm.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(IdentityUser<string>)))
            {
                BsonClassMap.RegisterClassMap<IdentityUser<string>>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(client => client.Id);
                    cm.SetIgnoreExtraElements(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(ClaimsIdentity)))
            {
                BsonClassMap.RegisterClassMap<ClaimsIdentity>(cm => { cm.AutoMap(); });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Claim)))
            {
                BsonClassMap.RegisterClassMap<Claim>(cm =>
                {
                    cm.MapProperty(claim => claim.Type);
                    cm.MapProperty(claim => claim.Value);
                    cm.MapProperty(claim => claim.ValueType);
                    cm.MapProperty(claim => claim.Issuer);
                    cm.MapProperty(claim => claim.OriginalIssuer);
                    cm.MapCreator(c => new Claim(c.Type, c.Value, c.ValueType, c.Issuer, c.OriginalIssuer));
                });
            }
        }
    }
}
