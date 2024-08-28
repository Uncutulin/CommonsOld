using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SQLitePCL;

namespace Commons.Identity.Services
{
    public class RolesService : RoleManager<CommonsRole>, IRolesService
    {
        private readonly ICommonsDbContext _context;

        public RolesService(ICommonsDbContext context
            , IRoleStore<CommonsRole> store
            , IEnumerable<IRoleValidator<CommonsRole>> roleValidators
            , ILookupNormalizer keyNormalizer
            , IdentityErrorDescriber errors
            , ILogger<RoleManager<CommonsRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
            _context = context;
        }

        public async Task<CommonsRole> Find(Expression<Func<CommonsRole, bool>> predicate)
        {
            return await _context.Roles.Include(x => x.RoleFunctions).ThenInclude(f => f.Function)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<List<CommonsRole>> GetRoles(Expression<Func<CommonsRole, bool>> predicate)
        {
            return await _context.Roles.Include(x => x.RoleFunctions).ThenInclude(f => f.Function)
                .Where(predicate).ToListAsync();
        }

        public async Task Create(string roleName, string description, string workSpaceId, DateTime? expiration = null)
        {
            var newRole = new CommonsRole(roleName, description, workSpaceId, expiration);
            await CreateAsync(newRole);
        }
        
        //public async Task UpdateClaims(string id)
        //{
        //    var role = await Roles.Include(x => x.RoleFunctions).ThenInclude(f => f.Function).FirstAsync(x => x.Id == id);
            
        //    foreach (Claim claim in await GetClaimsAsync(role))
        //    {
        //        await RemoveClaimAsync(role, claim);
        //    }

        //    if (!role.Enabled) return;

        //    List<string> routes = role.RoleFunctions.Select(x => x.Function).SelectMany(x => x.GetRoutes())
        //        .Select(x => x.Url).Distinct().ToList();
            
        //    await AddClaimAsync(role, new Claim(role.WorkSpaceId, JsonConvert.SerializeObject(routes)));
        //}

        public async Task Update(CommonsRole role)
        {
            _context.Entry(role).State = EntityState.Detached;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            //await UpdateClaims(role.Id);
        }

        public async Task AddFunction(CommonsRole role, CommonsFunction function)
        {
            _context.Entry(role).State = EntityState.Detached;
            _context.Entry(function).State = EntityState.Detached;
            await _context.AspNetRoleFunctions.AddAsync(
                new CommonsRoleFunction()
                {
                    Function = function,
                    Role = role
                }
                );
            await _context.SaveChangesAsync();
            //await UpdateClaims(role.Id);
        }

        public async Task Delete(CommonsRole role)
        {
            _context.Entry(role).State = EntityState.Detached;
            role.RoleFunctions = new List<CommonsRoleFunction>();
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            //await UpdateClaims(role.Id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task Disable(CommonsRole role)
        {
            _context.Entry(role).State = EntityState.Detached;
            role.Enabled = false;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            //await UpdateClaims(role.Id);
        }

        public async Task Enable(CommonsRole role)
        {
            _context.Entry(role).State = EntityState.Detached;
            role.Enabled = true;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            //await UpdateClaims(role.Id);
        }
    }
}
