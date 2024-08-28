using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Commons.Identity.Attributes
{
    public class UnsecuredAttribute : TypeFilterAttribute
    {
        public UnsecuredAttribute() : base(typeof(UnRouteClaimFilter))
        {
            Order = 1;
        }
    }

    public class UnRouteClaimFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}
