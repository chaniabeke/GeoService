using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoService.Domain.Managers
{
    public class CountryManager : ICountryRepository
    {
        private IUnitOfWork uow;

        public CountryManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void AddCountry(Country country)
        {
            uow.Countries.AddCountry(country);
        }

        public Country Find(int id)
        {
            return uow.Countries.Find(id);
        }

        public void RemoveCountry(Country country)
        {
            uow.Countries.RemoveCountry(country);
        }

        public void UpdateCountry(Country oldCountry, Country newCountry)
        {
            uow.Countries.UpdateCountry(oldCountry, newCountry);
        }
    }
}
