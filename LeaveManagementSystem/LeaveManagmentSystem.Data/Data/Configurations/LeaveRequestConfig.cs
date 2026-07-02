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
    // Infrastructure/Data/Configurations/LeaveRequestConfiguration.cs
    public class LeaveRequestConfig : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(lr => lr.Id);
            builder.Property(lr => lr.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(lr => lr.Reason).IsRequired().HasMaxLength(500);
            builder.Property(lr => lr.ReviewRemarks).HasMaxLength(500);

            builder.HasOne(lr => lr.Employee)
                .WithMany(u => u.LeaveRequests)
                .HasForeignKey(lr => lr.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(lr => lr.LeaveType)
                .WithMany(lt => lt.LeaveRequests)
                .HasForeignKey(lr => lr.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(
                 new LeaveRequest
                 {
                     Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                     EmployeeId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                     LeaveTypeId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                     StartDate = new DateOnly(2024, 2, 1),
                     EndDate = new DateOnly(2024, 2, 3),
                     TotalDays = 3,
                     Reason = "Fever and cold",
                     Status = LeaveStatus.Pending,
                     ReviewedBy = null,
                     ReviewRemarks = null,
                     CreatedAt = new DateTime(2024, 1, 25, 0, 0, 0, DateTimeKind.Utc),
                     UpdatedAt = null

                 },
                 new LeaveRequest
                 {
                     Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
                     EmployeeId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                     LeaveTypeId = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                     StartDate = new DateOnly(2024, 2, 5),
                     EndDate = new DateOnly(2024, 2, 7),
                     TotalDays = 3,
                     Reason = "Family function",
                     Status = LeaveStatus.Approved,
                     ReviewedBy = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                     ReviewRemarks = "Approved",
                     CreatedAt = new DateTime(2024, 1, 28, 0, 0, 0, DateTimeKind.Utc),
                     UpdatedAt = new DateTime(2024, 1, 29, 0, 0, 0, DateTimeKind.Utc)
                 }
             );
        }
    }
}
