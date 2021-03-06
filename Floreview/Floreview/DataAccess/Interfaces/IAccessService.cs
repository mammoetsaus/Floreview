﻿using Floreview.Models;
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
        Blog GetBlogByID(int ID);

        Blog InsertBlog(Blog blog);

        void UpdateBlog(Blog blog);

        void DeleteBlog(Blog blog);

        List<Blog> GetLatestBlogs(int amount);

        List<Blog> GetBlogsByFilterAndSortMethod(String filter, int sort);

        List<Blog> GetBlogsWithFilters(int? page, String query, int? category, String archive, String author);

        List<Blog> GetRelatedBlogs(int amount, int blogDetailID, int blogCategoryID);

        List<BlogAuthorFrequency> GetAllBlogAuthorFrequencies();

        List<BlogCategory> GetAllBlogCategories();

        BlogCategory GetBlogCategoryByID(int ID);

        BlogCategory InsertBlogCategory(BlogCategory blogCategory);

        void DeleteBlogCategory(BlogCategory blogCategory);

        Boolean IsNewBlogcategory(String categoryName);

        List<BlogCategoryFrequency> GetAllBlogFrequencies();

        List<BlogElement> GetAllBlogElements();

        List<BlogPublishdateFrequency> GetAllBlogPublishdateFrequencies();

        List<Company> GetAllCompanies();

        Company GetCompanyByID(int ID);

        Company InsertCompany(Company company);

        void UpdateCompany(Company company);

        void DeleteCompany(Company c);
        
        List<Company> GetCompaniesByCompanyName(String company);

        List<Company> GetCompaniesByCityName(String city);

        List<Company> GetCompaniesByCompanyAndCity(String company, String city);

        List<Company> GetCompaniesByFilterAndSortMethod(String filter, int sort);

        void DeleteFlorist(Florist florist);

        List<Location> GetLocationsByQuery(String query);

        Location GetLocationByCityName(String city);
    }
}
