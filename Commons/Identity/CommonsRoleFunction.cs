using System;
using System.Collections.Generic;
using System.Text;
using Commons.Models;

namespace Commons.Identity
{
    public class CommonsRoleFunction : Relational 
    {
        public virtual CommonsRole Role { get; set; }
        public virtual CommonsFunction Function { get; set; }
    }
}
