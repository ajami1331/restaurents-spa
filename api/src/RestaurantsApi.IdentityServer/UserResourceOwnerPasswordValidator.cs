namespace RestaurantsApi.IdentityServer
{
    using System.Threading.Tasks;
    using IdentityModel;
    using IdentityServer4.Models;
    using IdentityServer4.Validation;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using RestaurantsApi.Models;
    using RestaurantsApi.Repositories.Contracts;

    public class UserResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ISystemClock _clock;
        private readonly IRepository _mongoRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserClaimsFactory _userClaimsFactory;

        public UserResourceOwnerPasswordValidator(
            ISystemClock clock,
            IRepository mongoRepository,
            IPasswordHasher<User> passwordHasher,
            IUserClaimsFactory userClaimsFactory)
        {
            _clock = clock;
            _mongoRepository = mongoRepository;
            _passwordHasher = passwordHasher;
            _userClaimsFactory = userClaimsFactory;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            User user = await _mongoRepository.GetItemAsync<User>(u => u.UserName.Equals(context.UserName)).ConfigureAwait(false);

            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "user not found");

                return;
            }

            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, context.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "password verification failed");

                return;
            }

            context.Result = new GrantValidationResult(
                user.Id.ToString(),
                OidcConstants.AuthenticationMethods.Password,
                _clock.UtcNow.UtcDateTime,
                _userClaimsFactory.GetUserClaims(user));
        }
    }
}
