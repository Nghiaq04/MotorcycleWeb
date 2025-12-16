using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorcycleWeb.Models
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // User đã login nhưng không có quyền
                filterContext.Result = new RedirectResult("~/Admin/Account/AccessDenied");
            }
            else
            {
                // User chưa login → redirect về Login
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}