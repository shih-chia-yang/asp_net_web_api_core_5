using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace sa_login.Auth
{
    public class ManagerRequirementHandler : AuthorizationHandler<ManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManagerRequirement requirement)
        {
            if(!context.User.HasClaim(c=>c.Type=="CanManaged"))
                return Task.CompletedTask;
            bool isAdmin=Convert.ToBoolean(context.User.FindFirst(c=>c.Type=="CanManaged").Value);
            if(isAdmin==requirement.IsAdmin)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
