using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Super.Paula.Authorization
{
    public class IdentityClaimResourceHandler : AuthorizationHandler<IdentityClaimResourceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdentityClaimResourceRequirement requirement)
        {
            var isAuthorized = context.User.HasAuthorizations(requirement.Authorizations);

            if (isAuthorized)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (context.Resource is KeyValuePair<string, object> resource &&
                resource.Key == "identity")
            {
                var identityClaim = context.User.FindFirst("Identity");

                if (identityClaim != null &&
                    identityClaim.Value.Equals(resource.Value))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}