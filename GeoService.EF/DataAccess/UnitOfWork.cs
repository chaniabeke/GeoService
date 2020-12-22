using GeoService.Domain.Interfaces;
using GeoService.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoService.EF.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;

        public UnitOfWork()
        {
            this.context = new DataContext();

            Continents = new ContinentRepository(this.context);
            Countries = new CountryRepository(this.context);
        }

        public IContinentRepository Continents { get; private set; }
        public ICountryRepository Countries { get; private set; }

        public int Complete()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
