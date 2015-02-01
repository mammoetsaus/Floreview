using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using Floreview.Utils;
using Floreview.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.Controllers
{
    public class SearchController : Controller
    {
        private IAccessService _accessService = null;

        public SearchController(IAccessService service)
        {
            _accessService = service;
        }

        public SearchController()
        {

        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(IndexSearchVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IndexSearchResultVM result = new IndexSearchResultVM();

                    if (!String.IsNullOrEmpty(model.Name) && String.IsNullOrEmpty(model.City))
                    {
                        result.Companies = _accessService.GetCompaniesByCompanyName(model.Name);
                        ViewBag.Name = model.Name;
                        ViewBag.City = "-";
                    }
                    else if (String.IsNullOrEmpty(model.Name) && !String.IsNullOrEmpty(model.City))
                    {
                        result.Companies = _accessService.GetCompaniesByCityName(model.City);
                        ViewBag.Name = "-";
                        ViewBag.City = model.City;
                    }
                    else if (!String.IsNullOrEmpty(model.Name) && !String.IsNullOrEmpty(model.City))
                    {
                        result.Companies = _accessService.GetCompaniesByCompanyAndCity(model.Name, model.City);
                        ViewBag.Name = model.Name;
                        ViewBag.City = model.City;
                    }
                    else
                    {
                        throw new ArgumentException();
                    }

                    ViewBag.NumberOfCompanies = result.Companies.Count;

                    return View(result);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}