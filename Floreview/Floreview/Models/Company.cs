using Floreview.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class Company
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "Manage_AddStore_StoreName_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceName = "Manage_AddStore_StoreName_Length_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        public String Name { get; set; }

        [Required(ErrorMessageResourceName = "Manage_AddStore_Address_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        public String Address { get; set; }

        public String DescriptionShortNL { get; set; }

        public String DescriptionLongNL { get; set; }

        public String DescriptionShortEN { get; set; }

        public String DescriptionLongEN { get; set; }

        public String DescriptionShortFR { get; set; }

        public String DescriptionLongFR { get; set; }

        public String DescriptionShortDE { get; set; }

        public String DescriptionLongDE { get; set; }

        public DbGeography Coordinates { get; set; }

        public String Avatar { get; set; }

        public String ImageList { get; set; }

        [RegularExpression(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", ErrorMessageResourceName = "Manage_AddStore_Website_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        public String Website { get; set; }

        [Required(ErrorMessageResourceName = "Manage_AddStore_Florist_Email_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        [EmailAddress(ErrorMessageResourceName = "Manage_AddStore_Florist_Email_REGEX_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        public String Email { get; set; }

        [Required(ErrorMessageResourceName = "Manage_AddStore_Facebook_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        public String Facebook { get; set; }

        public virtual Florist Florist { get; set; }

        public virtual Location Location { get; set; }

        public virtual Genre Genre { get; set; }
    }
}