using LeaveManagmentSystem.Data.Data;
using LeaveManagmentSystem.Data.Repository.IRepository;
using LeaveManagmentSystem.Data.Repository.Repo;
using LeaveManagmentSystem.Data.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagmentSystem.Data.Repository
{
    // Infrastructure/DependencyInjection.cs
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // EF Core
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            // repositories
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            services.AddScoped<ILeaveRequestRepo, LeaveRequestRepo>();
            services.AddScoped<ILeaveBalanceRepo, LeaveBalanceRepo>();
            services.AddScoped<ILeaveTypeRepo, LeaveTypeRepo>();
            services.AddScoped<IAttendenceRecoRepo, AttendenceRepo>();
            services.AddScoped<IRolePermissionRepo, RolePermissionRepo>();

            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // auth handlers
            services.AddScoped<IAuthorizationHandler, HasPermissionHandler>();
            services.AddScoped<IAuthorizationHandler, DepartmentManagerHandler>();

            return services;
        }
    }
}
