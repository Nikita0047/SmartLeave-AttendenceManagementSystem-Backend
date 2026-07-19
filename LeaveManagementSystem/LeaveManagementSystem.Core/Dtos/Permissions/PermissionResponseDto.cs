using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos.Permissions
{
    public class PermissionResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;         // "leaves.approve"
        public string Description { get; set; } = string.Empty;
    }
}
