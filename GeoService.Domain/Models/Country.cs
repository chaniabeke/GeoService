using GeoService.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoService.Domain.Models
{
    public class Country
    {
        #region Properties
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Population { get; private set; }
        public double Surface { get; private set; }
        public Continent Continent { get; private set; }
        #endregion

        #region Constructors
        public Country (int id, string name, Continent continent)
        {
            SetId(id);
            SetName(name);
            SetContinent(continent);
        }
        public Country(int id, string name, Continent continent, double surface): this(id, name, continent)
        {
            SetSurface(surface);
        }
        public Country(int id, string name, Continent continent, int population) : this(id, name, continent)
        {
            SetPopulation(population);
        }
        public Country(int id, string name, Continent continent, int population, double surface) : this(id, name, continent, population)
        {
            SetSurface(surface);
        }
        #endregion

        #region Getters and Setters
        public void SetId(int id)
        {
            if (id <= 0) throw new CountryException("Country - Id invalid");
            Id = id;
        }
        public void SetName(string name)
        {
            if (name is null) throw new CountryException("Country - name invalid");
            if (name.Trim().Length < 1) throw new CountryException("Country - name invalid");
            Name = name;
        }
        public void SetPopulation(int population)
        {
            if (population <= 0) throw new CountryException("Country - population invalid");
            Population = population;
        }
        public void SetSurface(double surface)
        {
            if (surface <= 0) throw new CountryException("Country - surface invalid");
            Surface = surface;
        }
        public void SetContinent(Continent newContinent)
        {
            if (newContinent == null) throw new CountryException("Country - SetContinent - invalid continent");
            //TODO think
            if (newContinent == Continent) throw new CountryException("Country - SetContinent - not new");

            //Check if old continent had country and deletes
            if (Continent != null)
            {
                if (Continent.HasCountry(this)) Continent.DeleteCountry(this);
            }

            //check if new continent has country and adds
            if (!newContinent.HasCountry(this)) newContinent.AddCountry(this);

            Continent = newContinent;
        }
        #endregion

        #region Methods
        #endregion
    }
}
