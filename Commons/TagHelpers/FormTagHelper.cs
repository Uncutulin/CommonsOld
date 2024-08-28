using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Commons.TagHelpers
{
    [HtmlTargetElement("form", Attributes = "ajax-update-div")]
    public class FormTagHelper : TagHelper
    {
        [HtmlAttributeName("ajax-update-div")]
        public string UpdateDiv { get; set; }

        [HtmlAttributeName("ajax-method")]
        public RequestMethod Method { get; set; } = RequestMethod.Post;

        [HtmlAttributeName("ajax-mode")]
        public AjaxMode Mode { get; set; } = AjaxMode.Replace;

        [HtmlAttributeName("ajax-loading")]
        public string LoadingDiv { get; set; }

        [HtmlAttributeName("ajax-begin")]
        public string AtBegin { get; set; }

        [HtmlAttributeName("ajax-complete")]
        public string AtComplete { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (UpdateDiv != null)
            {
                output.Attributes.Add("data-ajax", "true");

                output.Attributes.Add("data-ajax-update", UpdateDiv);

                switch (Method)
                {
                    case RequestMethod.Post:
                        output.Attributes.Add("data-ajax-method", "Post");
                        break;
                    case RequestMethod.Get:
                        output.Attributes.Add("data-ajax-method", "Get");
                        break;
                }

                switch (Mode)
                {
                    case AjaxMode.Replace:
                        output.Attributes.Add("data-ajax-mode", "replace");
                        break;
                    case AjaxMode.After:
                        output.Attributes.Add("data-ajax-mode", "after");
                        break;
                    case AjaxMode.Before:
                        output.Attributes.Add("data-ajax-mode", "before");
                        break;
                }

                if (LoadingDiv != null)
                {
                    output.Attributes.Add("data-ajax-loading", LoadingDiv);
                }

                if (AtBegin != null)
                {
                    output.Attributes.Add("data-ajax-begin", AtBegin);
                }

                if (AtComplete != null)
                {
                    output.Attributes.Add("data-ajax-complete", AtComplete);
                }

            }
        }
    }
}
