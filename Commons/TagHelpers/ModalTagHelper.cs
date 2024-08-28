using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Commons.TagHelpers
{
    public enum UiColor
    {
        Default,
        Success,
        Danger,
        Warning,
        Info,
        Primary,
        None
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

    [HtmlTargetElement("modal")]
    public class ModalTagHelper : TagHelper
    {
        [HtmlAttributeName("title")]
        public string Title { get; set; }

        [HtmlAttributeName("class")]
        public string Class { get; set; }

        [HtmlAttributeName("style")]
        public string Style { get; set; }

        [HtmlAttributeName("function")]
        public string Function { get; set; }

        [HtmlAttributeName("load-url")]
        public string Url { get; set; }

        [HtmlAttributeName("color")]
        public UiColor Color { get; set; } = UiColor.Default;

        [HtmlAttributeName("size")]
        public ModalSizes Sizes { get; set; } = ModalSizes.Medium;

        [HtmlAttributeName("buttons")]
        public ModalFormButtons Buttons { get; set; } = ModalFormButtons.None;

        [HtmlAttributeName("callback-modal")]
        public bool Callback { get; set; } = false;

        [HtmlAttributeName("load-text")]
        public string LoadText { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // PREPROCESS

            string colorString;
            string sizeString;
            string leftBtnClass = "class=\"btn btn-outline pull-left\"";
            string rightBtnClass = "class=\"btn btn-outline\"";

            if (Url != null && !Url.EndsWith('/'))
            {
                Url = Url + '/';
            }

            switch (Color)
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

            switch (Sizes)
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
            if (Function.Substring(0, 1) == "#")
            {
                Function = Function.Substring(1, Function.Length - 1);
            }

            string modalId = Function + "Modal";


            // TAG
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            string classAttribute = $"modal modal-{colorString} fade";
            if (Callback)
            {
                classAttribute += " recursive-modal";
            }
            output.Attributes.Add("class", classAttribute);

            output.Attributes.Add("id", modalId);
            output.Attributes.Add("style", "display: none; padding-right: 17px;");
            output.Attributes.Add("data-loader-text", LoadText);



            // PRECONTENT

            var preContent = new StringBuilder();

            preContent.AppendFormat("<div class=\"modal-dialog {0}\">", sizeString);
            preContent.AppendFormat("<div class=\"modal-content\">");
            preContent.AppendFormat("<div class=\"modal-header\">");
            preContent.AppendFormat("<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">");
            preContent.AppendFormat("<span aria-hidden=\"true\">×</span>");
            preContent.AppendFormat("</button>");
            preContent.AppendFormat("<h4 class=\"modal-title\">{0}</h4>", Title);
            preContent.AppendFormat("</div>");
            if (Url != null)
            {
                //preContent.AppendFormat("<i class=\"fa fa-refresh fa-spin loader\" style=\"display: none\"></i>");
                preContent.Append($"<div class=\"holder\">");
                preContent.Append($"    <div class=\"preloader\">");
                preContent.Append($"        <div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div>");
                preContent.Append($"    </div>");
                preContent.Append($"<h4 class=\"preloader-text\"></h4>");
                preContent.Append($"</div>");
            }
            preContent.AppendFormat("<div class=\"modal-body {0}\" style=\"{1}\">", Class, Style);


            output.PreContent.SetHtmlContent(preContent.ToString());



            // POSTCONTENT

            var postContent = new StringBuilder();

            postContent.AppendFormat("</div>");

            switch (Buttons)
            {
                case ModalFormButtons.CerrarEnviar:
                    postContent.AppendFormat("<div class=\"modal-footer\">");
                    postContent.AppendFormat("<button type=\"button\" " + leftBtnClass + " data-dismiss=\"modal\">Cerrar</button>");
                    postContent.AppendFormat("<button type=\"button\" " + rightBtnClass + ">Enviar</button>");
                    postContent.AppendFormat("</div>");
                    break;
                case ModalFormButtons.Aceptar:
                    postContent.AppendFormat("<div class=\"modal-footer\">");
                    postContent.AppendFormat("<button type=\"button\" " + rightBtnClass + "  data-dismiss=\"modal\">Aceptar</button>");
                    postContent.AppendFormat("</div>");
                    break;
                case ModalFormButtons.CancelarAceptar:
                    break;
            }


            postContent.AppendFormat("</div>");
            postContent.AppendFormat("</div>");

            // SCRIPT

            postContent.AppendFormat("<script type=\"text/javascript\">");
            postContent.AppendFormat("function {0}(id){1}", Function, "{");
            //postContent.AppendFormat("var $modal = $(\"#{0}\");", modalId);
            //postContent.AppendFormat("var $loader = $modal.find('.loader')[0];");

            if (Url != null)
            {
                postContent.Append($"LoadModal('#{modalId}', '{Url}', id);");
                postContent.AppendFormat("{0};", "}");

                //postContent.AppendFormat("$loader.style.display = '';");
                //postContent.AppendFormat("$modal.find('.modal-body').empty();");
                //postContent.AppendFormat("$modal.modal('show');");
                //postContent.AppendFormat("$modal.find('.modal-body').load('{0}' + id, function() {1}", Url, "{");
                //postContent.AppendFormat("$loader.style.display = 'none';");
                //postContent.AppendFormat("{0});", "}");
                //postContent.AppendFormat("{0};", "}");
            }
            else
            {
                postContent.AppendFormat("var $modal = $(\"#{0}\");", modalId);
                postContent.AppendFormat("$modal.modal('show');");
                postContent.AppendFormat("{0};", "}");
            }

            postContent.AppendFormat("</script>");

            output.PostContent.SetHtmlContent(postContent.ToString());
        }
    }
}
