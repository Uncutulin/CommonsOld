using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Commons.Models;

namespace Commons.HtmlHelpers
{
    public enum BoxColor
    {
        Default,
        Success,
        Danger,
        Info,
        Warning,
        Primary
    }

    public static class BoxHtmlHelper
    {
        public static HtmlBoxElement BoxDefault(this IHtmlHelper html, int color,
            string css = "")
        {
            return BoxDefault(html, BoxColor.Default);
        }

        public static HtmlBoxElement BoxDefault(this IHtmlHelper html, BoxColor color = BoxColor.Default, string @class = "", string css = "")
        {
            // Colores: 0.Gris, 1.Verde, 2.Rojo, 3.Celeste, 4.Naranja, 5.Azul

            string codigoColor = string.Empty;
            switch (color)
            {
                case BoxColor.Default:
                    codigoColor = string.Empty;
                    break;
                case BoxColor.Success:
                    codigoColor = "box-success";
                    break;
                case BoxColor.Danger:
                    codigoColor = "box-danger";
                    break;
                case BoxColor.Info:
                    codigoColor = "box-info";
                    break;
                case BoxColor.Warning:
                    codigoColor = "box-warning";
                    break;
                case BoxColor.Primary:
                    codigoColor = "box-primary";
                    break;

            }

            @class = " " + @class;

            html.ViewContext.Writer.Write(
                "<div class=\"box " + codigoColor + @class + "\">"
                );

            return new HtmlBoxElement(html.ViewContext);

        }

        public static HtmlBoxElement BoxHeader(this IHtmlHelper html, string titulo, bool minimizar = false, bool cerrar = false, string icono = null, string css = "")
        {
            if (icono != null)
            {
                icono = "<i class=\"" + icono + "\"></i>";
            }
            html.ViewContext.Writer.Write(
                "<div class=\"box-header with-border " + css + " \">" + icono +
                "<h3 class=\"box-title\">" + titulo + "</h3>" +
                "<div class=\"box-tools pull-right\">");
            if (minimizar)
            {
                html.ViewContext.Writer.Write(
                    "<button type=\"button\" class=\"btn btn-box-tool\" data-widget=\"collapse\" data-toggle=\"tooltip\" title=\"Collapse\">" +
                    "<i class=\"fa fa-minus\"></i>" +
                    "</button>"
                );
            }
            if (cerrar)
            {
                html.ViewContext.Writer.Write(
                    "<button type=\"button\" class=\"btn btn-box-tool\" data-widget=\"remove\" data-toggle=\"tooltip\" title=\"Remove\">" +
                    "<i class=\"fa fa-times\"></i>" +
                    "</button>"
                );
            }
            html.ViewContext.Writer.Write("</div>");
            return new HtmlBoxElement(html.ViewContext);
        }

        public static HtmlBoxElement BoxBody(this IHtmlHelper html, bool padding = true, string css = "")
        {
            string codigoPadding = string.Empty;
            if (!padding)
            {
                codigoPadding = "no-padding";
            }
            html.ViewContext.Writer.Write(
                "<div class=\"box-body " + codigoPadding + " " + css + "\">"
                );
            return new HtmlBoxElement(html.ViewContext);
        }

        public static HtmlBoxElement BoxBodyTable(this IHtmlHelper html, int alturaMax = 0, string css = "", bool padding = false, bool responsive = true)
        {
            html.ViewContext.Writer.Write(
                "<div class=\"box-body "
            );
            if (responsive)
            {
                html.ViewContext.Writer.Write(
                    "table-responsive "
                );
            }
            if (!padding)
            {
                html.ViewContext.Writer.Write(
                    "no-padding "
                );
            }


            html.ViewContext.Writer.Write(
                css + "\" "
            );

            if (alturaMax != 0)
            {
                html.ViewContext.Writer.Write(
                    css + " style=\"max-height: " + alturaMax + "px\" "
                );
            }

            html.ViewContext.Writer.Write(
                "> "
            );


            return new HtmlBoxElement(html.ViewContext);
        }

