#pragma checksum "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\Shared\Components\MenuMessage\LayoutMenuMessage.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "83d9a112f6c309ebf44f37e19c8a27bcf21e5f588bcd5bbc7db8eb19ef0894e5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_MenuMessage_LayoutMenuMessage), @"mvc.1.0.view", @"/Views/Shared/Components/MenuMessage/LayoutMenuMessage.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Components/MenuMessage/LayoutMenuMessage.cshtml", typeof(AspNetCore.Views_Shared_Components_MenuMessage_LayoutMenuMessage))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"83d9a112f6c309ebf44f37e19c8a27bcf21e5f588bcd5bbc7db8eb19ef0894e5", @"/Views/Shared/Components/MenuMessage/LayoutMenuMessage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"81e4e94993e228c8b64f050e4be0fcf6ec1aecaa54d0a255445e7f608c947b1e", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_MenuMessage_LayoutMenuMessage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Commons.Models.Message>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(43, 215, true);
            WriteLiteral("<li class=\"dropdown messages-menu\">\n    <!-- Menu toggle button -->\n    <a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">\n        <i class=\"fa fa-envelope-o\"></i>\n        <span class=\"label label-success\">");
            EndContext();
            BeginContext(259, 13, false);
            Write(
#line 6 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\Shared\Components\MenuMessage\LayoutMenuMessage.cshtml"
                                           Model.Count()

#line default
#line hidden
            );
            EndContext();
            BeginContext(272, 84, true);
            WriteLiteral("</span>\n    </a>\n    <ul class=\"dropdown-menu\">\n        <li class=\"header\">You have ");
            EndContext();
            BeginContext(357, 13, false);
            Write(
#line 9 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\Shared\Components\MenuMessage\LayoutMenuMessage.cshtml"
                                     Model.Count()

#line default
#line hidden
            );
            EndContext();
            BeginContext(370, 113, true);
            WriteLiteral(" messages</li>\n        <li>\n            <!-- inner menu: contains the messages -->\n            <ul class=\"menu\">\n");
            EndContext();
#line 13 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\Shared\Components\MenuMessage\LayoutMenuMessage.cshtml"
             foreach (var message in Model)
            {

#line default
#line hidden

            BeginContext(541, 86, true);
            WriteLiteral("                <li>\n                    <!-- start message -->\n                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 627, "\"", 650, 1);
            WriteAttributeValue("", 634, 
#line 17 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\Shared\Components\MenuMessage\LayoutMenuMessage.cshtml"
                              message.URLPath

#line default
#line hidden
            , 634, 16, false);
            EndWriteAttribute();
            BeginContext(651, 130, true);
            WriteLiteral(">\n                        <div class=\"pull-left\">\n                            <!-- User Image -->\n                            <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 781, "\"", 805, 1);
            WriteAttributeValue("", 787, 
#line 20 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\Shared\Components\MenuMessage\LayoutMenuMessage.cshtml"
                                       message.AvatarURL

#line default
#line hidden
            , 787, 18, false);
            EndWriteAttribute();
            BeginContext(806, 179, true);
            WriteLiteral(" class=\"img-circle\" alt=\"ui\">\n                        </div>\n                        <!-- Message title and timestamp -->\n                        <h4>\n                            ");
            EndContext();
            BeginContext(986, 19, false);
            Write(
#line 24 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\Shared\Components\MenuMessage\LayoutMenuMessage.cshtml"
                             message.DisplayName

#line default
#line hidden
            );
            EndContext();
            BeginContext(1005, 66, true);
            WriteLiteral("\n                            <small><i class=\"fa fa-clock-o\"></i> ");
            EndContext();
            BeginContext(1072, 16, false);
            Write(
#line 25 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\Shared\Components\MenuMessage\LayoutMenuMessage.cshtml"
                                                                  message.TimeSpan

#line default
#line hidden
            );
            EndContext();
            BeginContext(1088, 111, true);
            WriteLiteral("</small>\n                        </h4>\n                        <!-- The message -->\n                        <p>");
            EndContext();
            BeginContext(1200, 17, false);
            Write(
#line 28 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\Shared\Components\MenuMessage\LayoutMenuMessage.cshtml"
                            message.ShortDesc

#line default
#line hidden
            );
            EndContext();
            BeginContext(1217, 52, true);
            WriteLiteral("</p>\n                    </a>\n                </li>\n");
            EndContext();
#line 31 "C:\Users\jocutuli\Desktop\Commons MVC\Commons MVC\CommonsDev\Views\Shared\Components\MenuMessage\LayoutMenuMessage.cshtml"
            }

#line default
#line hidden

            BeginContext(1283, 177, true);
            WriteLiteral("                <!-- end message -->\n            </ul>\n            <!-- /.menu -->\n        </li>\n        <li class=\"footer\"><a href=\"#\">See All Messages</a></li>\n    </ul>\n</li>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Commons.Models.Message>> Html { get; private set; }
    }
}
#pragma warning restore 1591
