using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;
using GeoService.EF.Mappers;
using GeoService.EF.Models;
using System;

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
            try
            {
                CountryDB countryDB = CountryMapper.CountryToDBModel(country);
                context.Countries.Add(countryDB);
                context.SaveChanges();
                Country newCountry = CountryMapper.CountryDBToBusinessModel(countryDB);
                return newCountry;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Country Find(int countryId)
        {
            try
            {
                CountryDB countryDB = context.Countries.Find(countryId);
                ContinentDB continentDB = context.Continents.Find(countryDB.ContinentId);
                countryDB.ContinentId = continentDB.Id;
                countryDB.Continent = continentDB;
                Country country = CountryMapper.CountryDBToBusinessModel(countryDB);
                return country;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Country Find(int continentId, int countryId)
        {
            try
            {
                CountryDB countryDB = context.Countries.Find(countryId);
                ContinentDB continentDB = context.Continents.Find(continentId);
                countryDB.ContinentId = continentId;
                countryDB.Continent = continentDB;
                Country country = CountryMapper.CountryDBToBusinessModel(countryDB);
                return country;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Country Find(string countryName)
        {
            try
            {
                CountryDB countryDB = context.Countries.Find(countryName);
                Country country = CountryMapper.CountryDBToBusinessModel(countryDB);
                return country;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoveCountry(int countryId)
        {
            try
            {
                CountryDB countryDB = context.Countries.Find(countryId);
                context.Countries.Remove(countryDB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCountry(int id, Country countryUpdated)
        {
            try
            {
                CountryDB countryDB = context.Countries.Find(id);
                CountryDB countryDBUpdated = CountryMapper.CountryToDBModel(countryUpdated);
                countryDB.Name = countryDBUpdated.Name;
                countryDB.ContinentId = countryDBUpdated.ContinentId;
                countryDB.Population = countryDBUpdated.Population;
                countryDB.Surface = countryDBUpdated.Surface;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}