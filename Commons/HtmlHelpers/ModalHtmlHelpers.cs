using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Commons.HtmlHelpers
{
    public enum UiColor
    {
        Default,
        Success,
        Danger,
        Warning,
        Info
    }

    public enum ModalFormButtons
    {
        Aceptar,
        CancelarAceptar,
        CerrarEnviar,
        None
    }

    public enum ModalSizes
    {
        Large,
        Medium,
        Small
    }

    public static class ModalHtmlHelpers
    {
        public static HtmlString ModalViewAsync(this IHtmlHelper html, string title, string functionName, string url, UiColor color = UiColor.Default, ModalFormButtons footer = ModalFormButtons.Aceptar, ModalSizes size = ModalSizes.Medium)
        {
            string colorString;
            string sizeString;
            string leftBtnClass = "class=\"btn btn-outline pull-left\"";
            string rightBtnClass = "class=\"btn btn-outline\"";
            string n = Environment.NewLine;

            switch (color)
            {
                case UiColor.Danger:
                    colorString = "danger";
                    break;
                case UiColor.Success:
                    colorString = "success";
                    break;
                case UiColor.Info:
                    colorString = "info";
                    break;
                case UiColor.Warning:
                    colorString = "warning";
                    break;
                default:
                    colorString = "default";
                    leftBtnClass = "class=\"btn btn-default pull-left\"";
                    rightBtnClass = "class=\"btn btn-primary\"";
                    break;
            }

            switch (size)
            {
                case ModalSizes.Large:
                    sizeString = "modal-lg";
                    break;
                case ModalSizes.Small:
                    sizeString = "modal-sm";
                    break;
                default:
                    sizeString = "";
                    break;
            }

            // Si me pasaron el btn con #, se lo saco.
            if (functionName.Substring(0, 1) == "#")
            {
                functionName = functionName.Substring(1, functionName.Length - 1);
            }

            string modalId = functionName + "Modal";

            string htmlString =
                "<!-- MODAL -->" + n +
                "<div class=\"modal modal-" + colorString + " fade\" id=\"" + modalId + "\" style=\"display: none; padding-right: 17px; \">" + n +
                    "<div class=\"modal-dialog " + sizeString + "\">" + n +
                        "<div class=\"modal-content\">" + n +
                            "<div class=\"modal-header\">" + n +
                                 "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">" + n +
                                    "<span aria-hidden=\"true\">×</span>" + n +
                                 "</button>" + n +
                                 "<h4 class=\"modal-title\">" + title + "</h4>" + n +
                            "</div>" + n +
                            "<div class=\"modal-body\">" + n +
                            "</div>" + n;

            // Imprimo el footer que me pidieron.
            switch (footer)
            {
                case ModalFormButtons.CerrarEnviar:
                    htmlString +=
                            "<div class=\"modal-footer\">" + n +
                                "<button type=\"button\" " + leftBtnClass + " data-dismiss=\"modal\">Cerrar</button>" + n +
                                "<button type=\"button\" " + rightBtnClass + ">Enviar</button>" + n +
                            "</div>" + n;
                    break;
                case ModalFormButtons.Aceptar:
                    htmlString +=
                            "<div class=\"modal-footer\">" + n +
                                "<button type=\"button\" " + rightBtnClass + "  data-dismiss=\"modal\">Aceptar</button>" + n +
                            "</div>" + n;
                    break;
                case ModalFormButtons.CancelarAceptar:

                    break;
            }

            htmlString +=
                         "</div>" + n +
                         "<!-- /.modal-content -->" + n +
                    "</div>" + n +
                "<!-- /.modal-dialog -->" + n +
                "</div>" + n +

                "<script type=\"text/javascript\">" + n +
                    "function " + functionName + "(id){" + n +
                        "var $modal = $(\"#" + modalId + "\");" + n +

                        "$modal.find('.modal-body').load('" + url + "' + id, function() {" + n +

                            "$modal.modal('show');" + n +
                         "});" + n +
                     "};" + n +
                "</script>" + n 
                ;

            return new HtmlString(htmlString);
        }
    }
}
