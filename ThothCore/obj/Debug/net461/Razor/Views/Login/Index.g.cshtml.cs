#pragma checksum "D:\projects\ThothCore\ThothCore\Views\Login\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "34b609e2bc09628b3ab4b65d4a5e411a6a786fb2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Login_Index), @"mvc.1.0.view", @"/Views/Login/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Login/Index.cshtml", typeof(AspNetCore.Views_Login_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\projects\ThothCore\ThothCore\Views\_ViewImports.cshtml"
using ThothCore;

#line default
#line hidden
#line 2 "D:\projects\ThothCore\ThothCore\Views\_ViewImports.cshtml"
using ThothCore.Domain.Models;

#line default
#line hidden
#line 3 "D:\projects\ThothCore\ThothCore\Views\_ViewImports.cshtml"
using React.AspNet;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34b609e2bc09628b3ab4b65d4a5e411a6a786fb2", @"/Views/Login/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"660ac6e2fc5c83aa8fdeb0dd976c1a69930a3dc4", @"/Views/_ViewImports.cshtml")]
    public class Views_Login_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "D:\projects\ThothCore\ThothCore\Views\Login\Index.cshtml"
  
	ViewData["Title"] = "Hello React";

#line default
#line hidden
            BeginContext(46, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(49, 81, false);
#line 6 "D:\projects\ThothCore\ThothCore\Views\Login\Index.cshtml"
Write(Html.React("CommentBox", new
{
	initialData = Model,
	pollInterval = 2000,
}));

#line default
#line hidden
            EndContext();
            BeginContext(130, 3, true);
            WriteLiteral(";\r\n");
            EndContext();
            BeginContext(163, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(184, 248, true);
                WriteLiteral("\r\n\t<script crossorigin src=\"https://cdnjs.cloudflare.com/ajax/libs/react/16.8.0/umd/react.development.js\"></script>\r\n\t<script crossorigin src=\"https://cdnjs.cloudflare.com/ajax/libs/react-dom/16.8.0/umd/react-dom.development.js\"></script>\r\n\t<script");
                EndContext();
                BeginWriteAttribute("src", " src=\"", 432, "\"", 468, 1);
#line 17 "D:\projects\ThothCore\ThothCore\Views\Login\Index.cshtml"
WriteAttributeValue("", 438, Url.Content("~/js/login.jsx"), 438, 30, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(469, 12, true);
                WriteLiteral("></script>\r\n");
                EndContext();
            }
            );
            BeginContext(484, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(487, 26, false);
#line 20 "D:\projects\ThothCore\ThothCore\Views\Login\Index.cshtml"
Write(Html.ReactInitJavaScript());

#line default
#line hidden
            EndContext();
            BeginContext(513, 4, true);
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
