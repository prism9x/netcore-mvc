using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.DataAccess.Data;
using Prism.Domain.Entities;
using Prism.Domain.Setting;

namespace Prism.DataAccess.Configuration
{
    public static class ConfigurationService
    {
        /// <summary>
        /// Tự động Migration Database
        /// </summary>
        /// <param name="webApplication"></param>
        /// <returns></returns>
        public static async Task AutoMigration(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<PrismDbContext>();

                // Phương thức này kiểm tra xem trong Db có tồn tại không nếu không thì sẽ tự tạo mới
                // Trong môi trường dev thì không nên sử dụng phương thức này
                //appContext.Database.EnsureCreated();
                await appContext.Database.MigrateAsync(); // Tự chạy các file Migration
            }
        }

        /// <summary>
        /// Seed Data cho Website
        /// </summary>
        /// <param name="webApplication"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task SeedData(this WebApplication webApplication, IConfiguration configuration)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var defaultUser = configuration.GetSection("SeedUser")?.Get<DefaultUser>() ?? new DefaultUser();
                var defaultRole = configuration.GetValue<string>("DefaultRole") ?? "SuperAdmin";

                try
                {
                    if (!await roleManager.RoleExistsAsync(defaultRole))
                    {
                        // Tạo Role
                        await roleManager.CreateAsync(new IdentityRole(defaultRole));
                    }

                    var existUser = await userManager.FindByNameAsync(defaultUser.UserName);
                    if (existUser == null)
                    {
                        var user = new AppUser
                        {
                            UserName = defaultUser.UserName,
                            Email = defaultUser.Email,
                            NormalizedEmail = defaultUser.Email?.ToUpper(),
                            IsActive = true,
                            AccessFailedCount = 0
                        };

                        // Tạo mới User Admin
                        var identityUser = await userManager.CreateAsync(user, defaultUser.Password ?? "Admin@123");

                        if (identityUser.Succeeded)
                        {
                            // Add Role
                            await userManager.AddToRoleAsync(user, defaultRole);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}