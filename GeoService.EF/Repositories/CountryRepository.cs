using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

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
            context.Countries.Add(country);
        }

        public Country Find(int id)
        {
            return context.Countries.Find(id);
        }

        public void RemoveCountry(Country country)
        {
            context.Countries.Remove(country);
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
