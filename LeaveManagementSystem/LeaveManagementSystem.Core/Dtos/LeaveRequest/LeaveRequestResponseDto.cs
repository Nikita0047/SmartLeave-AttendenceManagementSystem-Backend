using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos.LeaveRequest
{
   public class LeaveRequestResponseDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;

        public Guid LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int TotalDays { get; set; }
        public string Reason { get; set; }=string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? ReviewerName { get; set; }
        public string? ReviewRemark { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}
