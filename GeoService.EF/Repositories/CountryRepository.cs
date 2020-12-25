using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;
using GeoService.EF.Mappers;
using GeoService.EF.Models;

namespace GeoService.EF.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private DataContext context;

        public CountryRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddCountry(Country country)
        {
            CountryDB countryDB = CountryMapper.CountryToDBModel(country);
            context.Countries.Add(countryDB);
        }

        public Country Find(int continentId, int countryId)
        {
            CountryDB countryDB = context.Countries.Find(countryId);
            ContinentDB continentDB = context.Continents.Find(continentId);
            countryDB.Continent = continentDB;
            countryDB.ContinentId = continentId;
            Country country = CountryMapper.CountryDBToBusinessModel(countryDB);
            return country;
        }

        public void RemoveCountry(Country country)
        {
            CountryDB countryDB = CountryMapper.CountryToDBModel(country);
            context.Countries.Remove(countryDB);
        }

        public void UpdateCountry(Country oldCountry, Country newCountry)
        {
            oldCountry.Continent = newCountry.Continent;
            oldCountry.Population = newCountry.Population;
            oldCountry.Name = newCountry.Name;
            oldCountry.Surface = newCountry.Surface;
        }
    }
}