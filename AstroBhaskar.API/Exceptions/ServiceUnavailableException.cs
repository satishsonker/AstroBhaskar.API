using System;

namespace AstroBhaskar.API.Exceptions
{
    public class ServiceUnavailableException : Exception
    {
        public string HealthReportStatus { get; set; }
        public ServiceUnavailableException(string healthReportStatus) => this.HealthReportStatus = healthReportStatus;
    }
}
