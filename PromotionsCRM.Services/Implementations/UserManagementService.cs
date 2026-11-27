using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data.Repository.Contracts;
using PromotionsCRM.Data.Repository.Implementations;
using PromotionsCRM.Services.DTOs.UserManagement.Request;
using PromotionsCRM.Services.DTOs.UserManagement.Response;
using PromotionsCRM.Services.Interfaces;

namespace PromotionsCRM.Services.Implementations
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ISubmissionRepository _submissionRepository;

        public UserManagementService(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ISubmissionRepository submissionRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _submissionRepository = submissionRepository;
        }

        public async Task<bool> AssignRoleAsync(UserRoleRequestDto request, string currentUserId)
        {
            if (request.UserId == currentUserId)
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return false;
            }
            
            var roleExists  = await _roleManager.RoleExistsAsync(request.RoleName);
            
            if (!roleExists )
            {
                return false;
            }

            bool userAlreadyInRole = await _userManager.IsInRoleAsync(user, request.RoleName);

            if (userAlreadyInRole)
            {
                return false;
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);

            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId, string currentUserId)
        {
            if (userId == currentUserId)
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            bool hasProcessedSubmissions  = await _submissionRepository.AllAsNoTracking()
                .AnyAsync(s => s.ProcessedById == userId);

            if (hasProcessedSubmissions)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            
            return result.Succeeded;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToArrayAsync();

            var userList = new List<UserResponseDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserResponseDto()
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    Roles = roles.ToArray()
                });
            }

            return userList;
        }

        public async Task<bool> RemoveRoleAsync(UserRoleRequestDto request, string currentUserId)
        {
            if (request.UserId == currentUserId)
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                return false;
            }
            
            var roleExists  = await _roleManager.RoleExistsAsync(request.RoleName);
            
            if (!roleExists )
            {
                return false;
            }

            bool isInRole = await _userManager.IsInRoleAsync(user, request.RoleName);

            if (!isInRole)
            {
                return false;
            }

            var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);

            return result.Succeeded;
        }
    }
}
