using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Html;

namespace Commons.HtmlHelpers
{
    public static class GridButtonHtmlHelper
    {
        public static HtmlString DataTableButton(this IHtmlHelper helper, string url, string title, string icon = "fa fa-circle-o", BoxColor color = BoxColor.Default)
        {
            var colorString = "btn-default";
            switch (color)
            {
                case BoxColor.Danger:
                    colorString = "btn-danger";
                    break;
                case BoxColor.Info:
                    colorString = "btn-info";
                    break;
                case BoxColor.Primary:
                    colorString = "btn-primary";
                    break;
                case BoxColor.Warning:
                    colorString = "btn-warning";
                    break;
                case BoxColor.Success:
                    colorString = "btn-success";
                    break;
            }


            var result = $"`<a class='btn {colorString} btn-xs' data-toggle='tooltip' title='' data-placement='top' data-original-title='{title}' href='{url}'><i class='{icon}'></i></a>`";

            return new HtmlString(result);
        }


    }
}