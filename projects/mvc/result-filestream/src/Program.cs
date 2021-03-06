using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

    public class HomeController : Controller
    {
        IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = "<html><body><h1>Download File</h1>Download a copy of  <a href=\"/home/hegel\">Hegel (pdf)</a></body></html>",
                ContentType = "text/html"
            };
        }

        public FileStreamResult Hegel()
        {
            var pathToIdeas = System.IO.Path.Combine(_env.WebRootPath, "hegel.pdf");

            //This is a contrite example to demonstrate returning a stream. If you have a physical file on disk, just use PhySicalFileResult that takes a path. 
            return new FileStreamResult(System.IO.File.OpenRead(pathToIdeas), "application/pdf")
            {
                FileDownloadName = "hegel.pdf"
            };
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}