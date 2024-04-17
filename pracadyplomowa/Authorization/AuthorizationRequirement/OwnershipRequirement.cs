using Microsoft.AspNetCore.Authorization;

namespace pracadyplomowa.Authorization.AuthorizationRequirement
{
    public class OwnershipRequirement : IAuthorizationRequirement
    {

        public OwnershipRequirement(string propertyName) => this.propertyName = propertyName.Trim();

        public string propertyName { get; }
    }
}