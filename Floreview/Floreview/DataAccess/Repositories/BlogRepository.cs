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
        public BlogRepository()
        {

        }

        public BlogRepository(FlowerContext context) : base(context)
        {

        }

        public IEnumerable<Blog> GetLatestBlogs(int amount)
        {
            var result = (from b in context.Blog.Where(i => i.PublishDate <= DateTime.Now) select b).OrderByDescending(i => i.PublishDate).Take(amount);
            return result.ToList<Blog>();
        }


        public IEnumerable<Blog> GetNextRangeOfBlogs(int blockNumber, int blockSize)
        {
            var result = (from b in context.Blog.Where(i => i.PublishDate <= DateTime.Now) select b).OrderByDescending(i => i.PublishDate).ThenByDescending(i => i.ID).Skip(blockNumber).Take(blockSize);
            return result;
        }
        
        public IEnumerable<Blog> GetNextRangeOfBlogsByAuthor(int blockNumber, int blockSize, string accessCode)
        {
            var result = (from b in context.Blog.Where(i => i.PublishDate <= DateTime.Now && i.Author.AccessCode.Equals(accessCode)) select b).OrderByDescending(i => i.PublishDate).ThenByDescending(i => i.ID).Skip(blockNumber).Take(blockSize);
            return result;
        }

        public IEnumerable<Blog> GetNextRangeOfBlogsByCategory(int blockNumber, int blockSize, int typeID)
        {
            var result = (from b in context.Blog.Where(i => i.PublishDate <= DateTime.Now && i.Category.ID.Equals(typeID)) select b).OrderByDescending(i => i.PublishDate).ThenByDescending(i => i.ID).Skip(blockNumber).Take(blockSize);
            return result;
        }

        public IEnumerable<Blog> GetNextRangeOfBlogsByQuery(int blockNumber, int blockSize, string query)
        {
            var result = (from b in context.Blog.Where(i => i.PublishDate <= DateTime.Now && i.TitleNL.Contains(query)) select b).OrderByDescending(i => i.PublishDate).ThenByDescending(i => i.ID).Skip(blockNumber).Take(blockSize);
            return result;
        }

        public IEnumerable<Blog> GetNextRangeOfBlogsByDate(int blockNumber, int blockSize, string date)
        {
            int month, year;
            Int32.TryParse(date.Split('-')[0], out month);
            Int32.TryParse(date.Split('-')[1], out year);

            if (month != 0 && year != 0)
            {
                var result = (from b in context.Blog.Where(i => i.PublishDate <= DateTime.Now && i.PublishDate.Month.Equals(month) && i.PublishDate.Year.Equals(year)) select b).OrderByDescending(i => i.PublishDate).ThenByDescending(i => i.ID).Skip(blockNumber).Take(blockSize);
                return result;
            }

            return null;
        }

        public IEnumerable<DateTime> GetDatesForSideBlog()
        {
            var result = (from d in context.Blog.Where(i => i.PublishDate <= DateTime.Now) orderby d.PublishDate descending select d.PublishDate);
            return result.ToList<DateTime>();
        }
    }
}