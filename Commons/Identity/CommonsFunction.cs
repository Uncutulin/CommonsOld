using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace Commons.Identity
{
    public class CommonsFunction : Documental
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RoutesJson { get; set; }
        
        public List<CommonsRoute> GetRoutes()
        {
            if (RoutesJson == null) return new List<CommonsRoute>();
            return JsonConvert.DeserializeObject<List<CommonsRoute>>(RoutesJson);
        }

        public void SetRoutes(List<CommonsRoute> routes)
        {
            RoutesJson = JsonConvert.SerializeObject(routes);
        }

        public void AddRoute(CommonsRoute route)
        {
            var routes = GetRoutes();

            if (routes.All(x => x.Url != route.Url))
            {
                routes.Add(route);
            }

            SetRoutes(routes);
        }
    }

    internal class CommonsFunctionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string LastEditTime { get; set; }
        public bool Show { get; set; }
    }
}
