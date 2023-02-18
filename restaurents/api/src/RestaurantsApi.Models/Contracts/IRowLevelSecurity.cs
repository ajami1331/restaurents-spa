// IRowLevelSecurity.cs
// Authors: Araf Al-Jami
// Created: 27-06-2021 3:21 PM
// Updated: 27-06-2021 3:21 PM

namespace RestaurantsApi.Models.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IRowLevelSecurity
    {
        IEnumerable<string> RolesAllowedToRead { get; set; }

        IEnumerable<string> RolesAllowedToWrite { get; set; }

        IEnumerable<Guid> IdsAllowedToRead { get; set; }

        IEnumerable<Guid> IdsAllowedToWrite { get; set; }
    }
}
