using GeoService.API.Models;
using GeoService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.API.Mappers
{
    public static class ContinentMapper
    {
        public static Continent ContinentInMapper(ContinentApi continentApi) {
            Continent continent = new Continent();
            continent.Name = continentApi.Name;
            if(continentApi.Countries != null)
            {
                foreach (Country country in continentApi.Countries)
                {
                    continent.AddCountry(country);
                }
            }
            return continent;
        }
    }
}
