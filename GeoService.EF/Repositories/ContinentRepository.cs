using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;
using GeoService.EF.Mappers;
using GeoService.EF.Models;
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
            ContinentDB continentDB = ContinentMapper.ContinentToDBModel(continent);
            context.Continents.Add(continentDB);
            context.SaveChanges();
            Continent newContinent = ContinentMapper.ContinentDBToBusinessModel(continentDB);
            return newContinent;
        }

        public Continent Find(int id)
        {
            ContinentDB continentDB = context.Continents.Find(id);
            Continent continent = ContinentMapper.ContinentDBToBusinessModel(continentDB);
            return continent;
        }

        public List<Country> GetCountries(int id)
        {
            List<CountryDB> countryDBs = context.Countries.Where(x => x.ContinentId == id).ToList();
            List<Country> countries = CountryMapper.CountryDBListToBusinessModel(countryDBs);
            return countries;
        }

        public void RemoveContinent(Continent continent)
        {
            ContinentDB continentDB = ContinentMapper.ContinentToDBModel(continent);
            context.Continents.Remove(continentDB);
        }

        public void UpdateContinent(Continent oldContinent, Continent newContinent)
        {
            oldContinent.Name = newContinent.Name;
            //TODO EF - update countries?
        }
    }
}