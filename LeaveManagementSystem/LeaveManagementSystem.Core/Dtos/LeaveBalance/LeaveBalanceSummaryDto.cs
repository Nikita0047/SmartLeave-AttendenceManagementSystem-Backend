using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos.LeaveBalance
{
    public class LeaveBalanceSummaryDto
    {
        public Guid LeaveTypeId { get; set; }           
        public string LeaveTypeName { get; set; } = string.Empty;
        public int TotalAllotted { get; set; }    
        public int Used { get; set; }                 
        public int Pending { get; set; }                
        public int Remaining { get; set; }              
        public int Year { get; set; }
    }
}
