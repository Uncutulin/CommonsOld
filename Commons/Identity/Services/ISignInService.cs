using System.Threading.Tasks;

namespace Commons.Identity.Services
{
    public interface ISignInService
    {
        Task SignOutAsync();
        Task SignInAsync(string username);
    }
}