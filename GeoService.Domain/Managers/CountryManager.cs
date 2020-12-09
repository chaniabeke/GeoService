using GeoService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoService.Domain.Managers
{
    public class CountryManager : ICountryRepository
    {
        private IUnitOfWork uow;

        public CountryManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
    }
}
