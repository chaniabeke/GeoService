using GeoService.Domain.Models;
using System.Collections.Generic;

namespace GeoService.Domain.Interfaces
{
    public interface IContinentRepository
    {
        Continent AddContinent(Continent continent);

        void UpdateContinent(int id, string name);

        void RemoveContinent(Continent continent);

        Continent Find(int id);

        List<Country> GetCountries(int id);
    }
}