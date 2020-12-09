using GeoService.Domain.Interfaces;
using GeoService.EF.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoService.EF.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private DataContext context;

        public CountryRepository(DataContext context)
        {
            this.context = context;
        }
    }
}
