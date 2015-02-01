using Floreview.DataAccess.Interfaces;
using Floreview.DataAccess.Repositories;
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
        private IUnitOfWork _uow = null;

        private ILocation _locationRepository = null;

        private ICompany _companyRepository = null;

        private IBlog _blogRepository = null;

        private IBlogCategory _blogTypeRepository = null;

        private IGeneric<Florist> _floristRepository = null;

        public AccessService()
        {

        }

        public AccessService(IUnitOfWork uow, ILocation locationRepo, ICompany companyRepo, IBlog blogRepo, IBlogCategory blogTypeRepo, GenericRepository<Florist> floristRepo)
        {
            _uow = uow;
            _locationRepository = locationRepo;
            _companyRepository = companyRepo;
            _blogRepository = blogRepo;
            _blogTypeRepository = blogTypeRepo;
            _floristRepository = floristRepo;
        }


        public List<Blog> GetLatestBlogs(int amount)
        {
            return _blogRepository.GetLatestBlogs(amount).ToList<Blog>();
        }

        public List<Company> GetAllCompanies()
        {
            return _companyRepository.All().ToList<Company>();
        }

        public Company GetCompanyByID(int ID)
        {
            return _companyRepository.GetCompanyByID(ID);
        }

        public Company InsertCompany(Company company)
        {
            Company result = _companyRepository.Insert(company);
            _uow.SaveChanges();

            return result;
        }

        public void UpdateCompany(Company company)
        {
            _companyRepository.Update(company);
            _uow.SaveChanges();
        }

        public void DeleteCompany(Company company)
        {
            _companyRepository.Delete(company);
            _uow.SaveChanges();
        }

        public List<Company> GetCompaniesByCompanyName(String company)
        {
            return _companyRepository.GetCompaniesByCompanyName(company).ToList<Company>();
        }

        public List<Company> GetCompaniesByCityName(String city)
        {
            return _companyRepository.GetCompaniesByCityName(city).ToList<Company>();
        }

        public List<Company> GetCompaniesByCompanyAndCity(String company, String city)
        {
            return _companyRepository.GetCompaniesByCompanyAndCity(company, city).ToList<Company>();
        }

        public List<Company> GetCompaniesByFilterAndSortMethod(String filter, int sort)
        {
            List<Company> lstCompanies = null;

            if (String.IsNullOrEmpty(filter))
            {
                lstCompanies = _companyRepository.All().ToList<Company>();
            }
            else
            {
                lstCompanies = _companyRepository.GetCompaniesByCityName(filter).ToList<Company>();
            }

            switch (sort)
            {
                case 1:
                    return lstCompanies.OrderBy(o => o.Name).ToList<Company>();
                case 2:
                    return lstCompanies.OrderBy(o => o.Location.City).ThenBy(o => o.Name).ToList<Company>();
                case 3:
                    return lstCompanies.OrderBy(o => o.Location.Province.Name).ThenBy(o => o.Name).ToList<Company>();
                default:
                    return lstCompanies.OrderByDescending(o => o.ID).ThenBy(o => o.Name).ToList<Company>();
            }
        }

        public void DeleteFlorist(Florist florist)
        {
            _floristRepository.Delete(florist);
            _uow.SaveChanges();
        }

        public List<Location> GetLocationsByQuery(String query)
        {
            return _locationRepository.GetLocationsByQuery(query).ToList<Location>();
        }

        public Location GetLocationByCityName(String city)
        {
            return _locationRepository.GetLocationByCityName(city);
        }


        public List<Company> GetCompaniesMainCity(string main, string city, int region)
        {
            return _companyRepository.GetCompaniesMainCity(main, city, region).ToList<Company>();
        }

        public List<Blog> GetAllBlogs()
        {
            return _blogRepository.All().ToList<Blog>();
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
    }
}