using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.ViewModels.CMS
{
    public class StoreVM
    {
        #region Fields & Props
        public Company Company { get; set; }

        public String LatitudeRAW { get; set; }

        public String LongitudeRAW { get; set; }

        public HttpPostedFileBase CompanyAvatar { get; set; }

        public HttpPostedFileBase FloristAvatar { get; set; }

        public HttpPostedFileBase[] CompanyImages { get; set; }
        #endregion
    }
}