        public static string BoxTableAsync(this IHtmlHelper html, string url, int altura = 150, int alturaMax = 0,
            string css = "", string loaderName = "divLoader", bool padding = false, bool responsive = true)
        {
            html.ViewContext.Writer.Write(
                "<div class=\"box-body partialContents "
            );
            if (responsive)
            {
                html.ViewContext.Writer.Write(
                    "table-responsive "
                );
            }
            if (!padding)
            {
                html.ViewContext.Writer.Write(
                    "no-padding "
                );
            }

            html.ViewContext.Writer.Write(
                css + "\" "
            );

            if (alturaMax != 0)
            {
                html.ViewContext.Writer.Write(
                    css + " style=\"max-height: " + alturaMax + "px\" "
                );
            }

            html.ViewContext.Writer.Write(
                "data-url=\"" + url + "\"> "
            );

            html.ViewContext.Writer.Write(
                "<div style=\"height: " + altura + "px; width: 100%; clear: both; \"></div> "
            );

            html.ViewContext.Writer.Write(
                "</div> "
            );
            html.ViewContext.Writer.Write(
                "<div id=\"" + loaderName + "\" class=\"overlay\" style=\"display: none; \"> "
            );
            html.ViewContext.Writer.Write(
                "<i class=\"fa fa-refresh fa-spin\"></i> "
            );
            html.ViewContext.Writer.Write(
                "</div> "
            );

            return html.ViewContext.ToString();
        }

        public static HtmlBoxElement BoxFooter(this IHtmlHelper html, string css = "")
        {
            html.ViewContext.Writer.Write(
                "<div class=\"box-footer " + css + "\">"
            );
            return new HtmlBoxElement(html.ViewContext);
        }

        public static HtmlBoxElement BoxPagedFooter<T>(this IHtmlHelper html, string url, TablePage<T> tablePage, string replaceDiv, string loaderDiv = "divLoader", int pagesToShow = 10, string css = "") where T : class, new()
        {
            if (tablePage.PagesCount() == 1) return null;

            html.ViewContext.Writer.Write(
                "<div class=\"box-footer " + css + "\">"
            );
            html.ViewContext.Writer.Write(
                "<ul class=\"pagination pagination-sm no-margin pull-right\">"
            );

            if (!tablePage.IsFirstPage())
            {
                html.ViewContext.Writer.Write(
                    "<li><a data-ajax=\"true\" data-ajax-loading=\"#" + loaderDiv + "\" data-ajax-mode=\"replace\" data-ajax-update=\"#" + replaceDiv + "\"  href=\"" + url + "?pageId=" + tablePage.PreviousPage() + "\">«</a></li>"
                );
            }

            int min = 1, max = tablePage.PagesCount();

            if (tablePage.PagesCount() > pagesToShow)
            {
                int leftPages = pagesToShow / 2;
                int rightPages = pagesToShow - leftPages - 1;

                int mid = tablePage.Number;

                min = mid - leftPages;
                max = mid + rightPages;

                if (max > tablePage.PagesCount())
                {
                    max = tablePage.PagesCount();
                    min = max - pagesToShow;
                }

                if (min < 1)
                {
                    min = 1;
                    max = min + pagesToShow;
                }

            }

            for (int i = min; i <= max; i++)
            {
                html.ViewContext.Writer.Write(
                    "<li"
                );
                html.ViewContext.Writer.Write(
                    i == tablePage.Number ? " class=\"active\"" : " class=\"hidden-xs\""
                );
                html.ViewContext.Writer.Write(
                    "><a data-ajax=\"true\" data-ajax-loading=\"#" + loaderDiv + "\" data-ajax-mode=\"replace\" data-ajax-update=\"#" + replaceDiv + "\"  href=\"" + url + "?pageId=" + i + "\">" + i + "</a></li>"
                );
            }

            if (!tablePage.IsLastPage())
            {
                html.ViewContext.Writer.Write(
                    "<li><a data-ajax=\"true\" data-ajax-loading=\"#" + loaderDiv + "\" data-ajax-mode=\"replace\" data-ajax-update=\"#" + replaceDiv + "\"  href=\"" + url + "?pageId=" + tablePage.NextPage() + "\">»</a></li>"
                );
            }

            html.ViewContext.Writer.Write(
                "</ul>"
            );
            return new HtmlBoxElement(html.ViewContext);
        }

    }

    public class HtmlBoxElement : IDisposable
    {
        private readonly ViewContext _viewContext;
        public HtmlBoxElement(ViewContext viewContext)
        {
            _viewContext = viewContext;
        }
        public void Dispose()
        {
            _viewContext.Writer.Write(
                "</div>"
            );
        }
    }


}