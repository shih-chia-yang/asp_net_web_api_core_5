using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace sa_login.Auth.PayExpense
{
    public class ManagerPayExpenseRequirementHandler : AuthorizationHandler<ManagerPayExpenseRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManagerPayExpenseRequirement requirement)
        {
            if(!context.User.HasClaim(c=>c.Type =="HasExpenseCredit"))
                return Task.CompletedTask;
            var payExpense= bool.Parse(context.User.FindFirst(c=>c.Type == "HasExpenseCredit").Value);
            if(payExpense)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}