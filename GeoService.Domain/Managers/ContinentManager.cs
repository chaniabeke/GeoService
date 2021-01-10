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

            Continent continentWithId = uow.Continents.AddContinent(continent);

            UpdateCountries(continentWithId, continent.Countries);

            uow.Complete();
            continentWithId = Find(continentWithId.Id);
            return continentWithId;
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
                throw new ContinentManagerException("Remove Continent - Continent doesn't exist");
            if (Find(continentId).Countries.Count > 0)
                throw new ContinentManagerException("Remove Continent - Continent still has countries.");

            uow.Continents.RemoveContinent(continentId);

            uow.Complete();
        }

        public void UpdateContinent(int continentIdInDb, Continent updatedContinent)
        {
            if (Find(continentIdInDb) == null) throw new ContinentManagerException("Update Continent - Continent doesn't exist");
            if (updatedContinent == null) throw new ContinentManagerException("Update Continent - continent cannot be null");
            if (Find(updatedContinent.Name) != null && Find(continentIdInDb).Name != Find(updatedContinent.Name).Name)
                throw new ContinentManagerException($"Update Continent - Continent with name: {updatedContinent.Name} already exist.");

            uow.Continents.UpdateContinent(continentIdInDb, updatedContinent);
            if (updatedContinent.Countries.Count != 0) UpdateCountries(updatedContinent, updatedContinent.Countries);

            uow.Complete();
        }

        private void UpdateCountries(Continent continentWithId, IReadOnlyList<Country> countries)
        {
            List<Country> countriesList = new List<Country>();
            foreach (var country in countries)
            {
                countriesList.Add(country);
            }
            foreach (var country in countriesList)
            {
                if (uow.Countries.Find(country.Id) == null)
                    throw new ContinentManagerException($"Add Continent - Country with id: {country.Id} doesn't exist.");
                country.Continent = continentWithId;
                uow.Countries.UpdateCountry(country.Id, country, continentWithId.Id);
            }
        }
    }
}