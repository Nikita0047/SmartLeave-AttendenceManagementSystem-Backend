using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos.LeaveBalance
{
    public class AdjustLeaveBalanceDto
    {
        [Required]
        public Guid LeaveTypeId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int AdjustedTotal { get; set; }    // new TotalAllotted value

        [MaxLength(250)]
        public string? Reason { get; set; }       // audit note for the adjustment
    }
}
