using Floreview.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Floreview.DataAccess.Interfaces
{
    public interface IIdentityManager
    {
        bool AddUserToRole(string userId, string roleName);

        void ClearUserRoles(string userId);

        IdentityResult Create(ApplicationUser user, string password);

        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string auth);

        bool CreateRole(string name, string description = "");

        ApplicationUser FindAsync(string userName, string password);

        bool RoleExists(string name);

        ApplicationUser GetUser(string userName);
    }
}
