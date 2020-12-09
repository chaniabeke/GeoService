using GeoService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoService.Domain.Managers
{
    public class ContinentManager : IContinentRepository
    {
        private IUnitOfWork uow;

        public ContinentManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
    }
}
