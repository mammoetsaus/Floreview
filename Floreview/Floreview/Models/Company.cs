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
        #region Fields & Props
        public int ID { get; set; }

        [Required(ErrorMessage = "Gelieve een naam in te vullen.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De naam van een winkel heeft min 2 en max 50 karkaters.")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Gelieve een adres in te vullen.")]
        public String Address { get; set; }

        [Required(ErrorMessage = "Gelieve een korte beschrijving in te vullen.")]
        [StringLength(150, MinimumLength = 20, ErrorMessage = "Gelieve min 20 en max 150 karakters te gebruiken.")]
        public String DescriptionShort { get; set; }

        [Required(ErrorMessage = "Gelieve een lange beschrijving in te vullen.")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Gelieve min 20 en max 500 karakters te gebruiken.")]
        public String DescriptionLong { get; set; }

        public DbGeography Coordinates { get; set; }

        public String Avatar { get; set; }

        public String ImageList { get; set; }

        [RegularExpression(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", ErrorMessage = "Gelieve een correcte website in te vullen.")]
        public String Website { get; set; }

        [Required(ErrorMessage = "Gelieve een e-mail in te vullen.")]
        [EmailAddress(ErrorMessage = "Gelieve een geldig e-mailadres in te vullen")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Gelieve een facebook profiel in te vullen.")]
        public String Facebook { get; set; }
        

        public virtual Florist Florist { get; set; }

        public virtual Location Location { get; set; }

        public virtual Genre Genre { get; set; }
        #endregion
    }
}