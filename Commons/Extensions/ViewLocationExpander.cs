using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Commons.Extensions 
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {

            return new[]
            {
                "/Views/{1}/{0}.cshtml",
                "/Views/Shared/{0}.cshtml",
                "/Areas/{2}/Views/{1}/{0}.cshtml",
                "/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            //nothing to do here.  
        }
    }
}
