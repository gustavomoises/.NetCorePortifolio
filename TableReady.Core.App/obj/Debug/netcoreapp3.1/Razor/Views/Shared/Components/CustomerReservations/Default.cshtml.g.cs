#pragma checksum "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7691a905dcd75d35d243bb29cbdbfb76c68b4f3c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_CustomerReservations_Default), @"mvc.1.0.view", @"/Views/Shared/Components/CustomerReservations/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7691a905dcd75d35d243bb29cbdbfb76c68b4f3c", @"/Views/Shared/Components/CustomerReservations/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c385e059e264da2a028405a1ef1e89e8c7deb565", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_CustomerReservations_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TableReady.Core.App.Models.ResCustomerModelView>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 8 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
           Write(Html.DisplayNameFor(model => model.Restaurant));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 19 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
           Write(Html.DisplayNameFor(model => model.ReservationDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 22 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
           Write(Html.DisplayNameFor(model => model.PartySize));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 25 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
           Write(Html.DisplayNameFor(model => model.EntryOrigin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 28 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
           Write(Html.DisplayNameFor(model => model.ReservationStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 34 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 38 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
               Write(Html.DisplayFor(modelItem => item.Restaurant));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 41 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
               Write(Html.DisplayFor(modelItem => item.ReservationDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 44 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
               Write(Html.DisplayFor(modelItem => item.PartySize));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 47 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
               Write(Html.DisplayFor(modelItem => item.EntryOrigin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 50 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
               Write(Html.DisplayFor(modelItem => item.ReservationStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 53 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
               Write(Html.ActionLink("Details", "ReservationHistoryDetails", new { id = item.ReservationEntryId }, new { @class = "btn btn-primary mx-1" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 56 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Shared\Components\CustomerReservations\Default.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TableReady.Core.App.Models.ResCustomerModelView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
