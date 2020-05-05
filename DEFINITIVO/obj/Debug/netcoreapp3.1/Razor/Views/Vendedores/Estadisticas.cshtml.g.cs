#pragma checksum "/Users/jonanderfidalgo/Desktop/UnoMasVenga(30-04-20)/DEFINITIVO/Views/Vendedores/Estadisticas.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "04d95ecac9407908c5522a27375e3d86e93492bf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Vendedores_Estadisticas), @"mvc.1.0.view", @"/Views/Vendedores/Estadisticas.cshtml")]
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
#line 1 "/Users/jonanderfidalgo/Desktop/UnoMasVenga(30-04-20)/DEFINITIVO/Views/_ViewImports.cshtml"
using DEFINITIVO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/jonanderfidalgo/Desktop/UnoMasVenga(30-04-20)/DEFINITIVO/Views/_ViewImports.cshtml"
using DEFINITIVO.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/jonanderfidalgo/Desktop/UnoMasVenga(30-04-20)/DEFINITIVO/Views/Vendedores/Estadisticas.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"04d95ecac9407908c5522a27375e3d86e93492bf", @"/Views/Vendedores/Estadisticas.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"61e378d7026a2879faac2edc283a0b8f3439ed83", @"/Views/_ViewImports.cshtml")]
    public class Views_Vendedores_Estadisticas : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DEFINITIVO.Models.Vendedor>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_MiperfilPV", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<div>\r\n    <div class=\"row\">\r\n        <h3 class=\"col-4 offset-4\">Estadísticas</h3>\r\n    </div>\r\n    <div class=\"row\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "04d95ecac9407908c5522a27375e3d86e93492bf3842", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 13 "/Users/jonanderfidalgo/Desktop/UnoMasVenga(30-04-20)/DEFINITIVO/Views/Vendedores/Estadisticas.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            WriteLiteral(@"        <div class=""col-7 offset-1"" style=""width: 66%;background-color: white;"">
            <canvas id=""myChart"" width=""100%"" height=""80%""></canvas>
        </div>
    </div>
</div>


<script src=""https://cdn.jsdelivr.net/npm/chart.js@2.8.0""></script>
<script>
    var ctx = document.getElementById('myChart').getContext('2d');
    Chart.defaults.global.defaultFontSize = 18;
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [
");
#nullable restore
#line 30 "/Users/jonanderfidalgo/Desktop/UnoMasVenga(30-04-20)/DEFINITIVO/Views/Vendedores/Estadisticas.cshtml"
                 foreach(ProductoVendedor productoVendedor in Model.ProductoVendedor)
                {
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("\'");
#nullable restore
#line 32 "/Users/jonanderfidalgo/Desktop/UnoMasVenga(30-04-20)/DEFINITIVO/Views/Vendedores/Estadisticas.cshtml"
                             Write(productoVendedor.Producto.Titulo);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',");
#nullable restore
#line 32 "/Users/jonanderfidalgo/Desktop/UnoMasVenga(30-04-20)/DEFINITIVO/Views/Vendedores/Estadisticas.cshtml"
                                                                                  
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            ],
            datasets: [{
                label: 'nº de ventas',
                data: [12, 19, 3, 5],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
     ");
            WriteLiteral("           }]\r\n            }\r\n        }\r\n    });</script>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<IdentityUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<IdentityUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DEFINITIVO.Models.Vendedor> Html { get; private set; }
    }
}
#pragma warning restore 1591
