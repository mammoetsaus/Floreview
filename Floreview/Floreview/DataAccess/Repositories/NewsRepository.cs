using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Repositories
{
    public class NewsRepository : GenericRepository<News>, INews
    {
        #region Constructor
        public NewsRepository(FlowerContext context) : base (context)
        {

        }
        #endregion

        #region INews Interface
        public IEnumerable<News> GetRecentNews()
        {
            var result = (from n in context.News orderby n.ID descending select n).Take(3);
            return result.ToList<News>();
        }
        #endregion
    }
}