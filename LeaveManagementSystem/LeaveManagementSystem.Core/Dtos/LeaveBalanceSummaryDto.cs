using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos
{
    public class LeaveBalanceSummaryDto
    {
        public Guid LeaveTypeId { get; set; }           // Guid, not int
        public string LeaveTypeName { get; set; } = string.Empty;
        public int TotalAllottedLeaves { get; set; }    // fixed typo: Allotted
        public int Used { get; set; }                   // PascalCase
        public int Pending { get; set; }                // PascalCase
        public int Remaining { get; set; }              // PascalCase
        public int Year { get; set; }
    }
}
