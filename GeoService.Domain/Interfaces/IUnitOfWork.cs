using System;
using System.Collections.Generic;
using System.Text;

namespace GeoService.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IContinentRepository Continents { get; }
        ICountryRepository Countries { get; }
        int Complete();
    }
}
