#pragma checksum "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f3901c11661c710ae302531bffd1542f897096be"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Restaurant_HostManage), @"mvc.1.0.view", @"/Views/Restaurant/HostManage.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f3901c11661c710ae302531bffd1542f897096be", @"/Views/Restaurant/HostManage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c385e059e264da2a028405a1ef1e89e8c7deb565", @"/Views/_ViewImports.cshtml")]
    public class Views_Restaurant_HostManage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TableReady.Core.App.Models.WaitCustomerModelView>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 8 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
  
    ViewData["Title"] = "Host Management";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h3>Host Controls</h3>\r\n<hr />\r\n<br />\r\n\r\n<h4>Waiting for Table</h4>\r\n<div>\r\n    ");
#nullable restore
#line 18 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
Write(await Component.InvokeAsync("CheckinReservations", ViewBag.RestaurantID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n<h4>Reservations</h4>\r\n<div>\r\n    ");
#nullable restore
#line 22 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
Write(await Component.InvokeAsync("HostReservationManage", ViewBag.RestaurantID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n<h4>Waitlist</h4>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 29 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
           Write(Html.DisplayNameFor(model => model.WaitlistPosition));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 32 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
           Write(Html.DisplayNameFor(model => model.CustomerName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 35 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
           Write(Html.DisplayNameFor(model => model.PartySizew));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 38 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
           Write(Html.DisplayNameFor(model => model.EntryOriginw));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 41 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
           Write(Html.DisplayNameFor(model => model.WaitlistStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 47 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 51 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
               Write(Html.DisplayFor(modelItem => item.WaitlistPosition));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 54 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
               Write(Html.DisplayFor(modelItem => item.CustomerName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 57 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
               Write(Html.DisplayFor(modelItem => item.PartySizew));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 60 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
               Write(Html.DisplayFor(modelItem => item.EntryOriginw));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 63 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
               Write(Html.DisplayFor(modelItem => item.WaitlistStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 66 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
               Write(Html.ActionLink("Cancel", "WaitlistCancel", new { id = item.WaitlistEntryId }, new { @class = "btn btn-danger mx-1" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                    ");
#nullable restore
#line 67 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
               Write(Html.ActionLink("No Show", "WaitlistNoShow", new { id = item.WaitlistEntryId }, new { @class = "btn btn-warning mx-1" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 70 "C:\Users\849950\source\repos\TableReady.Core_Final\TableReady.Core.App\Views\Restaurant\HostManage.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TableReady.Core.App.Models.WaitCustomerModelView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
