using GeoService.Domain.Interfaces;
using GeoService.EF.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoService.EF.Repositories
{
    public class ContinentRepository : IContinentRepository
    {

        private DataContext context;

        public ContinentRepository(DataContext context)
        {
            this.context = context;
        }
    }
}
