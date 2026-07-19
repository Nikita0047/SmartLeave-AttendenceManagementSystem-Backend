using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos.LeaveRequest
{
    public class UpdateLeaveStatusDto
    {
        public LeaveStatus Status { get; set; }  // Approved or Rejected
        public string? Remarks { get; set; }     // optional reason
    }
}
