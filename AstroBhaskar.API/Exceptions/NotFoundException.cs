using System;

namespace AstroBhaskar.API.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string errorResponseType = "ObjectNotFound", string message = "Object not found") : base(message)
        {
            this.ErrorResponseType = errorResponseType;
        }

        public string ErrorResponseType { get; set; }
    }
}
