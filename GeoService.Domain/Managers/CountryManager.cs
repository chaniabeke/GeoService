using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;

namespace GeoService.Domain.Managers
{
    public class CountryManager : ICountryRepository
    {
        private IUnitOfWork uow;

        public CountryManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void AddCountry(Country country)
        {
            uow.Countries.AddCountry(country);
        }

        public Country Find(int continentId, int countryId)
        {
            return uow.Countries.Find(continentId, countryId);
        }

        public void RemoveCountry(Country country)
        {
            uow.Countries.RemoveCountry(country);
        }

        public void UpdateCountry(Country oldCountry, Country newCountry)
        {
            uow.Countries.UpdateCountry(oldCountry, newCountry);
        }
    }
}