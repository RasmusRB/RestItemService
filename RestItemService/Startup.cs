using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;

namespace RestItemService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Items API",
                    Version = "v1.0",
                    Description = "Example of OpenAPI for api/localItems",
                    TermsOfService = null,

                    //Contact = new Contact()
                    //{
                    //    name = "{your-name}",
                    //    email = "{your-email}",
                    //    url = "{your-url}"
                    //},

                    //License = new License()
                    //{
                    //    Name = "No licence required",
                    //    Url = String.Empty

                    //}
                });
            
                //var xmlFile = "{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAnyOrigin", builder => builder.AllowAnyOrigin());
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Items API v1.0");
                c.RoutePrefix = "api/help";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors(
                options =>
                {
                    // allow everything from everywhere
                    // options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                    // allow only GET/PUT
                    options.WithMethods("GET", "PUT");
                });
            // app.UseCors("AllowAnyOrigin");
        }
    }
}
