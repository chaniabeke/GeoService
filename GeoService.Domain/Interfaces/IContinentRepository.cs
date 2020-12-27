using GeoService.Domain.Models;
using System.Collections.Generic;

namespace GeoService.Domain.Interfaces
{
    public interface IContinentRepository
    {
        Continent AddContinent(Continent continent);

        void UpdateContinent(int continentId, Continent updatedContinent);

        void RemoveContinent(int continentId);

        Continent Find(int continentId);

        Continent Find(string continentName);

        List<Country> GetCountries(int continentId);
    }
}