using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using Floreview.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.Controllers
{
    public class HomeController : Controller
    {
        #region Fields & Props
        private IAccessService _accessService = null;
        #endregion

        #region Constructor
        public HomeController(IAccessService service)
        {
            _accessService = service;
        }

        public HomeController()
        {

        }
        #endregion

        #region Actions
        public ActionResult Index()
        {
            IndexVM model = new IndexVM();
            model.Blogs = _accessService.GetMostRecentBlogs();

            return View(model);
        }
        #endregion
    }
}