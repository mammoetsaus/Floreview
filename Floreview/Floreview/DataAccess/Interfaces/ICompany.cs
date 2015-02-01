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
        IEnumerable<Company> GetCompaniesByCompanyName(String company);

        IEnumerable<Company> GetCompaniesByCityName(String city);

        IEnumerable<Company> GetCompaniesByCompanyAndCity(String company, String city);


        IEnumerable<Company> GetCompaniesMainCity(String main, String city, int region);

        Company GetCompanyByID(int id);
    }
}
