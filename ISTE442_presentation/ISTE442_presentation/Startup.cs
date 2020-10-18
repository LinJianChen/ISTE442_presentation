using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISTE442_presentation.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ISTE442_presentation
{
    public class Startup
    {
        public IConfiguration configuration { get; set; }
        public Startup(IConfiguration config)
        {
            configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //add framework services here
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.Add(new ServiceDescriptor(typeof(ContactStoreContext), new ContactStoreContext(configuration.GetConnectionString("DBConnection"))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //create mvc route
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Contact}/{action=Index}/{id?}");
            });
        }
    }
}
