using System;
using System.Net;

namespace BusinessLogic.Exceptions
{
    /// <summary>
    /// Exception for 400 code
    /// </summary>
    public class BadRequestException : Exception, IStatusCodeException
    {
        public int Status { get; set; } = (int)HttpStatusCode.BadRequest;

        public BadRequestException(string message) : base(message)
        {

        }
    }
}
