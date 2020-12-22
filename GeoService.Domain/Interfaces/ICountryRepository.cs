using GeoService.Domain.Models;

namespace GeoService.Domain.Interfaces
{
    public interface ICountryRepository
    {
        void AddCountry(Country country);

        void UpdateCountry(Country oldCountry, Country newCountry);

        void RemoveCountry(Country country);

        Country Find(int id);
    }
}