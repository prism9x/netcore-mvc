using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Prism.Application.Abstracts;
using Prism.Application.Services;
using Prism.DataAccess.Data;
using Prism.DataAccess.Repository;
using Prism.Domain.Abstracts;
using Prism.Domain.Entities;

namespace Prism.Infrastruture.Configuration
{
    public static class ConfigurationService
    {
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<PrismDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<PrismDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "PrismCookie";
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.LoginPath = "/admin/authentication/login";
                options.SlidingExpiration = true;

            });


            services.Configure<IdentityOptions>(options => {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 3;
            });

        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IRoleService, RoleService>();


        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        }
    }
}
