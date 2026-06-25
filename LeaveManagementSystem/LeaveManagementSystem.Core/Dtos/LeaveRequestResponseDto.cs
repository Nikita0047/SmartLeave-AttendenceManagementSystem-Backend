using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos
{
   public class LeaveRequestResponseDto
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }

        public string LeaveTypeName { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int TotalDays { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string? ReviewerName { get; set; }
        public string? ReviewRemark { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
