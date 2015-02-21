using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floreview.DataAccess.Interfaces
{
    public interface IBlog : IGeneric<Blog>
    {
        IEnumerable<Blog> GetAllAccessibleBlogs(int skip);

        IEnumerable<Blog> GetLatestBlogs(int amount);

        IEnumerable<Blog> GetBlogsByName(String name);

        IEnumerable<BlogCategoryFrequency> GetAllBlogFrequencies();

        IEnumerable<BlogPublishdateFrequency> GetAllBlogPublishdateFrequencies();

        IEnumerable<BlogAuthorFrequency> GetAllBlogAuthorFrequencies();

        IEnumerable<Blog> GetAllBlogsByQuery(String query, int skip);

        IEnumerable<Blog> GetAllBlogsByCategoryID(int ID, int skip);

        IEnumerable<Blog> GetAllBlogsByArchive(int year, int month, int skip);

        IEnumerable<Blog> GetAllBlogsByAuthor(String author, int skip);

        IEnumerable<Blog> GetRelatedBlogs(int amount, int blogDetailID, int blogCategoryID);
    }
}
