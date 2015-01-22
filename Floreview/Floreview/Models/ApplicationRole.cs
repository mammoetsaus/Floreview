using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class ApplicationRole : IdentityRole
    {
        #region Fields & Props
        public virtual String Description { get; set; }
        #endregion

        #region Constructor
        public ApplicationRole() : base()
        {

        }

        public ApplicationRole(String name, String description) : base(name)
        {
            this.Description = description;
        }
        #endregion
    }
}