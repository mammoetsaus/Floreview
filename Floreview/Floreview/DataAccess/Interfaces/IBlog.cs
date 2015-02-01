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
        IEnumerable<Blog> GetLatestBlogs(int amount);


        IEnumerable<Blog> GetNextRangeOfBlogs(int blockNumber, int blockSize);

        IEnumerable<Blog> GetNextRangeOfBlogsByAuthor(int blockNumber, int blockSize, String accessCode);

        IEnumerable<Blog> GetNextRangeOfBlogsByCategory(int blockNumber, int blockSize, int typeID);

        IEnumerable<Blog> GetNextRangeOfBlogsByQuery(int blockNumber, int blockSize, String query);

        IEnumerable<Blog> GetNextRangeOfBlogsByDate(int blockNumber, int blockSize, String date);

        IEnumerable<DateTime> GetDatesForSideBlog();
    }
}
