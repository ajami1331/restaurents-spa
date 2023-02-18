// IEntity.cs
// Authors: Araf Al-Jami
// Created: 27-06-2021 3:23 PM
// Updated: 27-06-2021 3:23 PM

namespace RestaurantsApi.Models.Contracts
{
    using System;

    public interface IEntity
    {
        Guid Id { get; set; }

        Guid CreatedBy { get; set; }

        DateTime CreatedAt { get; set; }

        Guid UpdatedBy { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}
