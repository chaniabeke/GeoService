using GeoService.Domain.Models;
using GeoService.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoService.EF.Mappers
{
    internal static class ContinentMapper
    {
        internal static ContinentDB ContinentToDBModel(Continent continent)
        {
            ContinentDB continentDB = new ContinentDB();
            continentDB.Id = continent.Id;
            continentDB.Name = continent.Name;
            //foreach (Country country in continent.Countries)
            //{
            //    if (country != null)
            //    {
            //        CountryDB countryDB = CountryMapper.CountryToDBModel(country);
            //        continentDB.Countries.Add(countryDB);
            //    }
            //}
            return continentDB;
        }

        internal static Continent ContinentDBToBusinessModel(ContinentDB continentDB)
        {
            Continent continent = new Continent();
            if (continentDB != null)
            {
                continent.Id = continentDB.Id;
                continent.Name = continentDB.Name;
                //foreach (CountryDB countryDB in continentDB.Countries)
                //{
                //    if (countryDB != null)
                //    {
                //        Country country = CountryMapper.CountryDBToBusinessModel(countryDB);
                //        continent.AddCountry(country);
                //    }
                //}
                return continent;
            }
            return null;
        }

        //internal static Continent ContinentDBWithoutCountriesToBusinessModel(ContinentDB continentDB)
        //{
        //    Continent continent = new Continent();
        //    continent.Id = continentDB.Id;
        //    continent.Name = continentDB.Name;
        //    return continent;
        //}
        
    }
}
