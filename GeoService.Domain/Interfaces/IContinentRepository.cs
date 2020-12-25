using GeoService.Domain.Models;
using System.Collections.Generic;

namespace GeoService.Domain.Interfaces
{
    public interface IContinentRepository
    {
        void AddContinent(Continent continent);

        void UpdateContinent(Continent oldContinent, Continent newContinent);

        void RemoveContinent(Continent continent);

        Continent Find(int id);

        List<Country> GetCountries(int id);
    }
}