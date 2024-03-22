#pragma checksum "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4b05b6b1fc29712fbf930ea1d11856d388b981e6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\_ViewImports.cshtml"
using mongoDBproject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\_ViewImports.cshtml"
using mongoDBproject.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4b05b6b1fc29712fbf930ea1d11856d388b981e6", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9590a990c9d2a62736a51f2bce8f2c6c4e120105", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Переходы между страницами</h1>\r\n    ");
#nullable restore
#line 7 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Книги", "GetListBook"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 8 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Жанры", "GetListGenre"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 9 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Пользователи", "GetListUser"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 10 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Заказы", "GetListOrder"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 11 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Заказанные книги", "GetListOrderBook"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 12 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Книги для редактирования", "GetListLibrarianBook"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 13 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Жанры для редактирования", "GetListLibrarianGenre"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 14 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Уведомления", "GetListLibrarianOrderBook"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 15 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Книги на покупку", "GetListClientBook"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 16 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Мои книги", "GetListClientOrder"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 17 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Отчёт по пользователям", "GetAggregateUsers"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 18 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Отчёт по заказам", "GetAggregateOrders"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 19 "C:\Users\Serge\source\repos\mongoDBproject\mongoDBproject\Views\Home\Index.cshtml"
Write(Html.ActionLink("Отчёт по книгам", "GetAggregateBooks"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>");
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
