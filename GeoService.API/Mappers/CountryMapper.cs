using GeoService.API.Models;
using GeoService.Domain.Managers;
using GeoService.Domain.Models;

namespace GeoService.API.Mappers
{
    public static class CountryMapper
    {
        public static Country CountryInMapper(ContinentManager continentManager,CountryInApi countryIn)
        {
            Country country = new Country();
            country.Name = countryIn.Name;
            country.Population = countryIn.Population;
            country.Surface = countryIn.Surface;
            country.Continent = continentManager.Find(countryIn.ContinentId);
            return country;
        }
        public static CountryOutApi CountryOutMapper(string hostUrl, Country country)
        {
            CountryOutApi countryOut = new CountryOutApi();
            countryOut.Id = hostUrl + "/api/continent/" + country.Continent.Id + "/country/" + country.Id;
            countryOut.Name = country.Name;
            countryOut.Population = country.Population;
            countryOut.Surface = country.Surface;
            countryOut.Continent = hostUrl + "/api/continent/" + country.Continent.Id;
            return countryOut;
        }
    }
}