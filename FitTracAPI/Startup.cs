using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FitTracAPI.CentralHub;
using FitTracAPI.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace FitTracAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<FitTracAPIContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("FitTracAPIContext")));

            services.AddTransient<Controllers.ExercisesController, Controllers.ExercisesController>();

            //Registering Azure SignalR service
            services.AddSignalR();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "FitTracAPI",
                    Version = "v1",
                    Description = "A web API providing a custom toolkit for Managing Fitness Programs",
                    Contact = new Contact
                    {
                        Name = "Jaime Wu",
                        Email = "jaime.wu011@gmail.com",
                        Url = "https://github.com/Zefty/FitTracAPI"
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScribrAPI V1");
                c.RoutePrefix = string.Empty; // launch swagger from root
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // SignalR
            app.UseFileServer();
            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalrHub>("/hub");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
