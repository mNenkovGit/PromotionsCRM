using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PromotionsCRM.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public abstract class BaseController : Controller
    {
        protected string? GetUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier);

        protected string? GetUserEmail()
            => User.FindFirstValue(ClaimTypes.Email);

        protected bool IsUserAuhtenticated
            => User.Identity?.IsAuthenticated ?? false;
    }
}
