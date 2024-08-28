using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CommonsDev
{
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        CreateWebHostBuilder(args).Build().Run();
    //    }

    //    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    //        WebHost.CreateDefaultBuilder(args)
    //            .UseStartup<Startup>();
    //}


    // Program from SIGEM
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    var sharedFolder = Path.Combine(env.ContentRootPath, "..", "UIL");

                    config
                        .AddJsonFile(Path.Combine(sharedFolder, "appsettings.json"), optional: true)
                        .AddJsonFile(Path.Combine(sharedFolder, $"appsettings.{env.EnvironmentName}.json"), optional: true);

                    config.AddEnvironmentVariables();
                })
                .UseStartup<Startup>()
        ;
    }
}
