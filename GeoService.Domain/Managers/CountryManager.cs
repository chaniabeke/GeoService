using GeoService.Domain.Exceptions;
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
            if (country == null) throw new CountryManagerException("Add Continent - continent cannot be null");
            if (Find(country.Name) != null)
                throw new CountryManagerException($"Add Country - Country with name: {country.Name} already exist.");

            return uow.Countries.AddCountry(country);
        }

        public Country Find(int countryId)
        {
            if (countryId < 0) throw new CountryManagerException("FindCountry - invalid id");

            return uow.Countries.Find(countryId);
        }

        public Country Find(int continentId, int countryId)
        {
            Country country = uow.Countries.Find(continentId, countryId);

            if (country.Continent.Id != continentId)
                throw new CountryManagerException("FindCountry - continent doesn't belong to country");

            return country;
        }

        public Country Find(string countryName)
        {
            if (countryName.Trim().Length <= 0) throw new CountryManagerException("FindCountry - invalid CountryName.");
            return uow.Countries.Find(countryName);
        }

        public void RemoveCountry(int countryId)
        {
            if (Find(countryId) == null)
                throw new CountryManagerException("Country doesn't exist");

            uow.Countries.RemoveCountry(countryId);
        }

        public void UpdateCountry(int id, Country countryUpdated)
        {
            if (countryUpdated == null) throw new CountryManagerException("Add Continent - continent cannot be null");
            if (Find(countryUpdated.Name) != null)
                throw new CountryManagerException($"Add Country - Country with name: {countryUpdated.Name} already exist.");

            uow.Countries.UpdateCountry(id, countryUpdated);
        }
    }
}