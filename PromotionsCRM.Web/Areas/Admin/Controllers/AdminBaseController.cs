using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromotionsCRM.Common;
using PromotionsCRM.Web.Controllers;

namespace PromotionsCRM.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleNames.Administrator)]
    public abstract class AdminBaseController : BaseController
    {

    }
}
