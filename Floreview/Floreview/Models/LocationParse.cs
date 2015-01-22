using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class LocationParse
    {
        public int Zip { get; set; }

        public String City { get; set; }

        public int ZipMain { get; set; }

        public String CityMain { get; set; }

        public String Province { get; set; }
    }
}