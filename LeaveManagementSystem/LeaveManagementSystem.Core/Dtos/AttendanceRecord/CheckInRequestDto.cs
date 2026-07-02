using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos.AttendanceRecord
{
    public class CheckInRequestDto
    {
        [MaxLength(250)]
        public string? Notes { get; set; }        // optional — "WFH today"
    }
}
