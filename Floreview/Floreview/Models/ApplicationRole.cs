using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class ApplicationRole : IdentityRole
    {
        public virtual String Description { get; set; }

        public ApplicationRole() : base()
        {

        }

        public ApplicationRole(String name, String description) : base(name)
        {
            this.Description = description;
        }
    }
}