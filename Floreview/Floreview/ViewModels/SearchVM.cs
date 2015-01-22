using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Floreview.ViewModels
{
    public class SearchVM
    {
        #region Fields & Props
        public String SearchName { get; set; }

        public String SearchCity { get; set; }

        public String LatitudeRAW { get; set; }

        public String LongitudeRAW { get; set; }
        #endregion
    }
}