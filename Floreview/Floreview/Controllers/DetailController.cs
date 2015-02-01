using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using Floreview.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.Controllers
{
    public class DetailController : Controller
    {
        public IAccessService _accessService = null;

        public DetailController(IAccessService service)
        {
            _accessService = service;
        }

        public DetailController()
        {

        }

        public ActionResult Index(int? profile)
        {
            if (ModelState.IsValid)
            {
                if (profile.HasValue && profile > 0)
                {
                    Company company = _accessService.GetCompanyByID(profile.Value);

                    if (company != null)
                    {
                        DetailVM model = new DetailVM();
                        model.Company = company;

                        return View(model);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}