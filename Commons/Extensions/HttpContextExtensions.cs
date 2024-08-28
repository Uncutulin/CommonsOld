using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Commons.Identity;
using Commons.Identity.Extensions;
using Commons.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Commons.Extensions
{

    public static class HttpContextExtensions
    {
        private static string _key = "routes_session_key";
        
        public static bool UserHasRoute(this HttpContext context, string controller, string action)
        {
            return UserHasRoute(context, String.Empty, controller, action);
        }

        public static bool UserHasRoute(this HttpContext context, string area, string controller, string action)
        {
            var isAdmin = context.User.IsAdmin();
            if (isAdmin) return true;

            var isWorkSpaceAdmin = context.User.IsWorkSpaceAdmin(context.User.GetWorkSpaceId());
            if (isWorkSpaceAdmin) return true;

            var route = new CommonsRoute()
            {
                Area = area,
                Controller = controller,
                Action = action
            };

            var wsroutes = GetUserRoutes(context).Result;

            return wsroutes.Contains(route.Hash(context.User.Identity.Name));
        }

        public static bool UserHasRoute(this HttpContext context, string url)
        {
            var isAdmin = context.User.IsAdmin();
            if (isAdmin) return true;

            var isWorkSpaceAdmin = context.User.IsWorkSpaceAdmin(context.User.GetWorkSpaceId());
            if (isWorkSpaceAdmin) return true;

            var route = $"{context.User.Identity.Name}~{url}";

            var wsroutes = GetUserRoutes(context).Result;

            return wsroutes.Contains(route);
        }

        private static async Task<IEnumerable<string>> GetUserRoutes(this HttpContext context)
        {
            var session = context.Session;
            var routes = session.GetComplexData<IEnumerable<string>>(_key);

            if (routes == null)
            {

                using (var user = context.RequestServices.GetRequiredService<IUserService>())
                {
                    var roles = await user.GetRolesByUserName(context.User.GetUserName());

                    routes = roles.SelectMany(x => x.RoleFunctions).SelectMany(x => x.Function.GetRoutes()).Select(x => x.Hash(context.User.Identity.Name)).Distinct();
                    
                    session.SetComplexData(_key, routes);
                }

            }

            return routes;
        }

    }
}

