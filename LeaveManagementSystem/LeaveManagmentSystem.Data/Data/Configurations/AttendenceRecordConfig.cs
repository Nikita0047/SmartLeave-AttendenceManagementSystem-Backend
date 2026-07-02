using LeaveManagementSystem.Core.Entities;
using LeaveManagementSystem.Core.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Data.Configurations
{
    // Infrastructure/Data/Configurations/AttendanceRecordConfiguration.cs
    public class AttendanceRecordConfiguration : IEntityTypeConfiguration<AttendenceRecord>
    {
        public void Configure(EntityTypeBuilder<AttendenceRecord> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            // one record per employee per day
            builder.HasIndex(a => new { a.EmployeeId, a.Date }).IsUnique();

            builder.HasOne(a => a.Employee)
                .WithMany(u => u.AttendenceRecords)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(
                // John Doe - Checked in and out
                new AttendenceRecord
                {
                    Id = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                    EmployeeId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // John Doe
                    Date = new DateOnly(2024, 1, 15),
                    CheckIn = new TimeOnly(9, 0, 0),
                    CheckOut = new TimeOnly(18, 0, 0),
                    HoursWorked = 9.0,
                    Status = AttendenceStatus.Present
                },

                // John Doe - Absent
                new AttendenceRecord
                {
                    Id = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    EmployeeId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // John Doe
                    Date = new DateOnly(2024, 1, 16),
                    CheckIn = null,
                    CheckOut = null,
                    HoursWorked = null,
                    Status = AttendenceStatus.Absent
                },

                // John Doe - Checked in but not out yet
                new AttendenceRecord
                {
                    Id = Guid.Parse("50000000-0000-0000-0000-000000000003"),
                    EmployeeId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // John Doe
                    Date = new DateOnly(2024, 1, 17),
                    CheckIn = new TimeOnly(9, 30, 0),
                    CheckOut = null,
                    HoursWorked = null,
                    Status = AttendenceStatus.Present
                }
            );
        }
    }
}
