using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Commons.Identity.Services
{
    public interface IUserService : IDisposable
    {
        Task<IdentityResult> AddToRoleAsync(string userId, CommonsRole role);
        Task<List<CommonsRole>> GetRoles(string userId, string workSpaceId = null);
        Task<List<CommonsRole>> GetRolesByUserName(string name, string workSpaceId = null);
        Task<IdentityResult> RemoveFromRoleAsync(string userId, CommonsRole role);
        Task<List<IWorkSpace>> GetRelatedWorkSpaces(string name);
    }
}