using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }          // match User.Id
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }       // populate manually from role lookup
        public Guid DepartmentId { get; set; } // match User.DepartmentId
        public string DepartmentName { get; set; } /
        
    }
}
