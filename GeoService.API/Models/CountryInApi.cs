using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.API.Models
{
    public class CountryInApi
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public double Surface { get; set; }
        public int ContinentId { get; set; }
    }
}
