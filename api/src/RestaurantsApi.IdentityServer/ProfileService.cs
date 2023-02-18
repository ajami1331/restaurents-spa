// ProfileService.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 3:01 PM
// Updated: 24-06-2021 3:01 PM

namespace RestaurantsApi.IdentityServer
{
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityModel;
    using IdentityServer4.Extensions;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.Models;
    using RestaurantsApi.Repositories.Contracts;

    public class ProfileService : IProfileService
    {
        private readonly IRepository _mongoRepository;
        private readonly ILogger<ProfileService> _logger;
        private readonly ISystemClock _clock;
        private readonly IUserClaimsFactory _userClaimsFactory;


        public ProfileService(IRepository mongoRepository, ILogger<ProfileService> logger, ISystemClock clock, IUserClaimsFactory userClaimsFactory)
        {
            _mongoRepository = mongoRepository;
            _logger = logger;
            _clock = clock;
            _userClaimsFactory = userClaimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(_logger);
            string subjectId = context.Subject.GetSubjectId();

            if (context.RequestedClaimTypes.Any())
            {
                User user = await _mongoRepository.GetItemAsync<User>(u => u.Id.Equals(subjectId)).ConfigureAwait(false);
                if (user != null)
                    context.AddRequestedClaims(_userClaimsFactory.GetUserClaims(user));
                context.AddRequestedClaims(context.Subject.Claims);
            }

            context.LogIssuedClaims(_logger);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            _logger.LogDebug("IsActive called from: {caller}", context.Caller);

            string subjectId = context.Subject.GetSubjectId();
            User user = await _mongoRepository.GetItemAsync<User>(u => u.Id.Equals(subjectId)).ConfigureAwait(false);
            context.IsActive = user.IsActive;
        }
    }
}
