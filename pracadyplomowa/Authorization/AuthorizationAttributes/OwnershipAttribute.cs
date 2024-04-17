using Microsoft.AspNetCore.Authorization;

namespace pracadyplomowa.Authorization.AuthorizationAttributes
{
    public class OwnershipAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "Ownership";

        public OwnershipAttribute(string parameter) => Parameter = parameter;

        public string Parameter { get
        {
            return Policy.Substring(POLICY_PREFIX.Length);
        }
        set
        {
            Policy = $"{POLICY_PREFIX}{value.ToString()}";
        }
        }
    }
}