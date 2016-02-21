using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.SwaggerGen;
using Swashbuckle.SwaggerGen.XmlComments;

namespace LearningASP.NETCore
{
    public class Startup
    {
        //private readonly string pathXml = @"\\Mac\Home\Desktop\asp.net core\learn_asp.net core 1.0\artifacts\bin\SwaggerForASP.NETCore\Debug\dnx451\SwaggerForASP.NETCore1.0.xml";
        private readonly string pathXml = @"C:\Users\irving\Desktop\asp.net core\LearningASP.NETCore\artifacts\bin\LearningASP.NETCore\Debug\dnx451\LearningASP.NETCore.xml";

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSwaggerGen();

            services.ConfigureSwaggerDocument(options =>
            {
                options.SingleApiVersion(new Info
                {
                    License = new License { Name = "irving", Url = @"http://cnblogs.com/irving" },
                    Contact = new Contact { Name = "irving", Email = "zhouyongtao@outlook.com", Url = @"http://cnblogs.com/irving" },
                    Version = "v1",
                    Title = "ASP.NET Core 1.0 WebAPI",
                    Description = "A Simple For ASP.NET Core 1.0 WebAPI",
                    TermsOfService = "None"
                });
                options.OperationFilter(new ApplyXmlActionComments(pathXml));
            });

            services.ConfigureSwaggerSchema(options =>
            {
                options.IgnoreObsoleteProperties = true;
                options.DescribeAllEnumsAsStrings = true;
                options.ModelFilter(new ApplyXmlTypeComments(pathXml));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwaggerGen();

            app.UseSwaggerUi();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
