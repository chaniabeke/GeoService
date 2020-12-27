using System;

namespace GeoService.Domain.Exceptions
{
    public class ContinentManagerException : Exception
    {
        public ContinentManagerException(string message) : base(message)
        {
        }

        public ContinentManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}