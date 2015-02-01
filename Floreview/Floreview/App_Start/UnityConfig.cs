using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Web.Http;
using System.Data.Entity;
using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using Floreview.DataAccess.UnitOfWork;
using Floreview.DataAccess.Services;
using Floreview.Models;
using Floreview.DataAccess.Repositories;

namespace Floreview
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<DbContext, FlowerContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            container.RegisterType<IAccessService, AccessService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserManagementService, UserManagementService>(new HierarchicalLifetimeManager());container.RegisterType<IIdentityManager, IdentityManagerRepository>(new HierarchicalLifetimeManager());
            
            container.RegisterType<IIdentityManager, IdentityManagerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILocation, LocationRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICompany, CompanyRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBlog, BlogRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBlogCategory, BlogCategoryRepository>(new HierarchicalLifetimeManager());
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}