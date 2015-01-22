using Floreview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floreview.DataAccess.Interfaces
{
    public interface ILocation : IGeneric<Location>
    {
        IEnumerable<Location> GetLocationsAutocomplete(String query);

        Location GetLocationByCityName(String query);
    }
}
