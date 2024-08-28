using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Commons.Controllers;
using Commons.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Commons.Services
{
    public class ControllerDto
    {
        public string Area { get; set; }
        public string Name { get; set; }
        public List<string> Actions { get; set; }
    }

    public static class AssemblyService
    {
        public static List<CommonsRoute> GetRoutes()
        {
            List<CommonsRoute> routes = new List<CommonsRoute>();
            Assembly asm = Assembly.GetEntryAssembly();

            var controllers = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type) || typeof(BaseController).IsAssignableFrom(type))
                .ToList();

            foreach (Type controller in controllers)
            {
                var actionsList = asm.GetTypes()
                    .Where(type => typeof(Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any() && m.DeclaringType.Name == controller.Name)
                    .ToList();

                foreach (var action in actionsList)
                {
                    var area = controller.Namespace.Split('.').Reverse().Skip(1).First();

                    if (area == asm.GetName().Name) area = null;

                    var route = new CommonsRoute()
                    {
                        Area = area,
                        Controller = controller.Name.Replace("Controller", ""),
                        Action = action.Name,
                    };
                    routes.Add(route);
                }
            }

            routes = AddCommonsRoutes(routes);

            return routes;
        }

        private static List<CommonsRoute> AddCommonsRoutes(List<CommonsRoute> list)
        {
            list.Add(new CommonsRoute("/SecurityRoles/Index"));
            list.Add(new CommonsRoute("/SecurityRoles/_Create"));
            list.Add(new CommonsRoute("/SecurityRoles/Edit"));
            list.Add(new CommonsRoute("/SecurityRoles/_AddFunction"));
            list.Add(new CommonsRoute("/SecurityRoles/RemoveFunction"));
            list.Add(new CommonsRoute("/SecurityRoles/_Assign"));
            list.Add(new CommonsRoute("/SecurityRoles/RolesDataTable"));

            return list;
        }
    }
}
