using LeaveManagementSystem.Core.Authorization;
using LeaveManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security;

namespace LeaveManagmentSystem.Data.Data.Configurations
{
    public class PermissionConfig : IEntityTypeConfiguration<Permissions>
    {
        public void Configure(EntityTypeBuilder<Permissions> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(250);

            builder.HasIndex(p => p.Name).IsUnique();

            builder.HasData(
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = PermissionsConst.Leaves.View, Description = "View leave requests" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = PermissionsConst.Leaves.Apply, Description = "Apply for leave" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = PermissionsConst.Leaves.Approve, Description = "Approve or reject leave requests" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = PermissionsConst.Leaves.Cancel, Description = "Cancel own leave request" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = PermissionsConst.Leaves.Export, Description = "Export leave reports" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = PermissionsConst.Attendance.ViewOwn, Description = "View own attendance" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = PermissionsConst.Attendance.ViewTeam, Description = "View team attendance" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = PermissionsConst.Attendance.CheckIn, Description = "Check in and check out" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = PermissionsConst.Users.View, Description = "View all users" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = PermissionsConst.Users.Create, Description = "Create users" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = PermissionsConst.Users.Edit, Description = "Edit users" },
                new Permissions { Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = PermissionsConst.LeaveTypes.Manage, Description = "Manage leave types" }
            );
        }
    }
}