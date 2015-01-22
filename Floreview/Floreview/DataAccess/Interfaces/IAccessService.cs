using Floreview.Models;
using Floreview.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floreview.DataAccess.Interfaces
{
    public interface IAccessService
    {
        List<Location> GetLocationsAutocomplete(String query);

        List<Company> GetCompaniesSearchName(String name);

        List<Company> GetCompaniesSearchCity(String city);

        List<Company> GetCompaniesSearchBoth(String name, String city);

        Location GetLocationByCityName(String query);

        List<Company> GetCompaniesMainCity(String main, String city, int region);

        Company GetCompanyByID(int id);

        List<Company> GetAllCompanies();

        List<Company> GetCompaniesManage(String filter, int sort);

        int InsertCompany(Company c);

        void UpdateCompany(Company c);

        void DeleteCompany(Company c);

        List<Blog> GetAllBlogs();

        List<Blog> GetMostRecentBlogs();

        List<Blog> GetNextRangeOfBlogs(BlogVM model, int blockSize);

        List<BlogCategory> GetBlogCategoriesForSideBlog();

        Dictionary<DateTime, int> GetDatesForSideBlog();
    }
}
