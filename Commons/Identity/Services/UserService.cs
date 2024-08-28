using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Commons.Identity.Services
{
    public class UserService<TUser> : UserManager<TUser>, IUserService
        where TUser : CommonsUser
    {
        private readonly IRolesService _rolesService;

        public UserService( IRolesService rolesService
            , IUserStore<TUser> store
            , IOptions<IdentityOptions> optionsAccessor
            , IPasswordHasher<TUser> passwordHasher
            , IEnumerable<IUserValidator<TUser>> userValidators
            , IEnumerable<IPasswordValidator<TUser>> passwordValidators
            , ILookupNormalizer keyNormalizer
            , IdentityErrorDescriber errors
            , IServiceProvider services
            , ILogger<UserManager<TUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _rolesService = rolesService;
        }

        public async Task<IdentityResult> AddToRoleAsync(string userId, CommonsRole role)
        {
            var user = await FindByIdAsync(userId);
            return await AddToRoleAsync(user, role.Name);
        }

        public async Task<List<CommonsRole>> GetRoles(string userId, string workSpaceId = null)
        {
            var user = await FindByIdAsync(userId);
            var roles = new List<CommonsRole>();
            foreach (string s in await GetRolesAsync(user))
            {
                roles.Add(await _rolesService.Find(x => x.Name == s));
            }

            if (workSpaceId == null)
            {
                return roles;
            }
            else
            {
                return roles.Where(x => x.WorkSpaceId == workSpaceId).ToList();
            }
        }

        public async Task<List<CommonsRole>> GetRolesByUserName(string name, string workSpaceId = null)
        {
            var user = await FindByNameAsync(name);
            var roles = new List<CommonsRole>();
            foreach (string s in await GetRolesAsync(user))
            {
                roles.Add(await _rolesService.Find(x => x.Name == s));
            }

            if (workSpaceId == null)
            {
                return roles;
            }
            else
            {
                return roles.Where(x => x.WorkSpaceId == workSpaceId).ToList();
            }
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(string userId, CommonsRole role)
        {
            var user = await FindByIdAsync(userId);
            
            return await RemoveFromRoleAsync(user, role.Name);
        }

        public async Task<List<IWorkSpace>> GetRelatedWorkSpaces(string name)
        {
            var user = await FindByNameAsync(name);
            return user.GetRelatedIWorkSpaces();
        }

        public async Task<bool> IsAdmin(string name)
        {
            var user = await FindByNameAsync(name);
            return GetClaimsAsync(user).Result.Any(x => x.Type == "IsAdmin");
        }
    }


}
