using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreApp.Models;
using Microsoft.EntityFrameworkCore;


namespace NetCoreApp
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // better than AddDbContext method
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>(); // AddScope? because we want sql to be alive and available 
          //  services.AddTransient<IEmployeeRepository, SqlEmployeeRepository>();
          //  services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("htmlpage.html");

           
            // app.UseDefaultFiles() does not actually serve to the default file but instead it changes the request part 
            // to point to the default document, in this case to point default.html file in wwwroot folder
            // with "htmlpage" argument we specified which file needs to be considered as default page
            //app.UseDefaultFiles(defaultFilesOptions);

            //Could be used another way to change default pages:

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("htmlpage.html");

            //app.UseFileServer(fileServerOptions);


            // IMPORTANT:
            // UseDefaultFiles() must be registered before UseStaticFiles()
            // UseFileServer combines the functionality of UseStaticFiles, UseDefaultFiles and UseDirectoryBrowser middleware


            // used to get files from wwwroot folder directly.
            app.UseStaticFiles();
            //app.UseMvc(); // if we use app.UseMvc() we will need to put [Route("~/Home")] attribute routings in controller classes
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            }); // => doesnt come with any route services, thats why we pass arguments manually
            //app.UseMvcWithDefaultRoute(); //=> comes with route services
            //app.UseFileServer();
            //app.UseRouting();
            //app.Run(async (context) => 
            //    {
            //        //throw new Exception("Some erro processing the request");
            //        await context.Response.WriteAsync("Hosting Environment: " + env.EnvironmentName);
            //    });

        }
    }
}
