using Microsoft.AspNetCore.Identity;
using PromotionsCRM.Common;
using PromotionsCRM.Data.Seeding;

namespace PromotionsCRM.Web.Infrastructure.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task SeedIdentityAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;

            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            await IdentitySeeder.SeedRolesAsync(roleManager);

            await SeedConfiguredUserAsync(app.Configuration, userManager, "Administrator", RoleNames.Administrator);
            await SeedConfiguredUserAsync(app.Configuration, userManager, "TestUser", RoleNames.User);
        }

        private static async Task SeedConfiguredUserAsync(
            IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            string sectionName,
            string roleName)
        {
            string? email = configuration[$"UserSeed:{sectionName}:Email"];
            string? password = configuration[$"UserSeed:{sectionName}:Password"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            await IdentitySeeder.SeedUserWithRoleAsync(userManager, email, password, roleName);
        }
    }
}
