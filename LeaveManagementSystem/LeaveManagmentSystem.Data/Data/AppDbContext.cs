using LeaveManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;




namespace LeaveManagmentSystem.Data.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option): base(option)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Permissions> Permissions => Set<Permissions>();

        public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();
        public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
        public DbSet<LeaveBalance> LeaveBalances => Set<LeaveBalance>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<AttendenceRecord> AttendanceRecords => Set<AttendenceRecord>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // apply all IEntityTypeConfiguration classes in this assembly
            //if we do not use this then we have to manually register each configuration class
            //e.g : builder.configuration(new AttendenceRecordConfig)
            //but now this will search all the configuration class which will implement IentityConfig interface in this assembly/ current directery 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}

