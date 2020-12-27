using GeoService.Domain.Exceptions;
using System;

namespace GeoService.Domain.Models
{
    public class Country
    {
        #region Fields

        private int _id;
        private string _name;
        private int _population;
        private double _surface;
        private Continent _continent;

        #endregion Fields

        #region Properties

        public int Id
        {
            get => _id;
            set => _id = SetId(value);
        }

        public string Name
        {
            get => _name;
            set => _name = SetName(value);
        }

        public int Population
        {
            get => _population;
            set => _population = SetPopulation(value);
        }

        public double Surface
        {
            get => _surface;
            set => _surface = SetSurface(value);
        }

        public Continent Continent
        {
            get => _continent;
            set => _continent = SetContinent(value);
        }

        #endregion Properties

        #region Constructors

        public Country()
        {
        }

        public Country(string name, int population, double surface)
        {
            this.Name = name;
            this.Surface = surface;
            this.Population = population;
        }

        public Country(int id, string name, int population, double surface)
        {
            this.Id = id;
            this.Name = name;
            this.Surface = surface;
            this.Population = population;
        }

        public Country(int id, string name, int population, double surface, Continent continent) : this(id, name, population, surface)
        {
            this.Continent = continent;
        }

        #endregion Constructors

        #region Getters and Setters

        private int SetId(int id)
        {
            if (id <= 0) throw new CountryException("Country - Id invalid");
            return id;
        }

        private string SetName(string name)
        {
            if (name is null) throw new CountryException("Country - name invalid");
            if (name.Trim().Length < 1) throw new CountryException("Country - name invalid");
            return name;
        }

        private int SetPopulation(int population)
        {
            if (population <= 0) throw new CountryException("Country - population invalid");
            return population;
        }

        private double SetSurface(double surface)
        {
            if (surface <= 0) throw new CountryException("Country - surface invalid");
            return surface;
        }

        private Continent SetContinent(Continent newContinent)
        {
            if (newContinent == null) throw new CountryException("Country - SetContinent - invalid continent");
            if (newContinent == Continent) throw new CountryException("Country - SetContinent - not new");

            //Check if old continent had country and deletes
            if (Continent != null)
            {
                if (Continent.HasCountry(this)) Continent.DeleteCountry(this);
            }

            //check if new continent has country and adds
            if (!newContinent.HasCountry(this)) newContinent.AddCountry(this);

            return newContinent;
        }

        #endregion Getters and Setters

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is Country country &&
                   Name == country.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        #endregion Methods
    }
}