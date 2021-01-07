using System.Collections.Generic;

namespace GeoService.API.Models
{
    public class ContinentOutApi
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public List<string> Countries { get; set; } = new List<string>();
    }
}