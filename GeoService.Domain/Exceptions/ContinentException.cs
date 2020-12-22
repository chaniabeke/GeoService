using System;

namespace GeoService.Domain.Exceptions
{
    public class ContinentException : Exception
    {
        public ContinentException(string message) : base(message)
        {
        }

        public ContinentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}