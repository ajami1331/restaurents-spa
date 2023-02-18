// User.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 2:53 PM
// Updated: 24-06-2021 2:53 PM

namespace RestaurantsApi.Models
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;
    using RestaurantsApi.Models.Contracts;

    [BsonIgnoreExtraElements]
    public class User  : IEntity, IRowLevelSecurity
    {
        [BsonId]
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string DisplayName { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string[] Roles { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<string> RolesAllowedToRead { get; set; }

        public IEnumerable<string> RolesAllowedToWrite { get; set; }

        public IEnumerable<Guid> IdsAllowedToRead { get; set; }

        public IEnumerable<Guid> IdsAllowedToWrite { get; set; }
    }
}
