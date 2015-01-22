using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class Location
    {
        #region Fields & Props
        public int ID { get; set; }

        public int Zip { get; set; }

        [Required(ErrorMessage = "Gelieve een gemeente in te vullen.")]
        public String City { get; set; }

        public int ZipMain { get; set; }

        public String CityMain { get; set; }


        public virtual Province Province { get; set; }
        #endregion
    }
}