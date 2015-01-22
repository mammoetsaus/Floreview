using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using Floreview.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Services
{
    public class AccessService : IAccessService
    {
        #region Fields & Props
        private IUnitOfWork _uow = null;

        private ILocation _locationRepository = null;

        private ICompany _companyRepository = null;

        private IBlog _blogRepository = null;

        private IBlogCategory _blogTypeRepository = null;
        #endregion

        #region Constructor
        public AccessService()
        {

        }

        public AccessService(IUnitOfWork uow, ILocation locationRepo, ICompany companyRepo, IBlog blogRepo, IBlogCategory blogTypeRepo)
        {
            _uow = uow;
            _locationRepository = locationRepo;
            _companyRepository = companyRepo;
            _blogRepository = blogRepo;
            _blogTypeRepository = blogTypeRepo;
        }
        #endregion

        #region IAccess Interface
        public List<Location> GetLocationsAutocomplete(String query)
        {
            return _locationRepository.GetLocationsAutocomplete(query).ToList<Location>();
        }

        public List<Company> GetCompaniesSearchName(string name)
        {
            return _companyRepository.GetCompaniesSearchName(name).ToList<Company>();
        }

        public List<Company> GetCompaniesSearchCity(string city)
        {
            return _companyRepository.GetCompaniesSearchCity(city).ToList<Company>();
        }

        public List<Company> GetCompaniesSearchBoth(string name, string city)
        {
            return _companyRepository.GetCompaniesSearchBoth(name, city).ToList<Company>();
        }

        public Location GetLocationByCityName(string query)
        {
            return _locationRepository.GetLocationByCityName(query);
        }

        public List<Company> GetCompaniesMainCity(string main, string city, int region)
        {
            return _companyRepository.GetCompaniesMainCity(main, city, region).ToList<Company>();
        }

        public Company GetCompanyByID(int id)
        {
            return _companyRepository.GetCompanyByID(id);
        }

        public List<Company> GetAllCompanies()
        {
            return _companyRepository.All().ToList<Company>();
        }

        public List<Company> GetCompaniesManage(string filter, int sort)
        {
            List<Company> lstCompanies = null;

            if (String.IsNullOrEmpty(filter))
            {
                lstCompanies = _companyRepository.All().ToList<Company>();
            }
            else
            {
                lstCompanies = _companyRepository.GetCompaniesSearchCity(filter).ToList<Company>();
            }

            switch (sort)
            {
                case 1:
                    return lstCompanies.OrderBy(o => o.Name).ToList<Company>();
                case 2:
                    return lstCompanies.OrderBy(o => o.Location.City).ToList<Company>();
                case 3:
                    return lstCompanies.OrderBy(o => o.Location.Province.Name).ToList<Company>();
                default:
                    return lstCompanies.OrderByDescending(o => o.ID).ToList<Company>();
            }
        }

        public int InsertCompany(Company c)
        {
            Company company = _companyRepository.Insert(c);
            _uow.SaveChanges();

            return company.ID;
        }

        public void UpdateCompany(Company c)
        {
            _companyRepository.Update(c);
            _uow.SaveChanges();
        }

        public void DeleteCompany(Company c)
        {
            _companyRepository.Delete(c);
            _uow.SaveChanges();
        }

        public List<Blog> GetAllBlogs()
        {
            return _blogRepository.All().ToList<Blog>();
        }

        public List<Blog> GetMostRecentBlogs()
        {
            return _blogRepository.GetMostRecentBlogs().ToList<Blog>();
        }

        public List<Blog> GetNextRangeOfBlogs(BlogVM model, int blockSize)
        {
            if (String.IsNullOrEmpty(model.Author) && model.CategoryID == 0 && String.IsNullOrEmpty(model.Query) && String.IsNullOrEmpty(model.Date))
            {
                // no querystring values
                return _blogRepository.GetNextRangeOfBlogs(model.BlockNumber, blockSize).ToList<Blog>();
            }
            else if (!String.IsNullOrEmpty(model.Author) && model.CategoryID == 0 && String.IsNullOrEmpty(model.Query) && String.IsNullOrEmpty(model.Date))
            {
                // only author value
                return _blogRepository.GetNextRangeOfBlogsByAuthor(model.BlockNumber, blockSize, model.Author).ToList<Blog>();
            }
            else if (String.IsNullOrEmpty(model.Author) && model.CategoryID != 0 && String.IsNullOrEmpty(model.Query) && String.IsNullOrEmpty(model.Date)) 
            {
                // only category value
                return _blogRepository.GetNextRangeOfBlogsByCategory(model.BlockNumber, blockSize, model.CategoryID).ToList<Blog>();
            }
            else if (String.IsNullOrEmpty(model.Author) && model.CategoryID == 0 && !String.IsNullOrEmpty(model.Query) && String.IsNullOrEmpty(model.Date))
            {
                // only search value
                return _blogRepository.GetNextRangeOfBlogsByQuery(model.BlockNumber, blockSize, model.Query).ToList<Blog>();
            }
            else if (String.IsNullOrEmpty(model.Author) && model.CategoryID == 0 && String.IsNullOrEmpty(model.Query) && !String.IsNullOrEmpty(model.Date))
            {
                // only date value
                return _blogRepository.GetNextRangeOfBlogsByDate(model.BlockNumber, blockSize, model.Date).ToList<Blog>();
            }

            return null;
        }

        public List<BlogCategory> GetBlogCategoriesForSideBlog()
        {
            return _blogTypeRepository.GetBlogTypesForSideBlog().ToList<BlogCategory>();
        }

        public Dictionary<DateTime, int> GetDatesForSideBlog()
        {
            Dictionary<DateTime, int> dictFinalDates = new Dictionary<DateTime, int>();
            List<DateTime> lstAllDates = _blogRepository.GetDatesForSideBlog().ToList<DateTime>();

            foreach (DateTime date in lstAllDates)
            {
                // create new empty datetime object
                DateTime dateTest = new DateTime(date.Year, date.Month, 1, 0, 0, 0);

                if (!dictFinalDates.ContainsKey(dateTest))
                {
                    dictFinalDates.Add(dateTest, 1);
                }
                else
                {
                    dictFinalDates[dateTest]++;
                }
            }

            return dictFinalDates;
        }
        #endregion
    }
}