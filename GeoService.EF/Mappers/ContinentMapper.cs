using GeoService.Domain.Models;
using GeoService.EF.Models;
using System;

namespace GeoService.EF.Mappers
{
    internal static class ContinentMapper
    {
        internal static ContinentDB ContinentToDBModel(Continent continent)
        {
            try
            {
                ContinentDB continentDB = new ContinentDB();
                continentDB.Id = continent.Id;
                continentDB.Name = continent.Name;
                return continentDB;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static Continent ContinentDBToBusinessModel(ContinentDB continentDB)
        {
            try
            {
                Continent continent = new Continent();
                if (continentDB != null)
                {
                    continent.Id = continentDB.Id;
                    continent.Name = continentDB.Name;
                    return continent;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}