using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Commons.TagHelpers.Boxes;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Commons.TagHelpers
{
    [HtmlTargetElement("select")]
    public class SelectTagHelper : TagHelper
    {
        [HtmlAttributeName("searchable")]
        public bool Searchable { get; set; } = true;

        [HtmlAttributeName("width")]
        public string Width { get; set; } = "100%";

        [HtmlAttributeName("use-default")]
        public bool UseDefault { get; set; } = false;

        [HtmlAttributeName("menu-size")]
        public int? OptionsToShow { get; set; }

        [HtmlAttributeName("menu-header")]
        public string Header { get; set; }

        [HtmlAttributeName("class-btn")]
        public string BtnClasses { get; set; } = "";

        [HtmlAttributeName("color")]
        public BoxColor Color { get; set; } = BoxColor.Default;
        
        [HtmlAttributeName("multiple-max-options")]
        public int? MultipleMaxOptions { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            
            if (!UseDefault)
            {
                output.AddClass("selectpicker", HtmlEncoder.Default);

                if (Searchable) output.Attributes.Add("data-live-search", "true");
                if (OptionsToShow != null) output.Attributes.Add("data-size", OptionsToShow);
                if (Header != null) output.Attributes.Add("data-header", Header);
                if (MultipleMaxOptions != null)
                {
                    output.Attributes.Add("multiple", "multiple");
                    output.Attributes.Add("data-max-options", MultipleMaxOptions);
                }
                output.Attributes.Add("data-width", Width);

                switch (Color)
                {
                    case BoxColor.Default:
                        break;
                    case BoxColor.Danger:
                        BtnClasses = $"{BtnClasses} btn-danger";
                        break;
                    case BoxColor.Success:
                        BtnClasses = $"{BtnClasses} btn-success";
                        break;
                    case BoxColor.Info:
                        BtnClasses = $"{BtnClasses} btn-info";
                        break;
                    case BoxColor.Warning:
                        BtnClasses = $"{BtnClasses} btn-warning";
                        break;
                    case BoxColor.Primary:
                        BtnClasses = $"{BtnClasses} btn-primary";
                        break;
                }
                if (BtnClasses != null) output.Attributes.Add("data-style", BtnClasses);

                // POSTCONTENT

                var postContent = new StringBuilder();

                postContent.AppendFormat("<script> $('select').selectpicker(); </script>");

                output.PostElement.SetHtmlContent(postContent.ToString());

            }
        }
    }
}