using Commons;
using Commons.Extensions;
using Commons.Identity;
using Commons.Identity.DummyData;
using Commons.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommonsDev.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CommonsDev
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<User>()

            //services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });


            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            //.AddViewLocalization(
            //    LanguageViewLocationExpanderFormat.Suffix,
            //    opts => { opts.ResourcesPath = "Resources"; })
            //.AddDataAnnotationsLocalization();
            services.AddCommonsLibraryViews();
            services.AddCommonsServices<User, ApplicationDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env
        , UserService<User> userService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCommonsLibraryScripts();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseCookiePolicy();

            //DummyAdmin.Initialize(userService).Wait();

        }
    }
}
