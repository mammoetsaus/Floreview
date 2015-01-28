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

        [Required(ErrorMessage = "Gelieve een voornaam in te vullen.")]
        [StringLength(35, MinimumLength = 2, ErrorMessage = "Een voornaam heeft min 2 en max 35 karakters.")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Gelieve een achternaam in te vullen.")]
        [StringLength(35, MinimumLength = 2, ErrorMessage = "Een achternaam heeft min 2 en max 35 karakters.")]
        public String LastName { get; set; }

        [RegularExpression(@"\+3[23](?:\s*?\(0\))?(?:\s*?\d){8}$", ErrorMessage = "Gelieve een correct telefoonnummer in te vullen. (+329)")]
        public String Phone { get; set; }

        [RegularExpression(@"\+3[23](?:\s*?\(0\))?(?:\s*?\d){9}$", ErrorMessage = "Gelieve een correct GSM nummer in te vullen.  (+324)")]
        public String Cellphone { get; set; }

        public String ImagePath { get; set; }
    }
}