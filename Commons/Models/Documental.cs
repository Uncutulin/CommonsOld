using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Commons.Models
{
    public abstract class Documental
    {
        public string Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
        public string DeletedById { get; set; }
        public DateTime LastEditTime { get; set; } = DateTime.Now;
        public string LastEditById { get; set; }
        public bool Display { get; set; } = true;

        public bool IsVisible => DeletedDate == null && Display;
        public bool IsActive => DeletedDate == null;

        /// <summary>
        /// For retro compatibility with Visible.
        /// </summary>
        public bool Show
        {
            get => Display;
            set => Display = value;
        }

        public void Delete()
        {
            DeletedDate = DateTime.Now;
        }

        public void Edited()
        {
            LastEditTime = DateTime.Now;
        }
    }

    public abstract class Documental<TUser> : Documental where TUser : IdentityUser
    {
        [ForeignKey("DeletedById")]
        public virtual TUser DeletedBy { get; set; }
        [ForeignKey("LastEditById")]
        public virtual TUser LastEditBy { get; set; }

        public void Delete(TUser user)
        {
            DeletedDate = DateTime.Now;
            DeletedBy = user;
        }

        public void Edited(TUser user)
        {
            LastEditTime = DateTime.Now;
            LastEditBy = user;
        }
    }
}
