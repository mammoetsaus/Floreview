using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompany
    {
        public CompanyRepository(FlowerContext context) : base(context)
        {

        }

        public IEnumerable<Company> GetCompaniesByCompanyName(String company)
        {
            var result = (from c in context.Company.Where(i => i.Name.Contains(company) || i.Florist.FirstName.Contains(company) || i.Florist.LastName.Contains(company)) select c);
            return result.ToList<Company>();
        }

        public IEnumerable<Company> GetCompaniesByCityName(String city)
        {
            var result = (from c in context.Company.Where(i => i.Location.City.Contains(city)) select c);
            return result.ToList<Company>();
        }

        public IEnumerable<Company> GetCompaniesByCompanyAndCity(String company, String city)
        {
            var result = (from c in context.Company.Where(i => (i.Name.Contains(company) || i.Florist.FirstName.Contains(company) || i.Florist.LastName.Contains(company)) && (i.Location.City.Contains(city))) select c);
            return result.ToList<Company>();
        }


        public IEnumerable<Company> GetCompaniesMainCity(string main, string city, int region)
        {
            var result = (from c in context.Company.Where(i => (i.Location.CityMain.Equals(main) || (i.Location.ZipMain / 100).Equals(region)) && !i.Location.City.Equals(city)) select c);
            return result.ToList<Company>();
        }

        public Company GetCompanyByID(int id)
        {
            var result = (from c in context.Company.Where(i => i.ID.Equals(id)) select c).FirstOrDefault();
            return result;
        }
    }
}