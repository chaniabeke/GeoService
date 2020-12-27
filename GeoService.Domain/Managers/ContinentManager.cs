using GeoService.Domain.Exceptions;
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
            if (continent == null) throw new ContinentManagerException("Add Continent - continent cannot be null");
            if (Find(continent.Name) != null)
                throw new ContinentManagerException($"Add Continent - Continent with name: {continent.Name} already exist.");

            Continent continentNew = uow.Continents.AddContinent(continent);

            UpdateCountries(continent, continentNew);

            uow.Complete();
            continentNew = Find(continentNew.Id);
            return continentNew;
        }

        public Continent Find(int continentId)
        {
            if (continentId < 0) throw new ContinentManagerException("FindContinent - invalid id");
            Continent continent = uow.Continents.Find(continentId);
            if (GetCountries(continentId).Count != 0)
            {
                foreach (var country in GetCountries(continentId))
                {
                    if (!continent.HasCountry(country)) continent.AddCountry(country);
                }
            }
            return continent;
        }

        public Continent Find(string continentName)
        {
            if (continentName.Trim().Length <= 0) throw new ContinentManagerException("FindContinent - invalid ContinentName.");
            return uow.Continents.Find(continentName);
        }

        public List<Country> GetCountries(int continentId)
        {
            return uow.Continents.GetCountries(continentId);
        }

        public void RemoveContinent(int continentId)
        {
            if (Find(continentId) == null)
                throw new ContinentManagerException("Continent doesn't exist");
            uow.Continents.RemoveContinent(continentId);
        }

        public void UpdateContinent(int continentIdInDb, Continent updatedContinent)
        {
            if (updatedContinent == null) throw new ContinentManagerException("Add Continent - continent cannot be null");
            if (Find(updatedContinent.Name) != null)
                throw new ContinentManagerException($"Add Continent - Continent with name: {updatedContinent.Name} already exist.");

            uow.Continents.UpdateContinent(continentIdInDb, updatedContinent);
            if (updatedContinent.Countries.Count != 0) UpdateCountries(updatedContinent);
            uow.Complete();
        }

        private void UpdateCountries(Continent continent, Continent continentNew)
        {
            foreach (var countryNew in continent.Countries)
            {
                if (uow.Countries.Find(countryNew.Id) == null)
                    throw new ContinentManagerException($"Add Continent - Country with id: {countryNew.Id} doesn't exist.");
                uow.Countries.UpdateCountry(countryNew.Id, countryNew);
            }
        }

        private void UpdateCountries(Continent continent)
        {
            foreach (var countryNew in continent.Countries)
            {
                if (uow.Countries.Find(countryNew.Id) == null)
                    throw new ContinentManagerException($"Add Continent - Country with id: {countryNew.Id} doesn't exist.");
                uow.Countries.UpdateCountry(countryNew.Id, countryNew);
            }
        }
    }
}