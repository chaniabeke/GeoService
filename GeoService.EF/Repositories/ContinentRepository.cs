using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;
using GeoService.EF.Mappers;
using GeoService.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoService.EF.Repositories
{
    public class ContinentRepository : IContinentRepository
    {
        private DataContext context;

        public ContinentRepository(DataContext context)
        {
            this.context = context;
        }

        public Continent AddContinent(Continent continent)
        {
            try
            {
                ContinentDB continentDB = ContinentMapper.ContinentToDBModel(continent);
                context.Continents.Add(continentDB);
                context.SaveChanges();
                Continent newContinent = ContinentMapper.ContinentDBToBusinessModel(continentDB);
                return newContinent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Continent Find(int continentId)
        {
            try
            {
                ContinentDB continentDB = context.Continents.Find(continentId);
                Continent continent = ContinentMapper.ContinentDBToBusinessModel(continentDB);
                return continent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Continent Find(string continentName)
        {
            try
            {
                ContinentDB continentDB = context.Continents.Where(x => x.Name == continentName).FirstOrDefault();
                Continent continent = ContinentMapper.ContinentDBToBusinessModel(continentDB);
                return continent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Country> GetCountries(int continentId)
        {
            try
            {
                List<CountryDB> countryDBs = context.Countries.Where(x => x.ContinentId == continentId).ToList();
                List<Country> countries = CountryMapper.CountryDBListToBusinessModel(countryDBs);
                return countries;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoveContinent(int continentId)
        {
            try
            {
                ContinentDB continentDB = context.Continents.Find(continentId);
                context.Continents.Remove(continentDB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateContinent(int id, Continent continentUpdated)
        {
            try
            {
                ContinentDB continentDB = context.Continents.Find(id);
                ContinentDB continentDBUpdated = ContinentMapper.ContinentToDBModel(continentUpdated);

                continentDB.Name = continentDBUpdated.Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}