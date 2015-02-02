using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class Blog
    {
        public int ID { get; set; }

        public String Avatar { get; set; }

        public String TitleNL { get; set; }

        public String TitleEN { get; set; }

        public String TitleFR { get; set; }

        public String TitleDE { get; set; }

        public String TeaserNL { get; set; }

        public String TeaserEN { get; set; }

        public String TeaserFR { get; set; }

        public String TeaserDE { get; set; }

        public String ContentNL { get; set; }

        public String ContentEN { get; set; }

        public String ContentFR { get; set; }

        public String ContentDE { get; set; }

        public DateTime PublishDate { get; set; }

        public virtual BlogCategory Category { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}