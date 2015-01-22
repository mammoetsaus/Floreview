using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Floreview.DataAccess.Services
{
    public class UserManagementService : IUserManagementService
    {

        private IIdentityManager identityRepository = null;

        public UserManagementService()
        {

        }

        public UserManagementService(IIdentityManager repo)
        {
            this.identityRepository = repo;
        }

        public ApplicationUser FindAsync(string userName, string password)
        {
            return identityRepository.FindAsync(userName, password);
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string auth)
        {
            return identityRepository.CreateIdentityAsync(user, auth);
        }

        public IdentityResult Create(ApplicationUser user, string password)
        {
            return identityRepository.Create(user, password);
        }

        public bool AddUserToRoleUser(string userId)
        {
            return identityRepository.AddUserToRole(userId, "User");
        }

        public ApplicationUser GetUser(string userName)
        {
            return identityRepository.GetUser(userName);
        }
    }
}