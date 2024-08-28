using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace Commons.Extensions.Attributes
{

    /// <summary>
    /// Used on an EntityFramework Entity class to mark a property to be used as a Unique Key
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UniqueAttribute : RequiredAttribute
    {
        /// <summary>
        /// Marker attribute for unique key
        /// </summary>
        /// <param name="groupId">Optional, used to group multiple entity properties together into a combined Unique Key</param>
        /// <param name="order">Optional, used to order the entity properties that are part of a combined Unique Key</param>
        public UniqueAttribute(string groupId = null, int order = 0)
        {
            GroupId = groupId;
            Order = order;
        }


        public string GroupId { get; set; }
        public int Order { get; set; }
    }
}
