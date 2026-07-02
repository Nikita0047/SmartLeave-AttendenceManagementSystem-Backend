using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos.LeaveType
{
    public class LeaveTypeRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 365)]
        public int MaxDaysPerYear { get; set; }

        public bool IsCarryForwardAllowed { get; set; }

        public bool? IsActive { get; set; }
    }
}
