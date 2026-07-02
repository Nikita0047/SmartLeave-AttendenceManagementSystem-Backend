using LeaveManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Data.Configurations
{
    public class LeaveBalanceConfig : IEntityTypeConfiguration<LeaveBalance>
    {
        public void Configure(EntityTypeBuilder<LeaveBalance> builder)
        {
            builder.HasKey(lb => lb.Id);
            builder.Property(lb => lb.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            // one balance row per employee per leave type per year
            builder.HasIndex(lb => new { lb.EmployeeId, lb.LeaveTypeId, lb.Year }).IsUnique();

            builder.HasOne(lb => lb.Employee)
                .WithMany(u => u.LeaveBalances)
                .HasForeignKey(lb => lb.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(lb => lb.LeaveType)
                .WithMany(lt => lt.LeaveBalances)
                .HasForeignKey(lb => lb.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(

               new LeaveBalance
               {
                   Id = Guid.Parse("40000000-0000-0000-0000-000000000001"),
                   EmployeeId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                   LeaveTypeId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                   Year = 2024,
                   TotalAllotted = 12,
                   Used = 3,
                   Pending = 0
               },
               new LeaveBalance
               {
                   Id = Guid.Parse("40000000-0000-0000-0000-000000000002"),
                   EmployeeId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                   LeaveTypeId = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                   Year = 2024,
                   TotalAllotted = 15,
                   Used = 3,
                   Pending = 0
               },
               new LeaveBalance
               {
                   Id = Guid.Parse("40000000-0000-0000-0000-000000000003"),
                   EmployeeId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                   LeaveTypeId = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                   Year = 2024,
                   TotalAllotted = 20,
                   Used = 0,
                   Pending = 0
               }
            );
        }
    }
}
