using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using Commons.HtmlHelpers;
using Commons.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;

namespace Commons.TagHelpers
{
    [HtmlTargetElement("input", Attributes = "[type='checkbox']")]
    public class AspCheckboxTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression Aspfor { get; set; }

        [HtmlAttributeName("id")]
        public string Id { get; set; }

        [HtmlAttributeName("color")]
        public UiColor Color { get; set; } = UiColor.Default;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("id", Id);
            output.Attributes.Add("asp-for", Aspfor);

            base.Process(context, output);

            if (Aspfor != null && Aspfor.Metadata.ModelType != typeof(bool)) return;
            if (Color == UiColor.None) return;

            if (Id == null)
            {
                Id = Aspfor != null ? Aspfor.Name : RandomString(5);
            }


            switch (Color)
            {
                case UiColor.Info:
                    output.PreElement.AppendHtml(@"<div class='icheck-material-lightblue'>");
                    break;
                case UiColor.Default:
                    output.PreElement.AppendHtml(@"<div class='icheck-material-grey'>");
                    break;
                case UiColor.Danger:
                    output.PreElement.AppendHtml(@"<div class='icheck-material-red'>");
                    break;
                case UiColor.Primary:
                    output.PreElement.AppendHtml(@"<div class='icheck-material-indigo'>");
                    break;
                case UiColor.Warning:
                    output.PreElement.AppendHtml(@"<div class='icheck-material-orange'>");
                    break;
                case UiColor.Success:
                    output.PreElement.AppendHtml(@"<div class='icheck-material-green'>");
                    break;

            }


            output.PostElement.AppendHtml($"<label for='{Id}'></label>");
            output.PostElement.AppendHtml("</div>");


        }

        private static readonly Random Random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }

}
