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
    [HtmlTargetElement("box-footer")]
    public class BoxFooterTagHelper : TagHelper
    {
        [HtmlAttributeName("class")]
        public string Class { get; set; }

        [HtmlAttributeName("style")]
        public string Style { get; set; }

        [HtmlAttributeName("page-boxes")]
        public int PagesBoxes { get; set; } = 9;

        [HtmlAttributeName("page-update-div")]
        public string PagesDiv { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Page page = null;

            if (ViewContext.ViewData.Model is Page obj) page = obj;


            // TAG
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            string classAttribute = $"box-footer";
            if (Class != null) classAttribute += $" {Class}";
            output.Attributes.Add("class", classAttribute);
            if (Style != null) output.Attributes.Add("style", Style);


            // PRECONTENT

            var preContent = new StringBuilder();

            if (PagesDiv != null)
            {
                preContent.AppendFormat("<div class=\"row\">");
                preContent.AppendFormat("<div class=\"col-md-6\">");
            }

            output.PreContent.SetHtmlContent(preContent.ToString());


            // POSTCONTENT

            var postContent = new StringBuilder();

            if (PagesDiv != null)
            {
                postContent.AppendFormat("</div>");



                postContent.AppendFormat("<div class=\"col-md-6\">");

                postContent.AppendFormat("<form action=\"{0}\" data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"#{1}\" data-ajax-begin=\"Show{1}Loader()\" data-ajax-complete=\"Hide{1}Loader()\">", page.Url, PagesDiv);
                postContent.AppendFormat("<input type=\"hidden\" id=\"SearchText\" name=\"SearchText\" value=\"{0}\">", page.SearchText);
                postContent.AppendFormat("<input type=\"hidden\" asp-for=\"SearchFields\">");
                postContent.AppendFormat("<input type=\"hidden\" id=\"Number\" name=\"Number\" value=\"{0}\">", page.Number);

                foreach ((string, string) argument in page.Arguments)
                {
                    if (argument.Item2 == null) continue;
                    postContent.AppendFormat("<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\">", argument.Item1, argument.Item2);
                }

                postContent.AppendFormat("<ul class=\"pagination pagination-sm no-margin pull-right\">");

                // Number Boxes.
                int min = 1, max = page.PagesCount();
                if (page.PagesCount() > PagesBoxes)
                {
                    int leftPages = PagesBoxes / 2;
                    int rightPages = PagesBoxes - leftPages - 1;

                    int mid = page.Number;

                    min = mid - leftPages;
                    max = mid + rightPages;

                    if (max > page.PagesCount())
                    {
                        max = page.PagesCount();
                        min = max - PagesBoxes;
                    }

                    if (min < 1)
                    {
                        min = 1;
                        max = min + PagesBoxes;
                    }

                }

                // Previous page button.
                if (page.Number != 1)
                {
                    postContent.AppendFormat("<li>");
                    postContent.AppendFormat("<a onclick=\"SelectPage(this, {0})\" href=\"javascript: void(0);\"><i class=\"fa fa-angle-left\"></i></a>", page.Number - 1);
                    postContent.AppendFormat("</li>");
                }

                // Pages.
                for (int i = min; i < max + 1; i++)
                {
                    string classString = page.Number == i ? " class=active" : " class=hidden-xs";

                    postContent.AppendFormat("<li{0}>", classString);
                    postContent.AppendFormat("<a onclick=\"SelectPage(this, {0})\" href=\"javascript: void(0);\">{0}</a>", i);
                    postContent.AppendFormat("</li>");
                }

                // Next page button.
                if (page.Number != max)
                {
                    postContent.AppendFormat("<li>");
                    postContent.AppendFormat("<a onclick=\"SelectPage(this, {0})\" href=\"javascript: void(0);\"><i class=\"fa fa-angle-right\"></i></a>", page.Number + 1);
                    postContent.AppendFormat("</li>");
                }

                postContent.AppendFormat("</ul>");
                postContent.AppendFormat("<button style=\"display: none\" type=\"submit\"></button>");
                postContent.AppendFormat("</form>");
                postContent.AppendFormat("</div>");
                postContent.AppendFormat("</div>");

                // Script.
                postContent.AppendFormat("<script>");
                postContent.AppendFormat("function Show{0}Loader() {1}", PagesDiv, "{");
                postContent.AppendFormat("var box = $('#{0}').closest('.box')[0];", PagesDiv);
                postContent.AppendFormat("var loader = box.querySelector('.overlay.box-loader');");
                postContent.AppendFormat("if (loader) {0}", "{");
                postContent.AppendFormat("loader.style.display = '';");
                postContent.AppendFormat("{0}{0}", "}");

                postContent.AppendFormat("function Hide{0}Loader() {1}", PagesDiv, "{");
                postContent.AppendFormat("var box = $('#{0}').closest('.box')[0];", PagesDiv);
                postContent.AppendFormat("var loader = box.querySelector('.overlay.box-loader');");
                postContent.AppendFormat("if (loader) {0}", "{");
                postContent.AppendFormat("loader.style.display = 'none';");
                postContent.AppendFormat("{0}{0}", "}");

                postContent.AppendFormat("function SelectPage(event, page) {0}", "{");
                postContent.AppendFormat("var form = event.closest('form');");
                postContent.AppendFormat("var input = form.querySelector('#Number');");
                postContent.AppendFormat("var submit = form.querySelector('button');");
                postContent.AppendFormat("input.value = page;");
                postContent.AppendFormat("submit.click();");
                postContent.AppendFormat("{0}", "}");
                postContent.AppendFormat("");
                postContent.AppendFormat("</script>");

            }

            output.PostContent.SetHtmlContent(postContent.ToString());
        }

    }
}
