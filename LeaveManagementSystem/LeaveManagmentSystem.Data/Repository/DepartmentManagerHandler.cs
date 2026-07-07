using LeaveManagementSystem.Core.Authorization;
using LeaveManagementSystem.Core.Entities;
using LeaveManagmentSystem.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository
{
    public class DepartmentManagerHandler
     : AuthorizationHandler<DepartmentManagerRequirement, LeaveRequest>
    {
        private readonly IUserRepo _userRepo;

        public DepartmentManagerHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DepartmentManagerRequirement requirement,
            LeaveRequest resource)
        {
            // Admin bypasses department check — can act on anyone
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return;
            }

            // Replace this line:
            // var userIdClaim =  context.User.FindFirst().Value(ClaimTypes.NameIdentifier);
            // With the following:
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var managerId))
                return;

            // Issue 1 fix — Employee nav property must be loaded
            if (resource.Employee is null)
                return;

            // Issue 3 fix — manager must exist and be active
            var manager = await _userRepo.GetByIdWithDepartmentAsync(managerId);
            if (manager is null)
                return;

            // core check — manager's dept must match employee's dept
            if (manager.DepartmentId == resource.Employee.DepartmentId)
                context.Succeed(requirement);
        }
    }
}
