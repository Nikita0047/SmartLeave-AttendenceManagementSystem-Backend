using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos.LeaveRequest
{
    public class LeaveRequestDto
    {
        [Required]
        public Guid LeaveTypeId { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string? Reason { get; set; }
    }
}
