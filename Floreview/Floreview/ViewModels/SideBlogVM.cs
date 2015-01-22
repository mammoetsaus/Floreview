using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.ViewModels
{
    public class SideBlogVM
    {
        #region Fields & Props
        public String Author { get; set; }

        public int TypeID { get; set; }

        public String Query { get; set; }

        public List<BlogType> Categories { get; set; }

        public int[] Frequencies { get; set; }
        #endregion
    }
}