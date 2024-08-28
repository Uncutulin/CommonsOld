using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Identity.Services
{
    public interface IRolesService
    {
        Task Create(string roleName, string description, string workSpaceId, DateTime? expiration = null);
        Task Update(CommonsRole role);
        Task Delete(CommonsRole role);
        Task Disable(CommonsRole role);
        Task Enable(CommonsRole role);
        Task<CommonsRole> Find(Expression<Func<CommonsRole, bool>> predicate);
        Task<List<CommonsRole>> GetRoles(Expression<Func<CommonsRole, bool>> predicate);
        Task AddFunction(CommonsRole role, CommonsFunction function);
    }
}
