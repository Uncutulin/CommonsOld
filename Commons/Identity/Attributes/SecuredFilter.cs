using System.Linq;
using Commons.Extensions;
using Commons.Identity.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Commons.Identity.Attributes
{
    public class SecuredAttribute : TypeFilterAttribute
    {
        public SecuredAttribute() : base(typeof(RouteClaimFilter))
        {
            Order = 2;
        }
    }

    public class RouteClaimFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.FilterDescriptors.Any(x => x.Filter.GetType() == typeof(UnsecuredAttribute))) return;

            var isAdmin = context.HttpContext.User.IsAdmin();
            if (isAdmin) return;

            var isWorkSpaceAdmin = context.HttpContext.User.IsWorkSpaceAdmin(context.HttpContext.User.GetWorkSpaceId());
            if (isWorkSpaceAdmin) return;
            
            var hasClaim = context.HttpContext.UserHasRoute(
                context.RouteData.Values["area"]?.ToString()
                , context.RouteData.Values["controller"].ToString()
                , context.RouteData.Values["action"].ToString()
                );

            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
