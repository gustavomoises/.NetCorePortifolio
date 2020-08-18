#pragma checksum "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a540e154b00c496b19f04403075a4ffe5a170864"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_RecordsShow), @"mvc.1.0.view", @"/Views/Customer/RecordsShow.cshtml")]
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
#line 7 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\_ViewImports.cshtml"
using TableReady.Core.App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\_ViewImports.cshtml"
using TableReady.Core.App.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a540e154b00c496b19f04403075a4ffe5a170864", @"/Views/Customer/RecordsShow.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c385e059e264da2a028405a1ef1e89e8c7deb565", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_RecordsShow : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TableReady.Core.App.Models.RecordModelView>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 8 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
  
    ViewData["Title"] = "Reservations";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h4>Reservations</h4>\r\n<hr />\r\n<br />\r\n<div>\r\n    ");
#nullable restore
#line 16 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
Write(await Component.InvokeAsync("CustomerReservations", ViewBag.CustomerID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n<h4>Waitlist</h4>\r\n<hr />\r\n<br />\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 25 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
           Write(Html.DisplayNameFor(model => model.Restaurant));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 28 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
           Write(Html.DisplayNameFor(model => model.RecordDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 31 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
           Write(Html.DisplayNameFor(model => model.PartySize));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 34 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
           Write(Html.DisplayNameFor(model => model.EntryOrigin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 38 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
           Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 44 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 48 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
               Write(Html.DisplayFor(modelItem => item.Restaurant));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 51 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
               Write(Html.DisplayFor(modelItem => item.RecordDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 54 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
               Write(Html.DisplayFor(modelItem => item.PartySize));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 57 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
               Write(Html.DisplayFor(modelItem => item.EntryOrigin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 60 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
               Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 63 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
               Write(Html.ActionLink("Details", "WaitlistHistoryDetails", new { id = item.Id }, new { @class = "btn btn-primary mx-1" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n\r\n            </tr>\r\n");
#nullable restore
#line 67 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Customer\RecordsShow.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TableReady.Core.App.Models.RecordModelView>> Html { get; private set; }
    }
}
#pragma warning restore 1591