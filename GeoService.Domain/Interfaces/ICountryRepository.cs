using GeoService.Domain.Models;

namespace GeoService.Domain.Interfaces
{
    public interface ICountryRepository
    {
        Country AddCountry(Country country);

        void UpdateCountry(int countryId, Country country);

        void RemoveCountry(int countryId);

        Country Find(int countryId);

        Country Find(int continentId, int countryId);

        Country Find(string countryName);
    }
}