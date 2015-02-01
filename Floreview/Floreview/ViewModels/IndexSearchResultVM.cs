using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.ViewModels
{
    public class IndexSearchResultVM
    {
        public List<Company> Companies { get; set; }

        public List<Company> NearbyCompanies { get; set; }
    }
}