using Floreview.DataAccess.Context;
using Floreview.DataAccess.Interfaces;
using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floreview.DataAccess.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocation
    {
        #region Constructor
        public LocationRepository(FlowerContext context) : base(context)
        {
            
        }
        #endregion

        #region ILocation Interface
        public IEnumerable<Location> GetLocationsAutocomplete(String query)
        {
            var result = (from l in context.Location.Where(i => i.City.StartsWith(query)) select l).OrderBy(n => n.City);
            return result.ToList<Location>();
        }

        public Location GetLocationByCityName(string query)
        {
            var result = (from l in context.Location.Where(i => i.City.Equals(query)) select l).FirstOrDefault();
            return result;
        }
        #endregion
    }
}