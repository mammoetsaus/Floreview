using Floreview.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class Florist
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "Manage_AddStore_Florist_FirstName_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        [StringLength(35, MinimumLength = 2, ErrorMessageResourceName = "Manage_AddStore_Florist_FirstName_Length_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        public String FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Manage_AddStore_Florist_LastName_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        [StringLength(35, MinimumLength = 2, ErrorMessageResourceName = "Manage_AddStore_Florist_LastName_Length_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        public String LastName { get; set; }

        [RegularExpression(@"\+3[23](?:\s*?\(0\))?(?:\s*?\d){8}$", ErrorMessageResourceName = "Manage_AddStore_Florist_Phone_REGEX_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        public String Phone { get; set; }

        [RegularExpression(@"\+3[23](?:\s*?\(0\))?(?:\s*?\d){9}$", ErrorMessageResourceName = "Manage_AddStore_Florist_Mobile_REGEX_Error", ErrorMessageResourceType = typeof(Global), ErrorMessage = null)]
        public String Cellphone { get; set; }

        public String ImagePath { get; set; }
    }
}