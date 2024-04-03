using System;

namespace Weather_Monitoring_And_Reporting_Service
{
    public class WeatherDataFilePathNullException : Exception
    {
        public WeatherDataFilePathNullException(string message) : base(message)
        {
        }

        public WeatherDataFilePathNullException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
