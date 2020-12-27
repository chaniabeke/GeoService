using GeoService.Domain.Managers;
using GeoService.Domain.Models;
using GeoService.EF.DataAccess;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ContinentManager manager = new ContinentManager(new UnitOfWork());

            Continent continent = new Continent("Europe");
            continent.AddCountry(new Country("France", 5554285, 25742543265.527));
            continent.AddCountry(new Country("Spain", 252754, 57425742874.557));
            manager.AddContinent(continent);
        }
    }
}