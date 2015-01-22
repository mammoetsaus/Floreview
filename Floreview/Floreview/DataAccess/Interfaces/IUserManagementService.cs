using Floreview.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Floreview.DataAccess.Interfaces
{
    public interface IUserManagementService
    {
        IdentityResult Create(ApplicationUser user, string password);

        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string auth);

        ApplicationUser FindAsync(string userName, string password);

        bool AddUserToRoleUser(string userId);

        ApplicationUser GetUser(string userName);
    }
}
