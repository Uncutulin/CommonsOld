using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Commons.TagHelpers.Boxes
{
    public enum BoxColor
    {
        Default,
        Success,
        Danger,
        Info,
        Warning,
        Primary,
        None
    }

    /// <summary>
    /// Helper for making outsider box.
    /// </summary>
    [HtmlTargetElement("box")]
    public class BoxTagHelper : TagHelper
    {
        [HtmlAttributeName("color")]
        public BoxColor Color { get; set; } = BoxColor.Default;

        [HtmlAttributeName("class")]
        public string Class { get; set; }

        [HtmlAttributeName("style")]
        public string Style { get; set; }

        [HtmlAttributeName("loader")]
        public bool Loader { get; set; } = true;

        [HtmlAttributeName("loader-id")]
        public string LoaderId { get; set; }

        [HtmlAttributeName("load-url")]
        public string Url { get; set; }

        [HtmlAttributeName("load-initial-height")]
        public int InitialHeight { get; set; } = 150;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Creo el código de color.
            string codigoColor = string.Empty;
            switch (Color)
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
                case BoxColor.None:
                    codigoColor = "box-solid";
                    break;

            }


            // TAG
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            string classAttribute = $"box {codigoColor}";
            if (Class != null) classAttribute += $" {Class}";
            output.Attributes.Add("class", classAttribute);
            if (Style != null) output.Attributes.Add("style", Style);

            
            // POSTCONTENT

            var postContent = new StringBuilder();

            if (Url != null)
            {
                postContent.AppendFormat("<div class=\"partialContents\" data-url=\"{0}\">", Url);
                postContent.AppendFormat("<div style=\"height: {0}px; width: 100%; clear: both; \"></div>", InitialHeight);
                postContent.AppendFormat("</div>");
            }

            if (Loader)
            {
                if (LoaderId != null)
                {
                    LoaderId = $"id=\"{LoaderId}\" ";
                }

                postContent.AppendFormat("<div {0}class=\"overlay box-loader\" style=\"display: none; \">", LoaderId);
                postContent.AppendFormat("<i class=\"fa fa-refresh fa-spin\"></i>");
                postContent.AppendFormat("</div>");
            }

            output.PostContent.SetHtmlContent(postContent.ToString());
        }

    }
}
