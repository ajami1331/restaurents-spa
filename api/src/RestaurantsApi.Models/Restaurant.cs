// Restaurants.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 5:58 PM
// Updated: 24-06-2021 5:58 PM

namespace RestaurantsApi.Models
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;
    using RestaurantsApi.Models.Contracts;

    [BsonIgnoreExtraElements]
    public class Restaurant : IEntity, IRowLevelSecurity
    {
        [BsonId]
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public IEnumerable<string> RolesAllowedToRead { get; set; }

        public IEnumerable<string> RolesAllowedToWrite { get; set; }

        public IEnumerable<Guid> IdsAllowedToRead { get; set; }

        public IEnumerable<Guid> IdsAllowedToWrite { get; set; }

        public string Name { get; set; }

        public double AverageRating { get; set; }

        public long RatingSum { get; set; }

        public long ReviewCount { get; set; }
    }
}
