using GeoService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.API.Models
{
    public class ContinentApi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Country> Countries { get; set; }
    }
}
