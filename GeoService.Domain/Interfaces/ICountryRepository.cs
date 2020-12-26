using GeoService.Domain.Models;

namespace GeoService.Domain.Interfaces
{
    public interface ICountryRepository
    {
        Country AddCountry(Country country);

        void UpdateCountry(int id, string name, int continentId, int population, double surface);

        void RemoveCountry(Country country);

        Country Find(int countryId);

        Country Find(int continentId, int countryId);
    }
}