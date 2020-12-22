using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            context.Continents.Add(continent);
            return continent;
        }

        public Continent Find(int id)
        {
            return context.Continents.Find(id);
        }

        public List<Country> GetCountries(int id)
        {
            return context.Countries.Where(x => x.Continent.Id == id).ToList();
        }

        public void RemoveContinent(Continent continent)
        {
            context.Continents.Remove(continent);
        }

        public void UpdateContinent(Continent oldContinent, Continent newContinent)
        {
            oldContinent.Name = newContinent.Name;
        }
    }
}
