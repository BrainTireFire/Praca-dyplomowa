using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using pracadyplomowa.Authorization.AuthorizationRequirement;
using Newtonsoft.Json.Linq;

namespace pracadyplomowa.Authorization.AuthorizationHandlers
{
    public class OwnershipHandler : IAuthorizationHandler
    {
        private readonly AppDbContext _databaseContext;

        public OwnershipHandler(AppDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();
            int userId;
            foreach (Claim claim in context.User.Claims)
            {
                if ("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier".Equals(claim.Type))
                {
                    userId = Int32.Parse(claim.Value);


                    foreach (var requirement in pendingRequirements)
                    {
                        if (requirement is OwnershipRequirement)
                        {
                            int itemId = await readJsonInt((HttpContext)context.Resource, ((OwnershipRequirement)requirement).propertyName);
                            if (_databaseContext
                            .Objects
                            .FirstOrDefault(i => i.Id == itemId)?
                            .R_OwnerId == userId)
                            {
                                context.Succeed(requirement);
                                Console.WriteLine("Succeded Maciej requirement");
                            }
                        }
                    }
                }
            }


            return;
        }

        private async Task<int> readJsonInt(HttpContext httpContext, string key)
        {
            HttpRequestRewindExtensions.EnableBuffering(httpContext.Request);
            var bodyStream = new StreamReader(httpContext.Request.Body, leaveOpen: true);
            var bodyText = await bodyStream.ReadToEndAsync();
            int value = (int)JObject.Parse(bodyText)[key];
            httpContext.Request.Body.Position = 0;
            return value;
        }
    }
}