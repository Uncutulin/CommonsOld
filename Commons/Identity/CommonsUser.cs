using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Commons.Identity
{
    public abstract class CommonsUser : IdentityUser
    {
        public abstract string GetFirstName();
        public abstract string GetMiddleName();
        public abstract string GetLastName();
        public abstract string GetRoleString();

        /// <summary>
        /// Return a list with all workspaces that the user can access.
        /// Example: Institutes, Cuarters...
        /// Note: Return empty list if you are not using WorkSpaces.
        /// </summary>
        /// <returns></returns>
        public abstract List<IWorkSpace> GetRelatedIWorkSpaces();
    }
}
