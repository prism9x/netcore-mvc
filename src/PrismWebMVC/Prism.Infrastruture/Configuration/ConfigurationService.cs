using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                .AddEntityFrameworkStores<PrismDbContext>();
        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
