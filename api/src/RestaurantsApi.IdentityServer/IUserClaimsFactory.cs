// IUserClaimsFactory.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 3:09 PM
// Updated: 24-06-2021 3:09 PM

namespace RestaurantsApi.IdentityServer
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using RestaurantsApi.Models;

    public interface IUserClaimsFactory
    {
        IEnumerable<Claim> GetUserClaims(User user);
    }
}
