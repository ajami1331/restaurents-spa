// UserContextProvider.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 1:41 PM
// Updated: 05-07-2021 1:41 PM

using System.Security.Principal;
using IdentityServer4.Extensions;

namespace RestaurantsApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using IdentityModel;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.IdentityServer;
    using RestaurantsApi.Services.Abstractions;

    public class UserContextProvider : IUserContextProvider
    {
        private readonly ILogger<UserContextProvider> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserContextProvider(ILogger<UserContextProvider> logger, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        public UserContext GetUserContext()
        {
            ClaimsPrincipal user = this.httpContextAccessor.HttpContext.User;
            IEnumerable<Claim> claims = user.Claims;
            return new UserContext()
            {
                Roles = claims.Where(c => c.Type.Equals(ClaimTypes.Role)).Select(c => c.Value).ToArray(),
                UserName = claims.FirstOrDefault(c => c.Type.Equals(JwtClaimTypes.PreferredUserName))?.Value,
                Name = claims.FirstOrDefault(c => c.Type.Equals(JwtClaimTypes.Name))?.Value,
                Id = Guid.Parse(claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value),
            };
        }
    }
}
