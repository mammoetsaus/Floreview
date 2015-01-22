using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floreview.DataAccess.Interfaces
{
    public interface ICompany : IGeneric<Company>
    {
        IEnumerable<Company> GetCompaniesSearchName(String name);

        IEnumerable<Company> GetCompaniesSearchCity(String city);

        IEnumerable<Company> GetCompaniesSearchBoth(String name, String city);

        IEnumerable<Company> GetCompaniesMainCity(String main, String city, int region);

        Company GetCompanyByID(int id);
    }
}
