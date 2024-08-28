using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace Commons.Identity
{
    public class CommonsRole : IdentityRole
    {
        protected CommonsRole() : base() { }

        [Key]
        public override string Id { get; set; }

        public CommonsRole(string roleName, string description, string workSpaceId, DateTime? expiration = null) : base(roleName)
        {
            ShowName = roleName;
            Description = description;
            WorkSpaceId = workSpaceId;
            Expiration = expiration;
            Name = $"{workSpaceId}: {roleName}";
        }

        public string ShowName { get; set; }
        public string Description { get; set; }

        public bool Enabled { get; set; } = true;

        public bool Show { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Expiration { get; set; }

        public string WorkSpaceId { get; set; }

        public virtual List<CommonsRoleFunction> RoleFunctions { get; set; } = new List<CommonsRoleFunction>();


        public void AddFunction(CommonsFunction function)
        {
            if (RoleFunctions.Any(x => x.Function == function)) return;
            RoleFunctions.Add(new CommonsRoleFunction()
            {
                Function = function
            });
        }

        public void RemoveFunction(CommonsFunction function)
        {
            RoleFunctions.RemoveAll(x => x.Function == function);

        }
    }

    internal class CommonsRoleDto {
        public string ShowName { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string LastEditTime { get; set; }
        public bool Show { get; set; }
    }
}
