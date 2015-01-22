using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.Models
{
    public class News
    {
        #region Fields & Props
        public int ID { get; set; }

        public String Title { get; set; }

        public String Content { get; set; }

        public String ImagePath { get; set; }
        #endregion
    }
}