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
    public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(rp => rp.Id);
            builder.Property(rp => rp.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // unique constraint — a role can't have the same permission twice
            builder.HasIndex(rp => new { rp.Role, rp.PermissionId }).IsUnique();

            // seed — Employee
            builder.HasData(
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), Role = UserRole.Employee, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000001") }, // leaves.view
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), Role = UserRole.Employee, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000002") }, // leaves.apply
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), Role = UserRole.Employee, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000004") }, // leaves.cancel
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000004"), Role = UserRole.Employee, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000006") }, // attendance.view_own
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000005"), Role = UserRole.Employee, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000008") }, // attendance.checkin

                // seed — Manager (everything Employee has + approve + view_team)
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000006"), Role = UserRole.Manager, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000001") }, // leaves.view
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000007"), Role = UserRole.Manager, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000002") }, // leaves.apply
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000008"), Role = UserRole.Manager, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000003") }, // leaves.approve
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000009"), Role = UserRole.Manager, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000004") }, // leaves.cancel
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000010"), Role = UserRole.Manager, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000006") }, // attendance.view_own
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000011"), Role = UserRole.Manager, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000007") }, // attendance.view_team
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000012"), Role = UserRole.Manager, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000008") }, // attendance.checkin

                // seed — Admin (everything)
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000013"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000001") }, // leaves.view
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000014"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000002") }, // leaves.apply
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000015"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000003") }, // leaves.approve
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000016"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000004") }, // leaves.cancel
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000017"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000005") }, // leaves.export
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000018"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000006") }, // attendance.view_own
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000019"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000007") }, // attendance.view_team
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000020"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000008") }, // attendance.checkin
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000021"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000009") }, // users.view
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000022"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000010") }, // users.create
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000023"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000011") }, // users.edit
                new RolePermission { Id = Guid.Parse("10000000-0000-0000-0000-000000000024"), Role = UserRole.Admin, PermissionId = Guid.Parse("00000000-0000-0000-0000-000000000012") }  // leavetypes.manage
            );
        }
    }

}
