using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Entities
{
    public class RolePermission
    {
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
        public Guid PermissionId { get; set; }
        public Permissions Permission { get; set; }
    }
}
