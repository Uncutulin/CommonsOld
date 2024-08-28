using System.Reflection;
using CommonsUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Commons.Extensions
{
    public static partial class Extensions
    {
        public static void AddCommonsLibraryViews(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(Commons.ViewComponents.RegisterCommonsOnlineViewComponent)
                    .GetTypeInfo().Assembly));
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(Class1)
                    .GetTypeInfo().Assembly));
            });
        }

        public static void UseCommonsLibraryScripts(this IApplicationBuilder builder)
        {
            var embeddedProvider = new EmbeddedFileProvider(typeof(Commons.ViewComponents.RegisterCommonsOnlineViewComponent)
                .GetTypeInfo().Assembly, "Commons.wwwroot.js");

            builder.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = embeddedProvider,
                RequestPath = new PathString("/js")
            });

            embeddedProvider = new EmbeddedFileProvider(typeof(Commons.ViewComponents.RegisterCommonsOnlineViewComponent)
                .GetTypeInfo().Assembly, "Commons.wwwroot.css");

            builder.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = embeddedProvider,
                RequestPath = new PathString("/css")
            });

            embeddedProvider = new EmbeddedFileProvider(typeof(Class1)
                .GetTypeInfo().Assembly, "CommonsUI.wwwroot.js");

            builder.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = embeddedProvider,
                RequestPath = new PathString("/js")
            });

            embeddedProvider = new EmbeddedFileProvider(typeof(Class1)
                .GetTypeInfo().Assembly, "CommonsUI.wwwroot.css");

            builder.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = embeddedProvider,
                RequestPath = new PathString("/css")
            });
        }
    }


}
