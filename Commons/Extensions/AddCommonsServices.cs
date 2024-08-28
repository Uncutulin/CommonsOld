using System;
using Commons.Identity;
using Commons.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Commons.Extensions
{
    public static partial class Extensions
    {
        public static void AddCommonsServices<TUser, TContext>(this IServiceCollection services)
            where TUser : CommonsUser
            where TContext : CommonsDbContext<TUser>
        {
            // Db Context. Must inherit from CommonsDbContext.
            services.AddTransient<ICommonsDbContext, TContext>();

            // Roles
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<RolesService>();

            // Users
            services.AddTransient<IUserService, UserService<TUser>>();
            services.AddTransient<UserManager<TUser>, UserService<TUser>>();
            services.AddTransient<UserService<TUser>>();

            // Claims
            services.AddTransient<IClaimsService, ClaimsService<TUser>>();
            services.AddTransient<ClaimsService<TUser>>();

            // SignIn
            services.AddTransient<ISignInService, SignInService<TUser>>();
            services.AddTransient<SignInManager<TUser>, SignInService<TUser>>();

            // HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Identity
            services.AddIdentity<TUser, CommonsRole>()
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders()
                //.AddDefaultUI()
                .AddSignInManager<SignInService<TUser>>();

            // Session
            services.AddSession();

            // MVC - JSON Options
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

    }

}
