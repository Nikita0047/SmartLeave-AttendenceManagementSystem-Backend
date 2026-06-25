using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Authorization
{
    public  class HasPermissionRequirement: IAuthorizationRequirement
    {
        public string Permission { get; }   // readonly — no setter

        public HasPermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
