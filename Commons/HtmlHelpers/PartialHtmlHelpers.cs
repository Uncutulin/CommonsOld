using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Commons.HtmlHelpers
{
    public static class PartialHelper
    {
        public static HtmlString LoadPartialAsync(string url, int altura = 0, bool hasLoader = false)
        {
            StringBuilder html = new StringBuilder();


            if (!hasLoader) html.Append(
                "<div> "
            );
            if (!hasLoader) html.Append(
                "<div> "
            );
            html.Append(
                "<div class=\"partialContents\" data-url=\"" + url + "\"> "
            );

            if (altura > 0)
            {
                html.Append(
                    "<div style=\"height: " + altura + "px; width: 100%; clear: both; \"></div> "
                );
            }
            
            html.Append(
                "</div> "
            );
            if (!hasLoader) html.Append(
                "</div> "
            );
            if (!hasLoader) html.Append(
                "</div> "
            );

            return new HtmlString(html.ToString());
        }
    }
}
