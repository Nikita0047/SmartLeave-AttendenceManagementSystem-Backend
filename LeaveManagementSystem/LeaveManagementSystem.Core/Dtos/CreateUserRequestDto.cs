using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos
{
    public class CreateUserRequestDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
