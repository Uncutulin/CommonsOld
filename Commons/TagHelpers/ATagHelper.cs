using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Identity;
using Microsoft.AspNetCore.Http;

namespace Commons.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "title")]
    public class AToolipTagHelper : TagHelper
    {
        [HtmlAttributeName("title")]
        public string Title { get; set; }

        [HtmlAttributeName("title-position")]
        public TooltipPosition TooltipPosition { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (Title != null)
            {
                output.Attributes.Add("data-toggle", "tooltip");
                output.Attributes.Add("title", Title);

                switch (TooltipPosition)
                {
                    case TooltipPosition.Top:
                        output.Attributes.Add("data-placement", "top");
                        break;
                    case TooltipPosition.Bottom:
                        output.Attributes.Add("data-placement", "bottom");
                        break;
                    case TooltipPosition.Left:
                        output.Attributes.Add("data-placement", "left");
                        break;
                    case TooltipPosition.Right:
                        output.Attributes.Add("data-placement", "right");
                        break;
                }

                // POSTCONTENT

                var postContent = new StringBuilder();

                output.PostContent.SetHtmlContent(postContent.ToString());
            }

        }
    }
    public enum TooltipPosition
    {
        Top,
        Bottom,
        Right,
        Left
    }

    [HtmlTargetElement("a", Attributes = "confirm")]
    public class AConfirmTagHelper : TagHelper
    {
        [HtmlAttributeName("confirm")]
        public string ConfirmText { get; set; }
        
        [HtmlAttributeName("confirm-title")]
        public string ConfirmTitle { get; set; }
        
        [HtmlAttributeName("confirm-yes")]
        public string ConfirmYes { get; set; }
        
        [HtmlAttributeName("confirm-no")]
        public string ConfirmNo { get; set; }
        
        [HtmlAttributeName("confirm-icon")]
        public UiColor Color { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (ConfirmText != null)
            {
                output.Attributes.Add("data-confirmbodytext", ConfirmText);

                if (ConfirmYes != null)
                {
                    output.Attributes.Add("data-confirmyestext", ConfirmYes);
                }
                if (ConfirmNo != null)
                {
                    output.Attributes.Add("data-confirmnotext", ConfirmNo);
                }
                if (ConfirmTitle != null)
                {
                    output.Attributes.Add("data-confirmtitletext", ConfirmTitle);
                }
                output.Attributes.Add("data-confirmicon", Color);
            }
        }
    }

    public enum AjaxMode
    {
        Replace,
        Before,
        After
    }

    public enum RequestMethod
    {
        Get,
        Post
    }

    [HtmlTargetElement("a", Attributes = "ajax-update-div")]
    public class AAjaxTagHelper : TagHelper
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
