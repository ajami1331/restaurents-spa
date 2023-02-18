// UserClaimsFactory.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 3:07 PM
// Updated: 24-06-2021 3:07 PM

namespace RestaurantsApi.IdentityServer
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using IdentityModel;
    using RestaurantsApi.Common.Extensions;
    using RestaurantsApi.Models;

    public class UserClaimsFactory : IUserClaimsFactory
    {
        public IEnumerable<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.AddClaim(JwtClaimTypes.Name, user.DisplayName);
            claims.AddClaim(JwtClaimTypes.PreferredUserName, user.UserName);
            foreach (var userRole in user.Roles)
            {
                claims.AddClaim(JwtClaimTypes.Role, userRole);
            }

            return claims;
        }
    }
}
