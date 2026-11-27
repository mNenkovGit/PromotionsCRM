using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PromotionsCRM.Common;
using PromotionsCRM.Services.DTOs.UserManagement.Request;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Admin;

namespace PromotionsCRM.Web.Areas.Admin.Controllers
{
    public class UserManagementController : AdminBaseController
    {
        private readonly IUserManagementService _userManagementService;

        private readonly ILogger<UserManagementController> _logger;

        public UserManagementController(IUserManagementService userManagementService, ILogger<UserManagementController> logger)
        {
            _userManagementService = userManagementService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usersDto = await _userManagementService.GetAllUsersAsync();
            var usersViewModel = usersDto
                .Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Roles = u.Roles,
                })
                .ToArray();

            return View(usersViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> AssignRole(UserRoleInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[NotificationKeys.ErrorMessage] = "Please select a role!";
                return RedirectToAction(nameof(Index));
            }

            var currentUserId = GetUserId();

            if (currentUserId == null)
            {
                return Forbid();
            }

            try
            {
                var requestDto = new UserRoleRequestDto()
                {
                    UserId = inputModel.UserId,
                    RoleName = inputModel.RoleName,
                };

                bool isAssigned = await _userManagementService.AssignRoleAsync(requestDto, currentUserId);

                if (isAssigned)
                {
                    TempData[NotificationKeys.SuccessMessage] = "Role assigned successfully!";
                }
                else
                {
                    TempData[NotificationKeys.ErrorMessage] = "The role could not be assigned.";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while assigning role {RoleName} to user {UserId}", inputModel.RoleName, inputModel.UserId);
                TempData[NotificationKeys.ErrorMessage] = "An unexpected error occurred while assigning the role.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(UserRoleInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[NotificationKeys.ErrorMessage] = "Please select a role!";
                return RedirectToAction(nameof(Index));
            }

            var currentUserId = GetUserId();

            if (currentUserId == null)
            {
                return Forbid();
            }

            try
            {
                var requestDto = new UserRoleRequestDto()
                {
                    UserId = inputModel.UserId,
                    RoleName = inputModel.RoleName,
                };

                bool isRemoved = await _userManagementService.RemoveRoleAsync(requestDto, currentUserId);

                if (isRemoved)
                {
                    TempData[NotificationKeys.SuccessMessage] = "Role removed successfully!";
                }
                else
                {
                    TempData[NotificationKeys.ErrorMessage] = "The role could not be removed.";
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing role {RoleName} to user {UserId}", inputModel.RoleName, inputModel.UserId);
                TempData[NotificationKeys.ErrorMessage] = "An unexpected error occurred while removing the role.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                TempData[NotificationKeys.ErrorMessage] = "Invalid user.";
                return RedirectToAction(nameof(Index));
            }

            var currentUserId = GetUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }
            try
            {
                bool isDeleted = await _userManagementService.DeleteUserAsync(userId, currentUserId);

                if (isDeleted)
                {
                    TempData[NotificationKeys.SuccessMessage] = "User deleted successfully!";
                }
                else
                {
                    TempData[NotificationKeys.ErrorMessage] = "The user could not be deleted. Users who have processed submissions cannot be deleted!";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting the user {UserId}", userId);
                TempData[NotificationKeys.ErrorMessage] = "An unexpected error occurred while deleting the user.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
