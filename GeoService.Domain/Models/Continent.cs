using GeoService.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace GeoService.Domain.Models
{
    public class Continent
    {
        #region Fields

        private int _id;
        private string _name;
        private List<Country> _countriesFields = new List<Country>();

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

        private List<Country> _countries
        {
            get => _countriesFields;
            set => _countriesFields = SetCountries(value);
        }

        public IReadOnlyList<Country> Countries
        {
            get { return _countries.AsReadOnly(); }
        }

        #endregion Properties

        #region Constructors

        public Continent()
        {
        }

        public Continent(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Continent(int id, string name, List<Country> countries) : this(id, name)
        {
            this._countries = countries;
            foreach (Country country in countries) country.Continent = this;
        }

        #endregion Constructors

        #region Getters and Setters

        private int SetId(int id)
        {
            if (id <= 0) throw new ContinentException("Continent - Id invalid");
            return id;
        }

        private string SetName(string name)
        {
            if (name is null) throw new ContinentException("Continent - name invalid");
            if (name.Trim().Length < 1) throw new ContinentException("Continent - name invalid");
            return name;
        }

        private List<Country> SetCountries(List<Country> countries)
        {
            if (countries is null || countries.Count == 0) throw new ContinentException("Continent - invalid countriesList");
            return countries;
        }

        #endregion Getters and Setters

        #region Methods

        public int Population()
        {
            return _countries.Sum(x => x.Population);
        }

        public void AddCountry(Country country)
        {
            if (country == null) throw new ContinentException("Continent - AddCountry : invalid country");
            if (_countries.Contains(country))
                throw new ContinentException("Continent - AddCountry : country already exists");

            _countries.Add(country);
            if (country.Continent != this) country.Continent = this;
        }

        public bool HasCountry(Country country)
        {
            if (_countries.Contains(country)) return true;
            return false;
        }

        public void UpdateCountry(Country oldCountry, Country newCountry)
        {
            if (!_countries.Contains(oldCountry))
                throw new ContinentException("Continent - UpdateCountry : country doesn't exist");

            if (newCountry.Population != 0) oldCountry.Population = newCountry.Population;
            if (newCountry.Continent != null) oldCountry.Continent = newCountry.Continent;
            if (newCountry.Name != null) oldCountry.Name = newCountry.Name;
            if (newCountry.Surface != 0) oldCountry.Surface = newCountry.Surface;
        }

        //TODO verwijder land van continent wanneer land verwijdert wordt, in manager
        internal void DeleteCountry(Country country)
        {
            if (!_countries.Contains(country)) throw new ContinentException("Continent - DeleteCountry : country does not exist");
            _countries.Remove(country);
        }

        #endregion Methods
    }
}