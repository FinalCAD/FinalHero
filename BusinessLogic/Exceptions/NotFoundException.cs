using System;
using System.Net;

namespace BusinessLogic.Exceptions
{
    /// <summary>
    /// Exception for 404
    /// </summary>
    public class NotFoundException : Exception, IStatusCodeException
    {
        public int Status { get; set; } = (int)HttpStatusCode.NotFound;

        public NotFoundException(string message) : base(message)
        {
        }
       
    }
}
