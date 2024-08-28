using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.Identity;

namespace CommonsDev.Data
{
    public class User : CommonsUser
    {
        public override string GetFirstName()
        {
            return "Default";
        }

        public override string GetMiddleName()
        {
            return "Default";
        }

        public override string GetLastName()
        {
            return "Default";
        }

        public override string GetRoleString()
        {
            return "User";
        }

        public override List<IWorkSpace> GetRelatedIWorkSpaces()
        {
            return new List<IWorkSpace>();
        }
    }
}
