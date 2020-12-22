using GeoService.Domain.Models;
using System.Collections.Generic;

namespace GeoService.API.Models
{
    public class ContinentApi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Country> Countries { get; set; }
    }
}