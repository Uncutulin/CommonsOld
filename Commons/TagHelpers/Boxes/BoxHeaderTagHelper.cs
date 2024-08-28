using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Commons.TagHelpers.Boxes
{
    /// <summary>
    /// Helper for making the box header.
    /// </summary>
    [HtmlTargetElement("box-header")]
    public class BoxHeaderTagHelper : TagHelper
    {
        [HtmlAttributeName("class")] public string Class { get; set; }

        [HtmlAttributeName("style")] public string Style { get; set; }

        [HtmlAttributeName("title")] public string Title { get; set; }

        [HtmlAttributeName("icon")] public string Icon { get; set; }

        [HtmlAttributeName("minimize")] public bool Minimize { get; set; } = false;

        [HtmlAttributeName("close")] public bool Close { get; set; } = false;

        [HtmlAttributeName("border")] public bool Border { get; set; } = true;


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Icon != null) Icon = "<i class=\"" + Icon + "\"></i>";
            
            // TAG
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            string classAttribute = $"box-header";
            if (Border) classAttribute += " with-border";
            if (Class != null) classAttribute += $" {Class}";
            output.Attributes.Add("class", classAttribute);
            if (Style != null) output.Attributes.Add("style", Style);


            // PRECONTENT

            var preContent = new StringBuilder();
            
            preContent.AppendFormat("{0}<h3 class=\"box-title\">{1}</h3>", Icon, Title ?? "");
            preContent.AppendFormat("<div class=\"box-tools pull-right\">");
            preContent.AppendFormat("<div style=\"display: flex\">");

            output.PreContent.SetHtmlContent(preContent.ToString());

            // POSTCONTENT

            var postContent = new StringBuilder();

            if (Minimize)
            {
                postContent.AppendFormat(
                    "<button type=\"button\" class=\"btn btn-box-tool\" data-widget=\"collapse\" data-toggle=\"tooltip\" title=\"Minimizar\">");
                postContent.AppendFormat("<i class=\"fa fa-minus\"></i>");
                postContent.AppendFormat("</button>");
            }

            if (Close)
            {
                postContent.AppendFormat(
                    "<button type=\"button\" class=\"btn btn-box-tool\" data-widget=\"remove\" data-toggle=\"tooltip\" title=\"Ocultar\">");
                postContent.AppendFormat("<i class=\"fa fa-times\"></i>");
                postContent.AppendFormat("</button>");
            }
            
            postContent.AppendFormat("</div>");
            postContent.AppendFormat("</div>");

            output.PostContent.SetHtmlContent(postContent.ToString());
        }
    }
}
