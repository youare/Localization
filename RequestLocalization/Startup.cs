using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace RequestLocalization
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(o =>
            {
                o.ResourcesPath = "Resources";
            });
            services.Configure<RequestLocalizationOptions>(o=> {
                o.SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("de"),
                    new CultureInfo("bs"),
                    new CultureInfo("es"),
                    new CultureInfo("en"),
                    new CultureInfo("fr-FR"),
                };
                o.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es");
                o.FallBackToParentCultures = true;
                o.FallBackToParentUICultures = true;
                o.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());
                o.RequestCultureProviders.Insert(1, new CountryDomainRequestCultureProvider());
            });
            IMvcCoreBuilder mvcCoreBuilder = services.AddMvcCore();
            mvcCoreBuilder.AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRequestLocalization();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithCulture",
                    template: "{ui-culture}/{controller=Enumerations}/{action=Genders}/{id?}"
                    );
            });
            app.UseMvcWithDefaultRoute();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
