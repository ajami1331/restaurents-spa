namespace Is4.Demo.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityModel;
    using IdentityServer4.Models;
    using IdentityServer4.Validation;

    public class OfflineAccessRequestedValidator : ICustomTokenRequestValidator
    {
        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            ValidatedTokenRequest req = context.Result.ValidatedRequest;
            if (req.GrantType == GrantType.ResourceOwnerPassword &&
                !req.RequestedScopes.Contains(OidcConstants.StandardScopes.OfflineAccess))
            {
                context.Result = new TokenRequestValidationResult(req,
                    $"'{OidcConstants.StandardScopes.OfflineAccess}' should be requested in '{GrantType.ResourceOwnerPassword}' grant flow.");
            }

            return Task.CompletedTask;
        }
    }
}
