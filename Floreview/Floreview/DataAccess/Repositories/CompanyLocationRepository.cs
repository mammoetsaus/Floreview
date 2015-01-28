using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Repositories
{
    public class CompanyLocationRepository : GenericRepository<CompanyLocation>, ICompanyLocation
    {
        public CompanyLocationRepository()
        {

        }

        public CompanyLocationRepository(FlowerContext context) : base (context)
        {

        }

        public IEnumerable<CompanyLocation> GetAllCompanyLocationsByLocationID(int ID)
        {
            var result = (from c in context.CompanyLocation.Where(i => i.Location.ID.Equals(ID)) select c);
            return result.ToList<CompanyLocation>();
        }
    }
}