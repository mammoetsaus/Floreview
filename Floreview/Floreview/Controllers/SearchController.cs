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
        #region Fields & Props
        private IAccessService _accessService = null;
        #endregion

        #region Constructor
        public SearchController(IAccessService service)
        {
            _accessService = service;
        }

        public SearchController()
        {

        }
        #endregion

        #region Actions
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
                        result.Companies = _accessService.GetCompaniesSearchName(model.Name);
                        ViewBag.Name = model.Name;
                        ViewBag.City = "-";
                    }
                    else if (String.IsNullOrEmpty(model.Name) && !String.IsNullOrEmpty(model.City))
                    {
                        result.Companies = _accessService.GetCompaniesSearchCity(model.City);
                        ViewBag.Name = "-";
                        ViewBag.City = model.City;
                    }
                    else if (!String.IsNullOrEmpty(model.Name) && !String.IsNullOrEmpty(model.City))
                    {
                        result.Companies = _accessService.GetCompaniesSearchBoth(model.Name, model.City);
                        ViewBag.Name = model.Name;
                        ViewBag.City = model.City;
                    }
                    else
                    {
                        throw new ArgumentException();
                    }

                    if (!String.IsNullOrEmpty(model.City))
                    {
                        result.NearbyCompanies = new List<Company>();
                        Location searchedLocation = _accessService.GetLocationByCityName(model.City);
                        List<CompanyLocation> companyLocations = _accessService.GetAllCompanyLocationsByLocationID(searchedLocation.ID);

                        if (companyLocations != null && companyLocations.Count > 0)
                        {
                            companyLocations.ForEach(i => result.NearbyCompanies.Add(i.Company));
                        }
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
        #endregion
    }
}