using GeoService.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoService.Domain.Models
{
    public class Continent
    {
        #region Properties
        public int Id { get; private set; }
        public string Name { get; private set; }
        private List<Country> _countries = new List<Country>();
        public IReadOnlyList<Country> Countries
        {
            get { return _countries.AsReadOnly(); }
        }
        #endregion

        #region Constructors
        public Continent(int id, string name)
        {
            SetId(id);
            SetName(name);
        }
        public Continent(int id, string name, List<Country> countries) : this(id, name)
        {
            if (countries == null) throw new ContinentException("Continent - countries invalid");
            _countries = countries;
            foreach (Country country in countries) country.SetContinent(this);
        }
        #endregion

        #region Getters and Setters
        public void SetId(int id)
        {
            if (id <= 0) throw new ContinentException("Continent - Id invalid");
            Id = id;
        }
        public void SetName(string name)
        {
            if (name is null) throw new ContinentException("Continent - name invalid");
            if (name.Trim().Length < 1) throw new ContinentException("Continent - name invalid");
            Name = name;
        }
        #endregion

        #region Methods
        public int Population()
        {
            return _countries.Sum(x => x.Population);
        }
        public void AddCountry(Country country)
        {
            if (country == null) throw new ContinentException("Continent - AddCountry : Invalid Country");
            if (_countries.First(x => x.Name.Equals(country.Name)) != null) 
                throw new ContinentException("Continent - AddCountry : Country already exists");
           
            _countries.Add(country);
            if (country.Continent != this) country.SetContinent(this);
        }
        public bool HasCountry(Country country)
        {
            if (_countries.Contains(country)) return true;
            return false;
        }
        public void DeleteCountry(Country country)
        {
            if (!_countries.Contains(country)) throw new ContinentException("Continent - DeleteCountry - country does not exist");
            _countries.Remove(country);
        }
        #endregion
    }
}
