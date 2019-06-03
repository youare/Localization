using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using StringLocalizer.CustomLocalization;
using StringLocalizer.Services;

namespace StringLocalizer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IHelpService, HelpService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            //services.AddLocalization(o=> {
            //    o.ResourcesPath = "Resources";
            //});

            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
            services.Configure<JsonLocalizationOptions>(o => {
                o.ResourcesPath = "JsonResources";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                if (context.Request.Query.ContainsKey("ui-culture"))
                {
                    var tag = context.Request.Query["ui-culture"];
                    CultureInfo.CurrentUICulture = new CultureInfo(tag);
                }
                if (context.Request.Query.ContainsKey("about"))
                {
                    var searchTerm = context.Request.Query["about"];
                    var service = context.RequestServices.GetService<IAboutService>();
                    var content = service.Reply(searchTerm);
                    await context.Response.WriteAsync(content);
                    return;
                }
                if (context.Request.Query.ContainsKey("department"))
                {
                    var department = context.Request.Query["department"];
                    var service = context.RequestServices.GetService<IDepartmentService>();
                    var content = service.GetInfo(department);
                    await context.Response.WriteAsync(content);
                    return;
                }
                if (context.Request.Query.ContainsKey("help"))
                {
                    var serviceName = context.Request.Query["help"];
                    var service = context.RequestServices.GetService<IHelpService>();
                    var content = service.GetHelpFor(serviceName);
                    await context.Response.WriteAsync(content);
                    return;
                }
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
