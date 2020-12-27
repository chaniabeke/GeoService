using System.Collections.Generic;

namespace GeoService.EF.Models
{
    public class ContinentDB
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CountryDB> Countries { get; set; } = new List<CountryDB>();
    }
}