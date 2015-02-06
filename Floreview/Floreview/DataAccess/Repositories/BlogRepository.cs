using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using Floreview.DataAccess.Services;
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlog
    {
        public BlogRepository()
        {

        }

        public BlogRepository(FlowerContext context)
            : base(context)
        {

        }

        public IEnumerable<Blog> GetLatestBlogs(int amount)
        {
            return (from b in context.Blog.Where(i => i.PublishDate <= DateTime.UtcNow) select b).OrderByDescending(i => i.PublishDate).Take(amount);
        }

        public IEnumerable<Blog> GetBlogsByName(String name)
        {
            return (from b in context.Blog.Where(i => i.TitleNL.Contains(name) || i.TitleEN.Contains(name)
                || i.TitleFR.Contains(name) || i.TitleDE.Contains(name))
                    select b);
        }


        public IEnumerable<Blog> GetAllAccessibleBlogs(int skip)
        {
            return ((from b in context.Blog.Where(i => i.PublishDate <= DateTime.UtcNow).OrderByDescending(j => j.PublishDate).Skip(skip).Take(AccessService.BLOCKSIZE) select b));
        }

        public IEnumerable<BlogCategoryFrequency> GetAllBlogFrequencies()
        {
            return context.Database.SqlQuery<BlogCategoryFrequency>("SELECT BlogCategories.ID as 'CategoryID', BlogCategories.Name as 'Category', COUNT(1) as 'Frequency' FROM Blogs LEFT OUTER JOIN BlogCategories ON Blogs.Category_ID = BlogCategories.ID WHERE PublishDate < GETUTCDATE() GROUP BY BlogCategories.ID, BlogCategories.Name");
        }


        public IEnumerable<BlogPublishdateFrequency> GetAllBlogPublishdateFrequencies()
        {
            return context.Database.SqlQuery<BlogPublishdateFrequency>("SELECT YEAR(PublishDate) as 'Year', MONTH(PublishDate) as 'Month', COUNT(1) as 'Frequency' From Blogs WHERE PublishDate < GETUTCDATE() Group BY YEAR(PublishDate), MONTH(PublishDate)");
        }

        public IEnumerable<BlogAuthorFrequency> GetAllBlogAuthorFrequencies()
        {
            return context.Database.SqlQuery<BlogAuthorFrequency>("SELECT AspNetUsers.AccessCode as 'AccessCode', AspNetUsers.FirstName as 'FirstName', AspNetUsers.LastName as 'LastName', COUNT(*) as 'Frequency' FROM Blogs LEFT OUTER JOIN AspNetUsers ON Blogs.Author_Id = AspNetUsers.ID WHERE PublishDate < GETUTCDATE() GROUP BY AspNetUsers.AccessCode, AspNetUsers.FirstName, AspNetUsers.LastName");
        }


        public IEnumerable<Blog> GetAllBlogsByCategoryID(int ID, int skip)
        {
            return (from b in context.Blog.Where(i => (i.PublishDate <= DateTime.UtcNow) && i.Category.ID.Equals(ID)).OrderByDescending(j => j.PublishDate).Skip(skip).Take(AccessService.BLOCKSIZE) select b);
        }


        public IEnumerable<Blog> GetAllBlogsByArchive(int year, int month, int skip)
        {
            return (from b in context.Blog.Where(i => (i.PublishDate <= DateTime.UtcNow) && i.PublishDate.Year.Equals(year) && i.PublishDate.Month.Equals(month)).OrderByDescending(j => j.PublishDate).Skip(skip).Take(AccessService.BLOCKSIZE) select b);
        }


        public IEnumerable<Blog> GetAllBlogsByAuthor(String author, int skip)
        {
            return ((from b in context.Blog.Where(i => (i.PublishDate <= DateTime.UtcNow) && i.Author.AccessCode == author).OrderByDescending(j => j.PublishDate).Skip(skip).Take(AccessService.BLOCKSIZE) select b));
        }
    }
}