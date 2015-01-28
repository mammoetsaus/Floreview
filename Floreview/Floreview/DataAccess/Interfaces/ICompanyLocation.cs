using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Interfaces
{
    public interface ICompanyLocation : IGeneric<CompanyLocation>
    {
        IEnumerable<CompanyLocation> GetAllCompanyLocationsByLocationID(int ID);
    }
}