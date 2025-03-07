﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons.TagHelpers.DataTables;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Commons.TagHelpers.DataTables
{
    [HtmlTargetElement("data-table")]
    [RestrictChildren("data-table-columns")]
    public class DataTableTagHelper : TagHelper
    {
        private const string DataUrlName = "url-data";
        private const string ProcessingName = "processing";
        private const string ServerSideName = "server-side";
        private const string SearchDelayName = "search-delay";
        private const string QueryIdsName = "query-ids";
        private const string ModelTypeName = "model-type";
        private const string TableIdName = "table-id";
        private const string JsonDataName = "json-data";

        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly IStringLocalizer _localizer;

        [HtmlAttributeName(DataUrlName)]
        public string DataUrl { get; set; }

        [HtmlAttributeName(ProcessingName)]
        public bool Processing { get; set; } = true;

        [HtmlAttributeName(ServerSideName)]
        public bool ServerSide { get; set; } = true;

        [HtmlAttributeName(SearchDelayName)]
        public int SearchDelay { get; set; } = 1000;

        [HtmlAttributeName(QueryIdsName)]
        public string QueryIds { get; set; }

        [HtmlAttributeName(ModelTypeName)]
        public Type ModelType { get; set; }

        [HtmlAttributeName(TableIdName)]
        public string TableId { get; set; }

        [HtmlAttributeName(JsonDataName)]
        public string JsonData { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null || output == null) throw new ArgumentNullException();
            Contract.EndContractBlock();

            if (TableId == null) TableId = $"Table_{RandomString(5)}";

            if (ModelType == null) ModelType = ViewContext.ViewData.ModelMetadata.ModelType;

            output.TagName = "";
            var tableContext = await GetTableContextAsync(context, output);

            var sb = new StringBuilder();
            AppendTableHtml(sb, tableContext, context);

            sb.AppendLine("<script type=\"text/javascript\">");

            var initializeFunctionName = $"initialize_{TableId}";
            AppendInitializeFunction(sb, tableContext, initializeFunctionName);

            sb.AppendLine($"{initializeFunctionName}();");
            sb.AppendLine("</script>");

            output.Content.AppendHtml(sb.ToString());
        }

        private void AppendInitializeFunction(StringBuilder sb, DataTableContext tableContext,
            string initializeFunctionName)
        {
            sb.AppendLine("function " + initializeFunctionName + "(){");
            sb.AppendLine($"$('#{TableId}').DataTable({{");

            var localizationUrl = _localizer?["LocalizationUrl"];
            if (!string.IsNullOrEmpty(localizationUrl))
            {
                sb.AppendLine($"language: {{url: \"{localizationUrl}\"}},");
            }

            if (ServerSide)
            {
                AppendServerSideProcessingData(sb);

            }
            else if (!string.IsNullOrEmpty(JsonData))
            {
                AppendData(sb);
            }
            else
            {
                throw new NotImplementedException();
            }

            AppendColumns(sb, tableContext);

            sb.AppendLine("});");
            sb.AppendLine("}");
        }


        public DataTableTagHelper(IModelMetadataProvider modelMetadataProvider)
        {
            Contract.EndContractBlock();

            _modelMetadataProvider = modelMetadataProvider ?? throw new ArgumentNullException();

            //_localizer = localizer; -- , (IStringLocalizer<DataTableTagHelper> localizer)

        }


        private async Task<DataTableContext> GetTableContextAsync(TagHelperContext context, TagHelperOutput output)
        {
            Contract.Requires(context != null && output != null);

            var tableContext = new DataTableContext { ModelExplorer = GetModelExplorer(ModelType) };
            context.Items.Add(typeof(DataTableTagHelper), tableContext);

            await output.GetChildContentAsync();

            return tableContext;
        }

        private void AppendTableHtml(StringBuilder sb, DataTableContext tableContext, TagHelperContext context)
        {
            Contract.Requires(sb != null && tableContext != null && context != null);

            var classes = "";
            if (context.AllAttributes.TryGetAttribute("class", out TagHelperAttribute classTag))
            {
                classes = classTag.Value.ToString();
            }

            sb.AppendLine($"<table id=\"{TableId}\" class=\"{classes}\" cellspacing=\"0\" width=\"100%\">");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            if (tableContext.ColumnsProperties != null)
            {
                foreach (var columnProperty in tableContext.ColumnsProperties)
                {
                    sb.AppendLine($"<th>{columnProperty.Metadata?.DisplayName ?? columnProperty.Name}</th>");
                }
            }
            if (tableContext.ActionDataSet != null && tableContext.ActionDataSet.Any())
            {
                sb.AppendLine("<th></th>");
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("</table>");
        }

        private void AppendServerSideProcessingData(StringBuilder sb)
        {
            Contract.Requires(sb != null);

            sb.AppendLine("serverSide: true,");
            sb.AppendLine($"processing: {Processing.ToString().ToLower()},");
            sb.AppendLine($"searchDelay: {SearchDelay},");
            sb.AppendLine("ajax: {");
            //sb.AppendLine("contentType: \"json\",");
            sb.AppendLine("type: \"POST\",");
            sb.AppendLine($"url:\"{DataUrl}\",");

            //sb.AppendLine("data: function (params) {");
            //sb.AppendLine("return {");
            //sb.AppendLine("draw: params.draw,");
            //sb.AppendLine("start: params.start,");
            //sb.AppendLine("pageSize: params.length,");
            //sb.AppendLine("term: params.search.value,");
            //sb.AppendLine("orderField: params.columns[params.order[0].column].data,");
            //sb.AppendLine("orderDirection: params.order[0].dir,");
            //if (QueryIds != null)
            //{
            //    sb.AppendLine($"queryIds: \"{QueryIds}\",");
            //}
            //sb.AppendLine("};},");

            sb.AppendLine("},");
        }

        private void AppendData(StringBuilder sb)
        {
            Contract.Requires(sb != null);

            sb.AppendLine("searching: false,");
            sb.AppendLine("paging: false,");
            sb.AppendLine("ordering: false,");
            sb.AppendLine("info: false,");
            sb.AppendLine($"aaData: {JsonData},");
        }

        private void AppendColumns(StringBuilder sb, DataTableContext tableContext)
        {
            Contract.Requires(sb != null && tableContext != null);

            sb.AppendLine("columns: [");
            if (tableContext.ColumnsProperties != null)
            {
                foreach (var columnProperty in tableContext.ColumnsProperties)
                {
                    //var columnName = Char.ToLowerInvariant(columnProperty.Name[0]) + columnProperty.Name.Substring(1);
                    var columnName = columnProperty.Name;
                    if (columnProperty.Metadata.ModelType == typeof(DateTime) || columnProperty.Metadata.ModelType == typeof(DateTime?))
                    {
                        sb.AppendLine($"{{ \"data\": \"{columnName}\",");
                        sb.AppendLine("\"render\": function(data){");
                        sb.AppendLine("if (data == null) return data;");
                        sb.AppendLine("var d = new Date(data);");
                        sb.AppendLine("return d.toLocaleString();");
                        sb.AppendLine("}},");
                    }
                    else if (columnProperty.Metadata.ModelType == typeof(decimal) || columnProperty.Metadata.ModelType == typeof(decimal?))
                    {
                        sb.AppendLine($"{{ \"data\": \"{columnName}\",");
                        sb.AppendLine("\"render\": function(data){");
                        sb.AppendLine("return data.toLocaleString();");
                        sb.AppendLine("}},");
                    }
                    else
                    {
                        sb.AppendLine($"{{ \"data\": \"{columnName}\",");
                        sb.AppendLine("render: $.fn.dataTable.render.text()},");
                    }
                }
            }
            if (tableContext.ActionDataSet != null && tableContext.ActionDataSet.Any())
            {
                sb.AppendLine(GetColumnActionContent(tableContext.ActionDataSet));
            }
            sb.AppendLine("],");
        }
        
        private string GetColumnActionContent(List<ActionData> actionDataSet)
        {
            Contract.Requires(actionDataSet != null);

            var sb = new StringBuilder();
            sb.AppendLine("{ \"sortable\" : false,");
            sb.AppendLine("\"render\" : function(data, type, row){");
            sb.AppendLine("var action = ''");

            foreach (var actionData in actionDataSet)
            {
                if (actionData != null)
                {
                    if (!string.IsNullOrEmpty(actionData.CanExecuteProperty))
                    {
                        sb.Append($"if(row['{actionData.CanExecuteProperty}'] == true){{ action = action + `");
                    }
                    else
                    {
                        sb.Append("{ action = action + `");
                    }

                    string tooltip = $" data-toggle='tooltip' data-placement='top' title='{actionData.ActionTitle}' ";
                    string classes = (actionData.Classes == null) ? "" : $" class='{actionData.Classes}' ";

                    if (actionData.ActionUrl.Contains("id"))
                    {
                        var url = actionData.ActionUrl.Replace("id", "` + row['Id'] + `");
                        sb.Append($"<a {tooltip} {classes} href={url}>{actionData.Content.ToString()}</a>");
                    }
                    else
                    {
                        sb.Append($"<a {tooltip} {classes} href={actionData.ActionUrl}/` + row['Id'] + `>{actionData.Content.ToString()}</a>");
                    }

                    //sb.Append("<script> $('[data-toggle=\"tooltip\"]').tooltip() </script>");

                    if (actionDataSet.IndexOf(actionData) < actionDataSet.Count - 1 &&
                        string.IsNullOrEmpty(actionData.Classes))
                    {
                        sb.Append(" | ");
                    }

                    sb.Append("`;}");
                }
            }
            sb.Append("return action;");
            sb.Append("}}");
            return sb.ToString();
        }

        private ModelExplorer GetModelExplorer(Type modelType)
        {
            if (modelType != null)
            {
                return _modelMetadataProvider.GetModelExplorerForType(ModelType, null);
            }
            return null;
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


