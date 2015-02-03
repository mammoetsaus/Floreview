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
    public class StoreController : Controller
    {
        public IAccessService _accessService = null;

        public StoreController(IAccessService service)
        {
            _accessService = service;
        }

        public StoreController()
        {

        }

        public ActionResult Detail(int? profile)
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