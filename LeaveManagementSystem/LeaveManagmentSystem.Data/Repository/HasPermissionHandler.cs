using LeaveManagementSystem.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository
{
    // Infrastructure/Authorization/HasPermissionHandler.cs

   

    public class HasPermissionHandler
        : AuthorizationHandler<HasPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionRequirement requirement)
        {
            // user must be authenticated first
            if (context.User?.Identity?.IsAuthenticated != true)
                return Task.CompletedTask;

            // check if any claim matches type "permission" and the required value
            var hasPermission = context.User.Claims
                .Any(c => c.Type == "permission"
                       && c.Value == requirement.Permission);

            if (hasPermission)
                context.Succeed(requirement);

            // do NOT call context.Fail() here
            // if we don't succeed, other handlers still get a chance to run
            return Task.CompletedTask;
        }
    }
}
