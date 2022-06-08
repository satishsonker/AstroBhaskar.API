

#nullable enable 
using AstroBhaskar.API.Dto.Response.GlobalResponse;
using AstroBhaskar.API.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly Func<Exception, ExceptionResponse>? _localExceptionHandlerFunc;
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, Func<Exception, ExceptionResponse>? localExceptionHandlerFunc = null)
        {
            this._next = next;
            this._localExceptionHandlerFunc = localExceptionHandlerFunc;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExecptionAsync(context, ex);
            }
        }

        private async Task HandleExecptionAsync(HttpContext context, Exception exception)
        {
            // SeriLog Log exception in logger
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            ErrorResponse errorResponse = new ErrorResponse()
            {
                Message = exception.Message
            };

            ExceptionResponse exceptionResponse = this._localExceptionHandlerFunc != null
                ? this._localExceptionHandlerFunc(exception)
                : (ExceptionResponse)null;

            if (exceptionResponse != null)
            {
                response.StatusCode = exceptionResponse.StatusCode;
                errorResponse = exceptionResponse.ErrorResponse;
            }
            else
            {
                switch (exception)
                {
                    case BusinessRuleViolationException businessRuleViolationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.ErrorResponseType = businessRuleViolationException.ErrorResponseType;
                        break;
                    case NotFoundException notFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        errorResponse.ErrorResponseType = notFoundException.ErrorResponseType;
                        break;
                    case UnauthorizedException _:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        errorResponse.ErrorResponseType = "Unauthorized";
                        break;
                    case UnprocessableEntityException _:
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        errorResponse.ErrorResponseType = "UnprocessableEntity";
                        break;
                    case ForbiddenException _:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.ErrorResponseType = "Forbidden";
                        break;
                    case CustomValidationException validationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.ErrorResponseType = validationException.ErrorResponseType;
                        errorResponse.Errors = (object)validationException.Errors;
                        break;
                    case HttpRequestException requestException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.ErrorResponseType = "HttpClientError";
                        errorResponse.Message = requestException.Message;
                        break;
                    case ServiceUnavailableException unavailableException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.ErrorResponseType = "ServiceUnavailable";
                        errorResponse.Message = unavailableException.HealthReportStatus;
                        break;
                }

                string text = JsonSerializer.Serialize<ErrorResponse>(errorResponse);
                await response.WriteAsync(text);
            }
        }
    }
}

