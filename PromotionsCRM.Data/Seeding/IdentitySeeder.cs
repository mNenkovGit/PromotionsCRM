using Microsoft.AspNetCore.Identity;
using PromotionsCRM.Common;

namespace PromotionsCRM.Data.Seeding
{
    public class IdentitySeeder
    {
        private static readonly string[] DefaultRoles =
        {
            RoleNames.Administrator,
            RoleNames.User
        };

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (string roleName in DefaultRoles)
            {
                if (await roleManager.RoleExistsAsync(roleName))
                {
                    continue;
                }

                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(roleName));

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to create role '{roleName}': {GetErrors(result)}");
                }
            }
        }

        public static async Task SeedUserWithRoleAsync(
            UserManager<IdentityUser> userManager,
            string email,
            string password,
            string roleName)
        {
            IdentityUser? user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                IdentityResult createResult = await userManager.CreateAsync(user, password);

                if (!createResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to create user '{email}': {GetErrors(createResult)}");
                }
            }

            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                IdentityResult roleResult = await userManager.AddToRoleAsync(user, roleName);

                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to add user '{email}' to role '{roleName}': {GetErrors(roleResult)}");
                }
            }
        }

        private static string GetErrors(IdentityResult result)
            => string.Join(", ", result.Errors.Select(e => e.Description));
    }
}

