using AstroBhaskar.API.Dto.Response.GlobalResponse;
using Microsoft.AspNetCore.Builder;
using System;

namespace AstroBhaskar.API.Middleware
{
    public static class CustomExceptionHandler
    {
        private static ExceptionResponse? GetExceptionResponse(Exception exception)
        {
            return null;
        }

        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
