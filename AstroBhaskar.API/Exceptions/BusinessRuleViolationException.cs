using System;

namespace AstroBhaskar.API.Exceptions
{
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string errorResponseType, string message) : base(message)
        {
            this.ErrorResponseType = errorResponseType;
        }

        public string ErrorResponseType { get; set; }
    }
}
