using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Identity
{
    public class CommonsRoute
    {
        public CommonsRoute()
        {
            
        }

        public CommonsRoute(string route)
        {
            string[] split = route.Split('/');
            
            int index = split.Length;

            if (index < 2) return;

            Action = split[index - 1];
            Controller = split[index - 2];

            if (index >2)
            {
                if (split[index - 3] != "None" && split[index - 3] != null)
                {
                    Area = split[index - 3];
                }
            }


        }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Values { get; set; }
        
        /// <summary>
        /// Calculated property.
        /// </summary>
        public string Url
        {
            get
            {
                if (string.IsNullOrEmpty(Controller))
                {
                    return $"/{Action}";
                }
                if (string.IsNullOrEmpty(Area))
                {
                    return $"/{Controller}/{Action}";
                }
                else
                {
                    return $"/{Area}/{Controller}/{Action}";
                }
            }
        }


        public string Hash(string userName)
        {
            return $"{userName}~{Url}";
        }
    }
}
