using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Floreview.DataAccess.Repositories
{
    public class IdentityManagerRepository : IIdentityManager
    {

        private FlowerContext _db = null;
        private RoleManager<ApplicationRole> _roleManager = null;
        private UserManager<ApplicationUser> _userManager = null;


        public IdentityManagerRepository()
        {

        }

        public IdentityManagerRepository(FlowerContext context)
        {
            _db = context;
            _roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(_db));
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

        }

        public ApplicationUser FindAsync(string userName, string password)
        {
            return _userManager.Find(userName, password);
        }

        public ApplicationUser GetUser(string userName)
        {
            return _userManager.FindByName(userName);
        }

        public bool RoleExists(string name)
        {
            return _roleManager.RoleExists(name);
        }


        public bool CreateRole(string name, string description = "")
        {
            var idResult = _roleManager.Create(new ApplicationRole(name, description));
            return idResult.Succeeded;
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string auth)
        {
            return _userManager.CreateIdentityAsync(user, auth);
        }

        public IdentityResult Create(ApplicationUser user, string password)
        {
            return _userManager.Create(user, password);
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(string userId)
        {
            var user = _userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                _userManager.RemoveFromRole(userId, role.Role.Name);
            }
        }
    }

}