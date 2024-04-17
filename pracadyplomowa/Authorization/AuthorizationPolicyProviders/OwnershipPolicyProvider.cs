using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using pracadyplomowa.Authorization.AuthorizationRequirement;

namespace pracadyplomowa.Authorization.AuthorizationPolicyProviders
{

    public class OwnershipPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "Ownership";
        private DefaultAuthorizationPolicyProvider BackupPolicyProvider { get; }

        public OwnershipPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            // ASP.NET Core only uses one authorization policy provider, so if the custom implementation
            // doesn't handle all policies it should fall back to an alternate provider.
            BackupPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() {
            Console.WriteLine("GetDefaultPolicyAsync executed");
            return Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() {
            Console.WriteLine("GetFallbackPolicyAsync executed");
            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                string parameterName = policyName.Substring(POLICY_PREFIX.Length);
                var policy = new AuthorizationPolicyBuilder();
                Console.WriteLine("Parameter name: " + parameterName);
                policy.AddRequirements(new OwnershipRequirement(parameterName));
                return Task.FromResult(policy.Build());
            }
            return BackupPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}