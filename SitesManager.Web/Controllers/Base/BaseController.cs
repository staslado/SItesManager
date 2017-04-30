using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SitesManager.Data;
using SitesManager.Web.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace SitesManager.Web.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        private ApplicationUserManager _userManager;

        protected BaseController()
        {
            DbContext = new ApplicationDbContext();
        }

        protected ApplicationDbContext DbContext { get; }

        protected IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        protected ApplicationUserManager UserManager
            => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        protected ActionResult RedirectToLocal(string returnUrl)
            => Url.IsLocalUrl(returnUrl) ? (ActionResult)Redirect(returnUrl) : RedirectToAction("Index", "Sites");

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();

                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}