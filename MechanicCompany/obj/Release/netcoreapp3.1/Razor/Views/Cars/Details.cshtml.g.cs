#pragma checksum "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3679af83de7375c8d6cf50d2ef4e3905a18ae932"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cars_Details), @"mvc.1.0.view", @"/Views/Cars/Details.cshtml")]
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
#line 1 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\_ViewImports.cshtml"
using MechanicCompany;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\_ViewImports.cshtml"
using MechanicCompany.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3679af83de7375c8d6cf50d2ef4e3905a18ae932", @"/Views/Cars/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f68ee4f3d1f2e5caeaade61f5cdf18bbafc3d129", @"/Views/_ViewImports.cshtml")]
    public class Views_Cars_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MechanicCompany.Models.Car>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
  
    ViewData["Title"] = "Details about car";
    Layout = "~/Views/Shared/_Layout.cshtml";
    var companyMail = ViewBag.CompanyMail;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Details about car</h1>\r\n\r\n<div>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 15 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ApplicationUser));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 18 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayFor(model => model.ApplicationUser.FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 21 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.CarBrand));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 24 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayFor(model => model.CarBrand));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 27 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.CarModel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 30 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayFor(model => model.CarModel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 33 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ProductionYear));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 36 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayFor(model => model.ProductionYear));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 39 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.EngineVolume));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 42 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayFor(model => model.EngineVolume));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 45 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.EngineName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 48 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayFor(model => model.EngineName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 51 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.EnginePower));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 54 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayFor(model => model.EnginePower));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 57 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.EngineTypeOfFuel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 60 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayFor(model => model.EngineTypeOfFuel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 63 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.CarTypeOfBody));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 66 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayFor(model => model.CarTypeOfBody));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 69 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.RegistrationNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 72 "D:\Praca\MechanicCompany\MechanicCompanyApp\MechanicCompany\Views\Cars\Details.cshtml"
       Write(Html.DisplayFor(model => model.RegistrationNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3679af83de7375c8d6cf50d2ef4e3905a18ae93210848", async() => {
                WriteLiteral("<i class=\"fa fa-chevron-circle-left\"></i> Back to cars");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MechanicCompany.Models.Car> Html { get; private set; }
    }
}
#pragma warning restore 1591