using System;
using System.Collections.Generic;
using System.Text;
using Commons.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Commons.TagHelpers.Boxes
{
    /// <summary>
    /// Helper for making the box body.
    /// </summary>
    [HtmlTargetElement("box-search")]
    public class BoxSearchTagHelper : TagHelper
    {
        [HtmlAttributeName("class")]
        public string Class { get; set; }

        [HtmlAttributeName("style")]
        public string Style { get; set; }

        [HtmlAttributeName("update-div")]
        public string PagesDiv { get; set; }

        [HtmlAttributeName("prevent-default")]
        public bool PreventDefault { get; set; } = false;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Page page = null;

            if (ViewContext.ViewData.Model is Page obj) page = obj;

            if (page == null || PagesDiv == null) return;


            // TAG
            output.TagName = "form";
            output.TagMode = TagMode.StartTagAndEndTag;

            string classAttribute = $"";
            if (Class != null) classAttribute += $" {Class}";
            output.Attributes.Add("class", classAttribute);
            output.Attributes.Add("action", page.Url);
            output.Attributes.Add("data-ajax", "true");
            output.Attributes.Add("data-ajax-mode", "replace");
            output.Attributes.Add("data-ajax-update", $"#{PagesDiv}");
            output.Attributes.Add("data-ajax-begin", $"Show{PagesDiv}Loader()");
            output.Attributes.Add("data-ajax-complete", $"Hide{PagesDiv}Loader()");

            if (Style != null) output.Attributes.Add("style", Style);
            
            // PRECONTENT

            var preContent = new StringBuilder();

            //preContent.AppendFormat("<form action=\"{0}\" data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"#{1}\" data-ajax-begin=\"Show{1}Loader()\" data-ajax-complete=\"Hide{1}Loader()\">", page.Url, PagesDiv);
            preContent.AppendFormat("<div class=\"btn-toolbar\" style=\"margin-right: 5px\">");

            if (!PreventDefault)
            {
                preContent.AppendFormat("<div class=\"input-group input-group-sm\"  style=\"display: flex;\">");
                preContent.AppendFormat("<input id=\"Number\" name=\"Number\" type=\"hidden\" value=\"1\">");
                preContent.AppendFormat("<input id=\"SearchText\" name=\"SearchText\"  style=\"max-width: max-content;\" class=\"form-control pull-right\" placeholder=\"Buscar...\" value=\"{0}\">", page.SearchText);
                preContent.AppendFormat("<div class=\"input-group-btn\">");
                preContent.AppendFormat("<button type=\"submit\" class=\"btn btn-default\"><i class=\"fa fa-search\"></i></button>");
                preContent.AppendFormat("</div>");
                preContent.AppendFormat("</div>");
            }

            output.PreContent.SetHtmlContent(preContent.ToString());

            // POSTCONTENT

            var postContent = new StringBuilder();



            postContent.AppendFormat("</div>");
            //postContent.AppendFormat("</form>");

            output.PostContent.SetHtmlContent(postContent.ToString());
        }
    }
}
