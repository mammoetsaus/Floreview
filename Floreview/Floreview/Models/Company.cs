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

        [Required(ErrorMessage = "Gelieve een naam in te vullen.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Een winkel heeft min. 2 en max 30 karakters.")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Gelieve een adres in te vullen.")]
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

        [RegularExpression(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", ErrorMessage = "Gelieve een correcte website in te vullen.")]
        public String Website { get; set; }

        [Required(ErrorMessage = "Gelieve een e-mail in te vullen.")]
        [EmailAddress(ErrorMessage = "Gelieve een correct e-mail in te vullen.")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Gelieve een Facebook profiel in te vullen.")]
        public String Facebook { get; set; }

        public virtual Florist Florist { get; set; }

        public virtual Location Location { get; set; }

        public virtual Genre Genre { get; set; }
    }
}