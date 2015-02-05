using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Repositories
{
    public class BlogCategoryRepository : GenericRepository<BlogCategory>, IBlogCategory
    {
        public BlogCategoryRepository()
        {

        }

        public BlogCategoryRepository(FlowerContext context) : base(context)
        {

        }
    }
}