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

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.Property(u => u.PasswordHash).IsRequired();

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasOne(u => u.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // self-referencing manager relationship
            builder.HasOne(u => u.Manager)
                .WithMany(m => m.Subordinates)
                .HasForeignKey(u => u.ManagerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasData(

               new User {
                   Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 
                   FullName = "Admin User",
                   Email = "admin@company.com",
                   PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                   IsActive = true,
                   CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                   Role = UserRole.Admin,
                   DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                   ManagerId = null 
               },
               new User { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                   FullName = "Engineering Manager",
                   Email = "eng.manager@company.com",
                   PasswordHash = BCrypt.Net.BCrypt.HashPassword("Manager@123"), 
                   IsActive = true,
                   CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), 
                   Role = UserRole.Manager, 
                   DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                   ManagerId = null 
               },
               new User { 
                   Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                   FullName = "John Doe", 
                   Email = "john.doe@company.com", 
                   PasswordHash = BCrypt.Net.BCrypt.HashPassword("Employee@123"),
                   IsActive = true, 
                   CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), 
                   Role = UserRole.Employee, 
                   DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                   ManagerId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") }

             );


        }
    }
}
