using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Commons.Identity.Services
{
    public static class UserClaims
    {
        public static string FirstName => "User Firstname";
        public static string MiddleName => "User Middlename";
        public static string LastName => "User Lastname";
        public static string UserName => "User Username";
        public static string ProfileType => "User Profile_type";
    }

    public static class SettingsClaims
    {
        public static string SidebarCollapsed => "Settings Sidebar_Collapsed";
        public static string SelectedWorkSpaceId => "Settings Selected_WorkSpace_Id";
        public static string SelectedWorkSpaceName => "Settings Selected_WorkSpace_Name";
    }

    public class ClaimsService<TUser> : IClaimsService
        where TUser : CommonsUser
    {
        private readonly UserService<TUser> _userService;
        private readonly ICommonsDbContext _context;

        public ClaimsService(UserService<TUser> userService, ICommonsDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        /// <summary>
        /// Refresh user claims.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task Refresh(string username)
        {
            TUser applicationUser = await _userService.FindByNameAsync(username);

            if (applicationUser == null)
            {
                return;
            }

            // Actualizo las user claims

            IList<Claim> userClaims = await _userService.GetClaimsAsync(applicationUser);

            foreach (Claim userClaim in userClaims)
            {
                if (userClaim.Type == UserClaims.FirstName
                    || userClaim.Type == UserClaims.MiddleName
                    || userClaim.Type == UserClaims.LastName
                    || userClaim.Type == UserClaims.UserName
                    || userClaim.Type == UserClaims.ProfileType
                    || (userClaim.Type == SettingsClaims.SelectedWorkSpaceId && userClaim.Value == "Default WorkSpace")
                    || (userClaim.Type == SettingsClaims.SelectedWorkSpaceName && userClaim.Value == "Default WorkSpace")
                    )
                {
                    await _userService.RemoveClaimAsync(applicationUser, userClaim);
                }
            }

            await _userService.AddClaimAsync(applicationUser,
                new Claim(UserClaims.FirstName, applicationUser.GetFirstName() ?? " ")
                );

            await _userService.AddClaimAsync(applicationUser,
                new Claim(UserClaims.MiddleName, applicationUser.GetMiddleName() ?? " ")
            );

            await _userService.AddClaimAsync(applicationUser,
                new Claim(UserClaims.LastName, applicationUser.GetLastName() ?? " ")
            );

            await _userService.AddClaimAsync(applicationUser,
                new Claim(UserClaims.ProfileType, applicationUser.GetRoleString() ?? " ")
            );

            // Checkeo si tiene los UsserSettingsClaims


            if (userClaims.All(x => x.Type != SettingsClaims.SelectedWorkSpaceId)
                || userClaims.All(x => x.Type != SettingsClaims.SelectedWorkSpaceName))
            {
                var workSpaces = applicationUser.GetRelatedIWorkSpaces();
                if (workSpaces.Count != 0)
                {
                    await _userService.AddClaimAsync(applicationUser,
                        new Claim(SettingsClaims.SelectedWorkSpaceId, workSpaces.First().Id)
                        );
                    await _userService.AddClaimAsync(applicationUser,
                        new Claim(SettingsClaims.SelectedWorkSpaceName, workSpaces.First().Name)
                    );
                }
                else
                {
                    var workspace = _context.GetIWorkSpaces().FirstOrDefault();
                    if (await _userService.IsAdmin(applicationUser.UserName) && workspace != null)
                    {
                        await _userService.AddClaimAsync(applicationUser,
                            new Claim(SettingsClaims.SelectedWorkSpaceId, workspace.Id)
                        );
                        await _userService.AddClaimAsync(applicationUser,
                            new Claim(SettingsClaims.SelectedWorkSpaceName, workspace.Name)
                        );
                    }
                    else
                    {
                        await _userService.AddClaimAsync(applicationUser,
                            new Claim(SettingsClaims.SelectedWorkSpaceId, "Default WorkSpace")
                        );
                        await _userService.AddClaimAsync(applicationUser,
                            new Claim(SettingsClaims.SelectedWorkSpaceName, "Default WorkSpace")
                        );

                    }
                }
            }

        }

        public async Task ChangeSelectedInstitute(string username, string instituteId)
        {
            TUser applicationUser = await _userService.FindByNameAsync(username);

            var institute = _context.GetIWorkSpaces().FirstOrDefault(x => x.Id == instituteId);

            if (institute == null) return;

            if (applicationUser.GetRelatedIWorkSpaces().All(x => x.Id != instituteId) &&
                !await _userService.IsAdmin(applicationUser.UserName)) return;

            foreach (Claim userClaim in await _userService.GetClaimsAsync(applicationUser))
            {
                if (userClaim.Type == SettingsClaims.SelectedWorkSpaceId
                    || userClaim.Type == SettingsClaims.SelectedWorkSpaceName
                )
                {
                    await _userService.RemoveClaimAsync(applicationUser, userClaim);
                }
            }

            await _userService.AddClaimAsync(applicationUser,
                new Claim(SettingsClaims.SelectedWorkSpaceId, institute.Id)
            );
            await _userService.AddClaimAsync(applicationUser,
                new Claim(SettingsClaims.SelectedWorkSpaceName, institute.Name)
            );
        }

        public async Task AddWorkSpaceAdmin(string id, string workSpaceId)
        {
            var user = await _userService.FindByIdAsync(id);
            var claims = await _userService.GetClaimsAsync(user);

            if (claims.Any(x => x.Type == "WorkSpaceAdmin" && x.Value == workSpaceId)) return;

            await _userService.AddClaimAsync(user, new Claim("WorkSpaceAdmin", workSpaceId));
        }

        public async Task RemoveWorkSpaceAdmin(string id, string workSpaceId)
        {
            var user = await _userService.FindByIdAsync(id);
            var claim = _userService.GetClaimsAsync(user).Result
                .FirstOrDefault(x => x.Type == "WorkSpaceAdmin" && x.Value == workSpaceId);

            if (claim == null) return;

            await _userService.RemoveClaimAsync(user, claim);
        }

        public async Task SetAdmin(string userName, bool admin)
        {
            TUser applicationUser = await _userService.FindByNameAsync(userName);

            await _userService.RemoveClaimsAsync(applicationUser,
                claims: _userService.GetClaimsAsync(applicationUser).Result.Where(x => x.Type == "IsAdmin"));

            if (admin) await _userService.AddClaimAsync(applicationUser, new Claim("IsAdmin", DateTime.Now.ToShortDateString()));
        }

        public async Task<bool> IsWorkSpaceAdmin(string id, string workSpaceId)
        {
            var user = await _userService.FindByIdAsync(id);
            var claim = _userService.GetClaimsAsync(user).Result
                .FirstOrDefault(x => x.Type == "WorkSpaceAdmin" && x.Value == workSpaceId);

            return (claim != null);
        }
    }
}
