using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;
using GeoService.EF.Mappers;
using GeoService.EF.Models;
using System;
using System.Linq;

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
                Country newCountry = CountryMapper.CountryDBToBusinessModel(countryDB, country.Continent);
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
                if (countryDB != null)
                {
                    ContinentDB continentDB = context.Continents.Find(countryDB.ContinentId);

                    countryDB.ContinentId = continentDB.Id;
                    countryDB.Continent = continentDB;
                    Country country = CountryMapper.CountryDBToBusinessModel(countryDB);
                    return country;
                }
                return null;
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
                if (continentDB != null && countryDB != null)
                {
                    countryDB.ContinentId = continentId;
                    countryDB.Continent = continentDB;
                    Country country = CountryMapper.CountryDBToBusinessModel(countryDB);
                    return country;
                }
                return null;
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
                CountryDB countryDB = context.Countries.Where(x => x.Name == countryName).FirstOrDefault();
                if (countryDB != null) return CountryMapper.CountryDBToBusinessModel(countryDB);
                return null;
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

        public void UpdateCountry(int id, Country countryUpdated, int newContinentId)
        {
            try
            {
                CountryDB countryDB = context.Countries.Find(id);
                CountryDB countryDBUpdated = CountryMapper.CountryToDBModel(countryUpdated);
                countryDB.ContinentId = newContinentId;
                countryDB.Name = countryDBUpdated.Name;
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