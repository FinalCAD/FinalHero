using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BusinessLogic.Exceptions;
using System.Net;
using BusinessLogic.DTO.Responses;

namespace API.Middlewares
{
    /// <summary>
    /// Error handling middleware
    /// </summary>
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                await WriteResponseAsync(context, ex);
            }
        }

        private static async Task WriteResponseAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (ex is IStatusCodeException)
            {
                var exStatus = ex as IStatusCodeException;
                response.StatusCode = exStatus.Status;
            }

            await response.WriteAsync(JsonConvert.SerializeObject(
                new ErrorResponseDTO
                {
                    Code = response.StatusCode,
                    Message = ex.Message
                }));
        }
    }
}
