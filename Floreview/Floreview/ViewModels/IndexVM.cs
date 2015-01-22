using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.ViewModels
{
    public class IndexVM
    {
        #region Fields & Props
        public List<Blog> Blogs { get; set; }

        public String LatitudeRAW { get; set; }

        public String LongitudeRAW { get; set; }
        #endregion
    }
}