// Review.cs
// Authors: Araf Al-Jami
// Created: 27-06-2021 3:27 PM
// Updated: 27-06-2021 3:27 PM

namespace RestaurantsApi.Models
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;
    using RestaurantsApi.Models.Contracts;

    [BsonIgnoreExtraElements]
    public class Review : IEntity, IRowLevelSecurity
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

        public int Rating { get; set; }

        public Guid RestaurantId { get; set; }

        public string ReviewerName { get; set; }

        public string Comment { get; set; }

        public DateTime DateOfVisit { get; set; }

        public string Reply { get; set; }

        public Guid RepliedBy { get; set; }
    }
}
