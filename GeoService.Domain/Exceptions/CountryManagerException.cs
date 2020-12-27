using System;

namespace GeoService.Domain.Exceptions
{
    public class CountryManagerException : Exception
    {
        public CountryManagerException(string message) : base(message)
        {
        }

        public CountryManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}