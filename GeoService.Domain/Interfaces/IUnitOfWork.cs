using System;

namespace GeoService.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IContinentRepository Continents { get; }
        ICountryRepository Countries { get; }

        int Complete();
    }
}