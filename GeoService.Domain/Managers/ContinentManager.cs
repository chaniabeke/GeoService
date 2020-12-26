using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using System.Collections.Generic;

namespace GeoService.Domain.Managers
{
    public class ContinentManager : IContinentRepository
    {
        private IUnitOfWork uow;

        public ContinentManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Continent AddContinent(Continent continent)
        {
            Continent continentNew = uow.Continents.AddContinent(continent);
            //TODO manager if country exist, else exception
            foreach (var countryNew in continent.Countries)
            {
                uow.Countries.UpdateCountry(countryNew.Id, countryNew.Name,
                    continentNew.Id, countryNew.Population, countryNew.Surface);
            }
            uow.Complete();
            continentNew = Find(continentNew.Id);
            return continentNew;
        }

        public Continent Find(int id)
        {
            Continent continent = uow.Continents.Find(id);
            if (GetCountries(id).Count != 0)
            {
                foreach (var country in GetCountries(id))
                {
                    if (!continent.HasCountry(country))
                    {
                        continent.AddCountry(country);
                    }
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

        public void UpdateContinent(int id, string name)
        {
            uow.Continents.UpdateContinent(id, name);
            uow.Complete();
        }
    }
}