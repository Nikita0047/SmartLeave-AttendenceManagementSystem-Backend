using LeaveManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Entities
{
    public class AttendenceRecord
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public User Employee { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly? CheckIn { get; set; }
        public TimeOnly? CheckOut { get; set; }
        public double? HoursWorked { get; set; }     // computed on check-out
        public AttendenceStatus Status { get; set; }
    }
}
