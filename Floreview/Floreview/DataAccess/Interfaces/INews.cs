using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Interfaces
{
    public interface INews : IGeneric<News>
    {
        IEnumerable<News> GetRecentNews();
    }
}