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
    
public class LeaveTypeConfig : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasKey(lt => lt.Id);
            builder.Property(lt => lt.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(lt => lt.Name).IsRequired().HasMaxLength(100);

            // seed default leave types
            builder.HasData(
                new LeaveType { Id = Guid.Parse("20000000-0000-0000-0000-000000000001"), Name = "Sick Leave", MaxDaysPerYear = 12, IsCarryForwardAllowed = false, IsActive = true },
                new LeaveType { Id = Guid.Parse("20000000-0000-0000-0000-000000000002"), Name = "Casual Leave", MaxDaysPerYear = 15, IsCarryForwardAllowed = false, IsActive = true },
                new LeaveType { Id = Guid.Parse("20000000-0000-0000-0000-000000000003"), Name = "Earned Leave", MaxDaysPerYear = 20, IsCarryForwardAllowed = true, IsActive = true }
            );
        }
    }
}
