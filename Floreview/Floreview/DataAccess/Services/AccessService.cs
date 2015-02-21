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

        private IBlogCategory _blogCategoryRepository = null;

        private IGeneric<Florist> _floristRepository = null;

        private IGeneric<BlogElement> _blogElementRepository = null;

        public const int BLOCKSIZE = 20;

        public AccessService()
        {

        }

        public AccessService(IUnitOfWork uow, ILocation locationRepo, ICompany companyRepo, IBlog blogRepo, IBlogCategory blogCategoryRepo, GenericRepository<Florist> floristRepo, GenericRepository<BlogElement> blogElementRepo)
        {
            _uow = uow;
            _locationRepository = locationRepo;
            _companyRepository = companyRepo;
            _blogRepository = blogRepo;
            _blogCategoryRepository = blogCategoryRepo;
            _floristRepository = floristRepo;
            _blogElementRepository = blogElementRepo;
        }


        public Blog GetBlogByID(int ID)
        {
            return _blogRepository.GetByID(ID);
        }

        public Blog InsertBlog(Blog blog)
        {
            Blog result = _blogRepository.Insert(blog);
            _uow.SaveChanges();

            return result;
        }

        public void UpdateBlog(Blog blog)
        {
            _blogRepository.Update(blog);
            _uow.SaveChanges();
        }

        public void DeleteBlog(Blog blog)
        {
            _blogRepository.Delete(blog);
            _uow.SaveChanges();
        }

        public List<Blog> GetLatestBlogs(int amount)
        {
            return _blogRepository.GetLatestBlogs(amount).ToList<Blog>();
        }

        public List<Blog> GetBlogsByFilterAndSortMethod(String filter, int sort)
        {
            List<Blog> blogs = null;

            if (filter.Equals("%"))
            {
                blogs = _blogRepository.All().ToList<Blog>();
            }
            else
            {
                blogs = _blogRepository.GetBlogsByName(filter).ToList<Blog>();
            }

            switch (sort)
            {
                case 1:
                    return blogs.OrderByDescending(o => o.PublishDate).ToList<Blog>();
                default:
                    return blogs.OrderByDescending(o => o.ID).ToList<Blog>();
            }
        }

        public List<Blog> GetBlogsWithFilters(int? page, String query, int? category, String archive, String author)
        {
            List<Blog> blogs = new List<Blog>();

            int skip = 0;
            if (page.HasValue)
            {
                skip = (page.Value * BLOCKSIZE) - ((page.Value - 1) * BLOCKSIZE);
            }

            if (!String.IsNullOrEmpty(query))
            {
                blogs = _blogRepository.GetAllBlogsByQuery(query, skip).ToList<Blog>();
            }
            else if (category > 0)
            {
                blogs = _blogRepository.GetAllBlogsByCategoryID(category.Value, skip).ToList<Blog>();
            }
            else if (!String.IsNullOrEmpty(archive) && IsArchiveInCorrectFormat(archive))
            {
                blogs = _blogRepository.GetAllBlogsByArchive(GetArchiveYear(archive), GetArchiveMonth(archive), skip).ToList<Blog>();
            }
            else if (!String.IsNullOrEmpty(author))
            {
                blogs = _blogRepository.GetAllBlogsByAuthor(author, skip).ToList<Blog>();
            }

            if (blogs.Count == 0)
            {
                blogs = _blogRepository.GetAllAccessibleBlogs(skip).ToList<Blog>();
            }

            return blogs;
        }

        public List<Blog> GetRelatedBlogs(int amount, int blogDetailID, int blogCategoryID)
        {
            return _blogRepository.GetRelatedBlogs(amount, blogDetailID, blogCategoryID).ToList<Blog>();
        }

        public List<BlogAuthorFrequency> GetAllBlogAuthorFrequencies()
        {
            return _blogRepository.GetAllBlogAuthorFrequencies().ToList<BlogAuthorFrequency>();
        }

        public List<BlogCategory> GetAllBlogCategories()
        {
            return _blogCategoryRepository.All().OrderBy(i => i.Name).ToList<BlogCategory>();
        }

        public BlogCategory GetBlogCategoryByID(int ID)
        {
            return _blogCategoryRepository.GetByID(ID);
        }

        public void DeleteBlogCategory(BlogCategory blogCategory)
        {
            _blogCategoryRepository.Delete(blogCategory);
            _uow.SaveChanges();
        }

        public Boolean IsNewBlogcategory(String categoryName)
        {
            BlogCategory category = _blogCategoryRepository.GetBlogCategoryByName(categoryName);

            return (category == null) ? true : false; 
        }

        public BlogCategory InsertBlogCategory(BlogCategory blogCategory)
        {
            BlogCategory result = _blogCategoryRepository.Insert(blogCategory);
            _uow.SaveChanges();

            return result;
        }

        public List<BlogCategoryFrequency> GetAllBlogFrequencies()
        {
            return _blogRepository.GetAllBlogFrequencies().ToList<BlogCategoryFrequency>();
        }

        public List<BlogElement> GetAllBlogElements()
        {
            return _blogElementRepository.All().ToList<BlogElement>();
        }

        public List<BlogPublishdateFrequency> GetAllBlogPublishdateFrequencies()
        {
            return _blogRepository.GetAllBlogPublishdateFrequencies().ToList<BlogPublishdateFrequency>();
        }

        public List<Company> GetAllCompanies()
        {
            return _companyRepository.All().ToList<Company>();
        }

        public Company GetCompanyByID(int ID)
        {
            return _companyRepository.GetByID(ID);
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

            if (filter.Equals("%"))
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

        private Boolean IsArchiveInCorrectFormat(String archive)
        {
            try
            {
                String[] split = archive.Split('-');

                int year = Int32.Parse(split[0]);
                int month = Int32.Parse(split[1]);

                if (year > 2000 && month > 0 && month < 12)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int GetArchiveYear(String archive)
        {
            return Int32.Parse(archive.Split('-')[0]);
        }

        private int GetArchiveMonth(String archive)
        {
            return Int32.Parse(archive.Split('-')[1]);
        }
    }
}