using GeoService.Domain.Interfaces;
using GeoService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            uow.Continents.AddContinent(continent);
            uow.Complete();
            return continent;
        }

        public Continent Find(int id)
        {
            return uow.Continents.Find(id);
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
