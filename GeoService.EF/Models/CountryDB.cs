﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GeoService.EF.Models
{
    public class CountryDB
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public double Surface { get; set; }

        [ForeignKey("Continent")]
        public int ContinentId { get; set; }

        public ContinentDB Continent { get; set; }
    }
}