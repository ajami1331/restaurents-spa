// ClaimExtensions.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 3:11 PM
// Updated: 24-06-2021 3:11 PM

namespace RestaurantsApi.Common.Extensions
{
    using System.Collections.Generic;
    using System.Security.Claims;

    public static class ClaimExtensions
    {
        public static List<Claim> AddClaim(this List<Claim> claimList, string claimType, string claimValue)
        {
            if (string.IsNullOrEmpty(claimType) || string.IsNullOrEmpty(claimType))
            {
                return claimList;
            }

            claimList.Add(new Claim(claimType, claimValue));
            return claimList;
        }
    }
}
