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
            try
            {
                DetailVM model = new DetailVM();

                if (ModelState.IsValid)
                {
                    if (profile.HasValue && profile > 0)
                    {
                        Company company = _accessService.GetCompanyByID(profile.Value);

                        if (company != null)
                        {
                            model.Company = company;
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }

                return View(model);
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}