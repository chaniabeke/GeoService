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

        public Country AddCountry(Country country)
        {
            CountryDB countryDB = CountryMapper.CountryToDBModel(country);
            context.Countries.Add(countryDB);
            context.SaveChanges();
            Country newCountry = CountryMapper.CountryDBToBusinessModel(countryDB);
            return newCountry;
        }

        public Country Find(int countryId)
        {
            //TODO continnetn id naar object
            CountryDB countryDB = context.Countries.Find(countryId);
            ContinentDB continentDB = context.Continents.Find(countryDB.ContinentId);
            countryDB.ContinentId = continentDB.Id;
            Country country = CountryMapper.CountryDBToBusinessModel(countryDB);
            return country;
        }

        public Country Find(int continentId, int countryId)
        {
            CountryDB countryDB = context.Countries.Find(countryId);
            ContinentDB continentDB = context.Continents.Find(continentId);
            countryDB.ContinentId = continentId;
            countryDB.Continent = continentDB;
            Country country = CountryMapper.CountryDBToBusinessModel(countryDB);
            return country;
        }

        public void RemoveCountry(Country country)
        {
            CountryDB countryDB = CountryMapper.CountryToDBModel(country);
            context.Countries.Remove(countryDB);
        }

        public void UpdateCountry(Country country, string name, Continent continent, int population, double surface)
        {
            CountryDB countryDB = CountryMapper.CountryToDBModel(country);
            countryDB.Name = name;
            countryDB.ContinentId = continent.Id;
            countryDB.Population = population;
            countryDB.Surface = surface;
            context.SaveChanges();
        }

        public void UpdateCountry(int id, string name, int continentId, int population, double surface)
        {
            CountryDB countryDB = context.Countries.Find(id);
            countryDB.Name = name;
            countryDB.ContinentId = continentId;
            countryDB.Population = population;
            countryDB.Surface = surface;
        }
    }
}