using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Commons.Identity.Services
{
    public class SignInService<TUser> : Microsoft.AspNetCore.Identity.SignInManager<TUser>, ISignInService
        where TUser : CommonsUser
    {
        private readonly UserService<TUser> _userManager;
        private readonly ClaimsService<TUser> _claimsService;
        private readonly IHostingEnvironment _env;
        private readonly IHttpContextAccessor _http;

        public SignInService(IHostingEnvironment environment, ClaimsService<TUser> claimsService, UserService<TUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TUser>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
            _userManager = userManager;
            _claimsService = claimsService;
            _env = environment;
            _http = contextAccessor;
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            await _claimsService.Refresh(userName);
            
            return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public override async Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null)
        {
            await _claimsService.Refresh(user.UserName);

            await base.SignInAsync(user, isPersistent, authenticationMethod);
        }

        public override async Task SignInAsync(TUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {
            await _claimsService.Refresh(user.UserName);

            await base.SignInAsync(user, authenticationProperties, authenticationMethod);
        }

        public async Task<SignInResult> SignInAsAdmin(string userName, string password, bool isPersistent)
        {
            if (_env.IsDevelopment())
            {
                await _claimsService.SetAdmin(userName, true);
            }

            return await PasswordSignInAsync(userName, password, isPersistent, false);
        }

        public async Task SignInAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return;

            await _claimsService.Refresh(user.UserName);

            await SignInAsync(user, false);
        }

        public override Task SignOutAsync()
        {
            _http.HttpContext.Session.Clear();

            return base.SignOutAsync();
        }
    }


}
