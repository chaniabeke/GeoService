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

        public Country AddCountry(Country country)
        {
            return uow.Countries.AddCountry(country);
        }

        public Country Find(int countryId)
        {
            return uow.Countries.Find(countryId);
        }

        public Country Find(int continentId, int countryId)
        {
            return uow.Countries.Find(continentId, countryId);
        }

        public void RemoveCountry(Country country)
        {
            uow.Countries.RemoveCountry(country);
        }

        public void UpdateCountry(int id, string name, int continentId, int population, double surface)
        {
            uow.Countries.UpdateCountry(id, name, continentId, population, surface);
        }
    }
}