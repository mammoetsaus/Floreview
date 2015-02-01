using Floreview.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Floreview.ViewModels.CMS
{
    public class StoreVM
    {
        #region Fields & Props
        [Required(ErrorMessage = "Godverdomme wa is me da nu?")]
        public Company Company { get; set; }

        public HttpPostedFileBase CompanyAvatar { get; set; }

        public HttpPostedFileBase FloristAvatar { get; set; }

        public HttpPostedFileBase[] CompanyImages { get; set; }
        #endregion
    }
}