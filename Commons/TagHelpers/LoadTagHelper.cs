using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Commons.TagHelpers
{
    [HtmlTargetElement("load")]
    public class LoadTagHelper : TagHelper
    {
        [HtmlAttributeName("load-url")]
        public string Url { get; set; }

        [HtmlAttributeName("load-initial-height")]
        public int InitialHeight { get; set; } = 150;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // TAG
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("class", "partialContents");
            output.Attributes.Add("data-url", Url);

            
            // PRECONTENT

            var preContent = new StringBuilder();
            
            output.PreContent.SetHtmlContent(preContent.ToString());


            // POSTCONTENT

            var postContent = new StringBuilder();

            postContent.AppendFormat("<div style=\"height: {0}px; width: 100%; clear: both; \"></div>", InitialHeight);

            output.PostContent.SetHtmlContent(postContent.ToString());
        }
    }
}
