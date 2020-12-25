using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using System.Collections.Generic;

namespace GeoService.Domain.Managers
{
    public class ContinentManager
    {
        private IUnitOfWork uow;

        public ContinentManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Continent AddContinent(Continent continent)
        {
            uow.Continents.AddContinent(continent);
            uow.Complete();
            return continent;
        }

        public Continent Find(int id)
        {
            Continent continent = uow.Continents.Find(id);
            foreach (var country in GetCountries(id))
            {
                if (!continent.HasCountry(country))
                {
                    continent.AddCountry(country);
                }
            }
            return continent;
        }

        public List<Country> GetCountries(int id)
        {
            return uow.Continents.GetCountries(id);
        }

        public void RemoveContinent(Continent continent)
        {
            uow.Continents.RemoveContinent(continent);
        }

        public void UpdateContinent(Continent oldContinent, Continent newContinent)
        {
            uow.Continents.UpdateContinent(oldContinent, newContinent);
        }
    }
}