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
        private IAccessService _accessService = null;

        public HomeController(IAccessService service)
        {
            _accessService = service;
        }

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            IndexVM model = new IndexVM();
            model.LatestBlogs = _accessService.GetLatestBlogs(3);

            return View(model);
        }
    }
}