#pragma checksum "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "19641146d632ae47adb78b762b8189a9061fc966a61fabb5bab46388445c99d7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TestClasses_Delete), @"mvc.1.0.view", @"/Views/TestClasses/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/TestClasses/Delete.cshtml", typeof(AspNetCore.Views_TestClasses_Delete))]
namespace AspNetCore
{
    #line default
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\_ViewImports.cshtml"
using CommonsDev

    ;
#line 2 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\_ViewImports.cshtml"
using CommonsDev.Models

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"19641146d632ae47adb78b762b8189a9061fc966a61fabb5bab46388445c99d7", @"/Views/TestClasses/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"81e4e94993e228c8b64f050e4be0fcf6ec1aecaa54d0a255445e7f608c947b1e", @"/Views/_ViewImports.cshtml")]
    public class Views_TestClasses_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CommonsDev.Models.TestClass>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
  
    ViewData["Title"] = "Delete";

#line default
#line hidden

            BeginContext(80, 170, true);
            WriteLiteral("\r\n<h2>Delete</h2>\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n    <h4>TestClass</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(251, 40, false);
            Write(
#line 15 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayNameFor(model => model.Name)

#line default
#line hidden
            );
            EndContext();
            BeginContext(291, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(335, 36, false);
            Write(
#line 18 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayFor(model => model.Name)

#line default
#line hidden
            );
            EndContext();
            BeginContext(371, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(415, 48, false);
            Write(
#line 21 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayNameFor(model => model.CreationDate)

#line default
#line hidden
            );
            EndContext();
            BeginContext(463, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(507, 44, false);
            Write(
#line 24 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayFor(model => model.CreationDate)

#line default
#line hidden
            );
            EndContext();
            BeginContext(551, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(595, 47, false);
            Write(
#line 27 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayNameFor(model => model.DeletedDate)

#line default
#line hidden
            );
            EndContext();
            BeginContext(642, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(686, 43, false);
            Write(
#line 30 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayFor(model => model.DeletedDate)

#line default
#line hidden
            );
            EndContext();
            BeginContext(729, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(773, 47, false);
            Write(
#line 33 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayNameFor(model => model.DeletedById)

#line default
#line hidden
            );
            EndContext();
            BeginContext(820, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(864, 43, false);
            Write(
#line 36 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayFor(model => model.DeletedById)

#line default
#line hidden
            );
            EndContext();
            BeginContext(907, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(951, 48, false);
            Write(
#line 39 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayNameFor(model => model.LastEditTime)

#line default
#line hidden
            );
            EndContext();
            BeginContext(999, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1043, 44, false);
            Write(
#line 42 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayFor(model => model.LastEditTime)

#line default
#line hidden
            );
            EndContext();
            BeginContext(1087, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1131, 48, false);
            Write(
#line 45 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayNameFor(model => model.LastEditById)

#line default
#line hidden
            );
            EndContext();
            BeginContext(1179, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1223, 44, false);
            Write(
#line 48 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayFor(model => model.LastEditById)

#line default
#line hidden
            );
            EndContext();
            BeginContext(1267, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1311, 43, false);
            Write(
#line 51 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayNameFor(model => model.Display)

#line default
#line hidden
            );
            EndContext();
            BeginContext(1354, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1398, 39, false);
            Write(
#line 54 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayFor(model => model.Display)

#line default
#line hidden
            );
            EndContext();
            BeginContext(1437, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1481, 40, false);
            Write(
#line 57 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayNameFor(model => model.Show)

#line default
#line hidden
            );
            EndContext();
            BeginContext(1521, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1565, 36, false);
            Write(
#line 60 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
             Html.DisplayFor(model => model.Show)

#line default
#line hidden
            );
            EndContext();
            BeginContext(1601, 38, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n    \r\n    ");
            EndContext();
            BeginContext(1639, 207, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "19641146d632ae47adb78b762b8189a9061fc966a61fabb5bab46388445c99d712373", async() => {
                BeginContext(1665, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(1675, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "19641146d632ae47adb78b762b8189a9061fc966a61fabb5bab46388445c99d712790", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.
#line 65 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\TestClasses\Delete.cshtml"
                                      Id

#line default
#line hidden
                );
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1711, 84, true);
                WriteLiteral("\r\n        <input type=\"submit\" value=\"Delete\" class=\"btn btn-default\" /> |\r\n        ");
                EndContext();
                BeginContext(1795, 38, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "19641146d632ae47adb78b762b8189a9061fc966a61fabb5bab46388445c99d714808", async() => {
                    BeginContext(1817, 12, true);
                    WriteLiteral("Back to List");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1833, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1846, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CommonsDev.Models.TestClass> Html { get; private set; }
    }
}
#pragma warning restore 1591
