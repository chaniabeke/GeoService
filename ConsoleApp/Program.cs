using GeoService.Domain.Managers;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ContinentManager continentManager = new ContinentManager(new UnitOfWork());
            CountryManager countryManager = new CountryManager(new UnitOfWork());

            Continent continent1 = new Continent("Antartica");
            Continent continent2 = new Continent("Europe");
            Continent continent1WithId = continentManager.AddContinent(continent1);
            Continent continent2WithId = continentManager.AddContinent(continent2);
            Country country1 = new Country("Tuvalu", 11792, 30, continent1WithId);
            Country country2 = new Country("Nauru", 10824, 20, continent1WithId);
            countryManager.AddCountry(country1);
            countryManager.AddCountry(country2);
        }
    }
}