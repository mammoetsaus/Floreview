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
        #region Fields & Props
        public IAccessService _accessService = null;
        #endregion

        #region Constructor
        public DetailController(IAccessService service)
        {
            _accessService = service;
        }

        public DetailController()
        {

        }
        #endregion

        #region Actions
        public ActionResult Index(int profile)
        {
            try
            {
                DetailVM model = new DetailVM();

                if (ModelState.IsValid)
                {

                    if (profile > 0)
                    {
                        // get company by id
                        Company company = _accessService.GetCompanyByID(profile);

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
        #endregion
    }
}