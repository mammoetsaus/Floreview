using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlog
    {
        #region Constructor
        public BlogRepository()
        {

        }

        public BlogRepository(FlowerContext context)
            : base(context)
        {

        }
        #endregion

        #region IBlog Interface
        public IEnumerable<Blog> GetMostRecentBlogs()
        {
            // argument 1: select only available blogs (not future blogs)
            // argument 2: order by timestamp
            var result = (from b in context.Blog.Where(i => i.Timestamp <= DateTime.Now) select b).OrderByDescending(i => i.Timestamp).Take(3);
            return result.ToList<Blog>();
        }

        public IEnumerable<Blog> GetNextRangeOfBlogs(int blockNumber, int blockSize)
        {
            var result = (from b in context.Blog.Where(i => i.Timestamp <= DateTime.Now) select b).OrderByDescending(i => i.Timestamp).ThenByDescending(i => i.ID).Skip(blockNumber).Take(blockSize);
            return result;
        }
        
        public IEnumerable<Blog> GetNextRangeOfBlogsByAuthor(int blockNumber, int blockSize, string accessCode)
        {
            var result = (from b in context.Blog.Where(i => i.Timestamp <= DateTime.Now && i.Author.AccessCode.Equals(accessCode)) select b).OrderByDescending(i => i.Timestamp).ThenByDescending(i => i.ID).Skip(blockNumber).Take(blockSize);
            return result;
        }

        public IEnumerable<Blog> GetNextRangeOfBlogsByCategory(int blockNumber, int blockSize, int typeID)
        {
            var result = (from b in context.Blog.Where(i => i.Timestamp <= DateTime.Now && i.Category.ID.Equals(typeID)) select b).OrderByDescending(i => i.Timestamp).ThenByDescending(i => i.ID).Skip(blockNumber).Take(blockSize);
            return result;
        }

        public IEnumerable<Blog> GetNextRangeOfBlogsByQuery(int blockNumber, int blockSize, string query)
        {
            var result = (from b in context.Blog.Where(i => i.Timestamp <= DateTime.Now && i.Title.Contains(query)) select b).OrderByDescending(i => i.Timestamp).ThenByDescending(i => i.ID).Skip(blockNumber).Take(blockSize);
            return result;
        }

        public IEnumerable<Blog> GetNextRangeOfBlogsByDate(int blockNumber, int blockSize, string date)
        {
            int month, year;
            Int32.TryParse(date.Split('-')[0], out month);
            Int32.TryParse(date.Split('-')[1], out year);

            if (month != 0 && year != 0)
            {
                var result = (from b in context.Blog.Where(i => i.Timestamp <= DateTime.Now && i.Timestamp.Month.Equals(month) && i.Timestamp.Year.Equals(year)) select b).OrderByDescending(i => i.Timestamp).ThenByDescending(i => i.ID).Skip(blockNumber).Take(blockSize);
                return result;
            }

            return null;
        }

        public IEnumerable<DateTime> GetDatesForSideBlog()
        {
            var result = (from d in context.Blog.Where(i => i.Timestamp <= DateTime.Now) orderby d.Timestamp descending select d.Timestamp);
            return result.ToList<DateTime>();
        }
        #endregion
    }
}