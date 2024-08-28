using System.Threading.Tasks;

namespace Commons.Identity.Services
{
    public interface IClaimsService
    {
        Task Refresh(string username);
        Task ChangeSelectedInstitute(string username, string isntituteId);
        Task AddWorkSpaceAdmin(string id, string workSpaceId);
        Task RemoveWorkSpaceAdmin(string id, string workSpaceId);
        Task<bool> IsWorkSpaceAdmin(string id, string workSpaceId);
    }
}