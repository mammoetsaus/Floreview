using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using Floreview.Resources;
using Floreview.Utils;
using Floreview.ViewModels.CMS;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.Controllers.CMS
{
    [Authorize]
    public class ManageController : Controller
    {
        private IAccessService _accessService = null;

        private IUserManagementService _userManagementService = null;

        public ManageController(IAccessService accessService, IUserManagementService userManagementService)
        {
            _accessService = accessService;
            _userManagementService = userManagementService;
        }

        public ManageController()
        {

        }

        public ActionResult Store()
        {
            return View();
        }

        public ActionResult AddStore()
        {
            StoreVM model = new StoreVM();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStore(StoreVM model)
        {
            if (ModelState.IsValid)
            {
                model.Company.Coordinates = GeocodingEngine.GetLatLongFromAddress(String.Format("{0}, {1}", model.Company.Location.City, model.Company.Address));
                model.Company.Location = _accessService.GetLocationByCityName(model.Company.Location.City);
                model.Company.Avatar = "http://floreview.blob.core.windows.net/profiles/profile_store_default.jpg";
                model.Company.Florist.ImagePath = "http://floreview.blob.core.windows.net/profiles/profile_florist_default.jpg";
                Company insertedCompany = _accessService.InsertCompany(model.Company);

                if (model.CompanyAvatar != null)
                {
                    insertedCompany.Avatar = BlobStorageEngine.UploadCompanyAvatar(model.CompanyAvatar, insertedCompany);
                }

                if (model.FloristAvatar != null)
                {
                    insertedCompany.Florist.ImagePath = BlobStorageEngine.UploadFloristAvatar(model.FloristAvatar, insertedCompany);
                }

                if (model.CompanyImages[0] != null)
                {
                    insertedCompany.ImageList = BlobStorageEngine.UploadCompanyImages(model.CompanyImages, insertedCompany);
                }

                _accessService.UpdateCompany(insertedCompany);

                return RedirectToAction("Store", "Manage");
            }

            return View(model);
        }

        public ActionResult EditStore(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue && id > 0)
                {
                    Company company = _accessService.GetCompanyByID(id.Value);

                    if (company != null)
                    {
                        StoreVM model = new StoreVM();
                        model.Company = company;

                        return View(model);
                    }
                }
            }

            return RedirectToAction("Store", "Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStore(StoreVM model)
        {
            if (ModelState.IsValid)
            {
                Company company = _accessService.GetCompanyByID(model.Company.ID);

                company.Coordinates = GeocodingEngine.GetLatLongFromAddress(String.Format("{0}, {1}", model.Company.Location.City, model.Company.Address));
                company.Location = _accessService.GetLocationByCityName(model.Company.Location.City);
                company.Name = model.Company.Name;
                company.Facebook = model.Company.Facebook;
                company.Website = model.Company.Website;
                company.Address = model.Company.Address;
                company.Email = model.Company.Email;

                company.Florist.FirstName = model.Company.Florist.FirstName;
                company.Florist.LastName = model.Company.Florist.LastName;
                company.Florist.Phone = model.Company.Florist.Phone;
                company.Florist.Cellphone = model.Company.Florist.Cellphone;

                company.DescriptionShortNL = model.Company.DescriptionShortNL;
                company.DescriptionLongNL = model.Company.DescriptionLongNL;
                company.DescriptionShortEN = model.Company.DescriptionShortEN;
                company.DescriptionLongEN = model.Company.DescriptionLongEN;
                company.DescriptionShortFR = model.Company.DescriptionShortFR;
                company.DescriptionLongFR = model.Company.DescriptionLongFR;
                company.DescriptionShortDE = model.Company.DescriptionShortDE;
                company.DescriptionLongDE = model.Company.DescriptionLongDE;


                if (model.CompanyAvatar != null)
                {
                    company.Avatar = BlobStorageEngine.UploadCompanyAvatar(model.CompanyAvatar, company);
                }

                if (model.FloristAvatar != null)
                {
                    company.Florist.ImagePath = BlobStorageEngine.UploadFloristAvatar(model.FloristAvatar, company);
                }

                if (model.CompanyImages[0] != null)
                {
                    company.ImageList = BlobStorageEngine.UpdateCompanyImages(model.CompanyImages, company);
                }

                _accessService.UpdateCompany(company);

                return RedirectToAction("Store", "Manage");
            }

            return View(model);
        }

        public ActionResult DeleteStore(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue && id > 0)
                {
                    Company company = _accessService.GetCompanyByID(id.Value);

                    if (company != null)
                    {
                        BlobStorageEngine.DeleteCompanyPhotos(company);

                        _accessService.DeleteFlorist(company.Florist);
                        _accessService.DeleteCompany(company);
                    }
                }
            }

            return RedirectToAction("Store", "Manage");
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult AddBlog()
        {
            var blogCategories = _accessService.GetAllBlogCategories();

            BlogVM model = new BlogVM();
            model.BlogElements = _accessService.GetAllBlogElements();
            model.BlogCategories = new SelectList(blogCategories, "ID", "Name");
            model.SelectedBlogCategoryID = 0;
            model.Blog = new Blog() { PublishDate = DateTime.Today };

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddBlog(BlogVM model)
        {
            if (ModelState.IsValid)
            {
                // parse publish date to UTC
                model.Blog.PublishDate = model.Blog.PublishDate.AddHours(-1);

                model.Blog.Avatar = "http://floreview.blob.core.windows.net/blog/blog_avatar_default.jpg";
                model.Blog.Category = _accessService.GetBlogCategoryByID(model.SelectedBlogCategoryID);
                model.Blog.Author = _userManagementService.GetUser(User.Identity.Name);
                model.Blog.ContentNL = LanguageUtility.GetLanguageBlogContent(model.Blog, 0);
                model.Blog.ContentEN = LanguageUtility.GetLanguageBlogContent(model.Blog, 1);
                model.Blog.ContentFR = LanguageUtility.GetLanguageBlogContent(model.Blog, 2);
                model.Blog.ContentDE = LanguageUtility.GetLanguageBlogContent(model.Blog, 3);
                Blog insertedBlog = _accessService.InsertBlog(model.Blog);

                if (model.BlogAvatar != null)
                {
                    insertedBlog.Avatar = BlobStorageEngine.UploadBlogAvatar(model.BlogAvatar, insertedBlog);
                }

                _accessService.UpdateBlog(insertedBlog);

                return RedirectToAction("Blog", "Manage");
            }

            var blogCategories = _accessService.GetAllBlogCategories();
            model.BlogElements = _accessService.GetAllBlogElements();
            model.BlogCategories = new SelectList(blogCategories, "ID", "Name");

            return View(model);
        }

        public ActionResult DeleteBlog(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue && id > 0)
                {
                    Blog blog = _accessService.GetBlogByID(id.Value);

                    if (blog != null)
                    {
                        BlobStorageEngine.DeleteBlogAvatar(blog);

                        _accessService.DeleteBlog(blog);
                    }
                }
            }

            return RedirectToAction("Blog", "Manage");
        }

        public ActionResult BlogCategory()
        {
            BlogCategoryVM model = new BlogCategoryVM();
            model.BlogCategories = _accessService.GetAllBlogCategories();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddBlogCategory(BlogCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                if (_accessService.IsNewBlogcategory(model.BlogCategory.Name))
                {
                    _accessService.InsertBlogCategory(model.BlogCategory);
                }
            }

            return RedirectToAction("BlogCategory", "Manage");
        }

        public ActionResult DeleteBlogCategory(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue && id > 0)
                {
                    BlogCategory blogCategory = _accessService.GetBlogCategoryByID(id.Value);

                    if (blogCategory != null)
                    {
                        try
                        {
                            _accessService.DeleteBlogCategory(blogCategory);
                        }
                        catch (DbUpdateException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            return RedirectToAction("BlogCategory", "Manage");
        }
    }
}