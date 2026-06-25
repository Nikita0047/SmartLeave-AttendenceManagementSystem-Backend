using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos
{
   public class UpdateLeaveStatusDto
    {
        [Required]
        public LeaveStatus Status { get; set; }

        public string? Remarks { get; set; }
    }
}
