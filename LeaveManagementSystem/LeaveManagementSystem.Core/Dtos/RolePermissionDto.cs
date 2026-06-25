using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos
{
    public class RolePermissionDto
    {
       
        [Required]
        public UserRole Role { get; set; }

        [Required]
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
