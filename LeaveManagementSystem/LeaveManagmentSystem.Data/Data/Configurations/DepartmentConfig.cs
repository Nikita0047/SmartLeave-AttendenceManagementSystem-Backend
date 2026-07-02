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
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(d => d.Name).IsUnique();

            builder.HasData(
           new Department
           {
               Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
               Name = "Engineering",
               IsActive = true
           },
           new Department
           {
               Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
               Name = "Human Resources",
               IsActive = true
           },
           new Department
           {
               Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
               Name = "Finance",
               IsActive = true
           },
           new Department
           {
               Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
               Name = "Operations",
               IsActive = true
           }
       );
        }
    }
}
