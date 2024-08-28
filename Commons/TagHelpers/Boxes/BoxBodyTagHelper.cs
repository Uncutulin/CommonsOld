using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Commons.TagHelpers.Boxes
{
    /// <summary>
    /// Helper for making the box body.
    /// </summary>
    [HtmlTargetElement("box-body")]
    public class BoxBodyTagHelper : TagHelper
    {
        [HtmlAttributeName("class")]
        public string Class { get; set; }

        [HtmlAttributeName("style")]
        public string Style { get; set; }

        [HtmlAttributeName("load-url")]
        public string Url { get; set; }

        [HtmlAttributeName("load-initial-height")]
        public int InitialHeight { get; set; } = 150;

        [HtmlAttributeName("table")]
        public bool Table { get; set; } = false;


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // TAG
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            string classAttribute = $"box-body";
            if (Url != null) classAttribute += " partialContents";
            if (Table) classAttribute += " table-responsive no-padding";
            if (Class != null) classAttribute += $" {Class}";
            output.Attributes.Add("class", classAttribute);
            if (Style != null) output.Attributes.Add("style", Style);
            if (Url != null) output.Attributes.Add("data-url", Url);

            // PRECONTENT

            var preContent = new StringBuilder();
            
            if (Url != null)
            {
                preContent.AppendFormat("<div style=\"height: {0}px; width: 100%; clear: both; \"></div>", InitialHeight);
            }

            output.PreContent.SetHtmlContent(preContent.ToString());


            // POSTCONTENT

            var postContent = new StringBuilder();
            

            output.PostContent.SetHtmlContent(postContent.ToString());
        }

    }
}
