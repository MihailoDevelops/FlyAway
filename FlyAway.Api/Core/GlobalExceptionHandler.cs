using FluentValidation;
using FlyAway.Application.Exceptions;
using Newtonsoft.Json;

namespace FlyAway.Api.Core
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                object response = null;
                var statusCode = StatusCodes.Status500InternalServerError;
                switch (ex)
                {
                    case UnauthorizedUseCaseException _:
                        statusCode = StatusCodes.Status403Forbidden;
                        response = new { message = "You have no permission to execute that command." };
                        break;
                    case EntityNotFoundException _:
                        statusCode = StatusCodes.Status404NotFound;
                        response = new { message = "Resource not found." };
                        break;
                    case ValidationException ve:
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        response = new { 
                            message = "Validation failed with the following errors:",
                            errors = ve.Errors.Select(x => new { x.PropertyName, x.ErrorMessage })
                        };
                        break;
                }

                httpContext.Response.StatusCode = statusCode;

                if (response != null)
                {
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    return;
                }

                await Task.FromResult(httpContext.Response);
            }
        }
    }
}
