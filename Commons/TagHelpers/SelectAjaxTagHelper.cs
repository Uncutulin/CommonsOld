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
    /// <summary>
    /// Recieves a List<SelectpickerItem>
    /// </summary>
    [HtmlTargetElement("select-ajax")]
    public class SelectAjaxTagHelper : SelectTagHelper
    {
        [HtmlAttributeName("url")]
        public string Url { get; set; } 

        [HtmlAttributeName("text-searching")]
        public string TextSearching { get; set; } = "Buscando coincidencias...";

        [HtmlAttributeName("text-no-results")]
        public string TextNoResults { get; set; } = "No se encontraron coincidencias.";

        [HtmlAttributeName("text-initialized")]
        public string TextInitialized { get; set; } = "Escriba un texto para buscar...";

        [HtmlAttributeName("text-placeholder")]
        public string TextPlaceholder { get; set; } = "";

        [HtmlAttributeName("text-selected")]
        public string TextSelected { get; set; } = "Seleccionado";

        [HtmlAttributeName("id")]
        public string Id { get; set; }

        [HtmlAttributeName("route-name")]
        public string RouteName { get; set; } = "q";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // TAG
            output.TagName = "select";
            output.TagMode = TagMode.StartTagAndEndTag;

            base.Process(context, output);

            if (Id != null) output.Attributes.Add("id", Id);
            
            if (!UseDefault && Url != null)
            {
                if (Id == null)
                {
                    Id = RandomString(7);
                    output.Attributes.Add("id", Id);
                }
                output.AddClass($"with-ajax", HtmlEncoder.Default);

                // POSTCONTENT

                var postContent = new StringBuilder();

                var title = output.Attributes.FirstOrDefault(x => x.Name == "title")?.Value?.ToString() ?? "Buscar...";

                string script = $"<script>" +
                             $"var options = {'{'}" +
                             $"    values: \"a, b, c\"," +
                             $"    ajax: {'{'}" +
                             $"        url: \"{Url}\"," +
                             $"        type: \"POST\"," +
                             $"        dataType: \"json\"," +
                             $"        data: {'{'}" +
                             $"            {RouteName}:" + " \"{{{q}}}\"" +
                             $"        {'}'}" +
                             $"    {'}'}," +
                             $"    locale: {'{'}" +
                             $"        emptyTitle: \"{title}\"," +
                             $"        statusSearching: \"{TextSearching}\"," +
                             $"        statusNoResults: \"{TextNoResults}\"," +
                             $"        statusInitialized: \"{TextInitialized}\"," +
                             $"        searchPlaceholder: \"{TextPlaceholder}\"," +
                             $"        currentlySelected: \"{TextSelected}\"" +
                             $"    {'}'}," +
                             $"    preprocessData: function(data) {'{'}" +
                             $"        var i," +
                             $"            l = data.length," +
                             $"            array = [];" +
                             $"        if (l) {'{'}" +
                             $"            for (i = 0; i < l; i++) {'{'}" +
                             $"                array.push(" +
                             $"                    $.extend(true, data[i], {'{'}" +
                             $"                        text: data[i].Text," +
                             $"                        value: data[i].Value," +
                             $"                        disabled: data[i].Disabled," +
                             $"                        divider: data[i].Divider," +
                             $"                        class: data[i].Class," +
                             $"                        data: {'{'}" +
                             $"                            subtext: data[i].Subtext," +
                             $"                            icon: data[i].Icon" +
                             $"                        {'}'}" +
                             $"                    {'}'})" +
                             $"                );" +
                             $"            {'}'}" +
                             $"        {'}'}" +
                             $"        return array;" +
                             $"    {'}'}" +
                             $"{'}'};" +
                             $"$(\"#{Id}\")" +
                             $"    .selectpicker()" +
                             $"    .filter(\".with-ajax\")" +
                             $"    .ajaxSelectPicker(options);" +
                             $"$(\"select\").trigger(\"change\");" +
                             $"function chooseSelectpicker(index, selectpicker) {'{'}" +
                             $"    $(selectpicker).val(index);" +
                             $"    $(selectpicker).selectpicker('refresh');" +
                             $"{'}'}" +
                             $"</script>";


                postContent.Append(script);

                output.PostElement.SetHtmlContent(postContent.ToString());

            }

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}