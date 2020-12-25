using GeoService.API.Models;
using GeoService.Domain.Managers;
using GeoService.Domain.Models;

namespace GeoService.API.Mappers
{
    public static class ContinentMapper
    {
        public static Continent ContinentInMapper(CountryManager countryManager, ContinentInApi continentIn)
        {
            Continent continent = new Continent();
            continent.Name = continentIn.Name;
            if (continentIn.Countries != null)
            {
                foreach (var countryId in continentIn.Countries)
                {
                    Country country = countryManager.Find(countryId);
                    continent.AddCountry(country);
                }
            }
            return continent;
        }
        public static ContinentOutApi ContinentOutMapper(string hostUrl, Continent continent)
        {
            ContinentOutApi continentOut= new ContinentOutApi();
            continentOut.Id = hostUrl + "/api/continent/" + continent.Id;
            continentOut.Name = continent.Name;
            if (continent.Countries != null)
            {
                foreach (Country country in continent.Countries)
                {
                    string url = hostUrl + "/api/continent/" + continent.Id + "/country/" + country.Id;
                    continentOut.Countries.Add(url);
                }
            }
            return continentOut;
        }
    }
}