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

        public ActionResult Index(String name, String city)
        {
            if (ModelState.IsValid)
            {
                IndexSearchResultVM result = new IndexSearchResultVM();

                if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(city))
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (!String.IsNullOrEmpty(name) && String.IsNullOrEmpty(city))
                {
                    result.Companies = _accessService.GetCompaniesByCompanyName(name);
                    ViewBag.Name = name;
                    ViewBag.City = "-";

                    ViewBag.NumberOfCompanies = result.Companies.Count;
                    return View(result);
                }
                else if (String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(city))
                {
                    result.Companies = _accessService.GetCompaniesByCityName(city);
                    ViewBag.Name = "-";
                    ViewBag.City = city;

                    ViewBag.NumberOfCompanies = result.Companies.Count;
                    return View(result);
                }
                else if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(city))
                {
                    result.Companies = _accessService.GetCompaniesByCompanyAndCity(name, city);
                    ViewBag.Name = name;
                    ViewBag.City = city;

                    ViewBag.NumberOfCompanies = result.Companies.Count;
                    return View(result);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}