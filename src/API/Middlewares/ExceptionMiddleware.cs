using Application.DTOs.ErrorsDto;
using Application.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var message = string.Empty;
                var result = string.Empty;

                switch (ex)
                {

                    case DbUpdateException sqlException:
                        var converExceptionTosqlException = ex.InnerException as SqlException;
                        if (converExceptionTosqlException.Number == 2601 || converExceptionTosqlException.Number == 2627)
                        {
                            statusCode = (int)HttpStatusCode.BadRequest;
                            message = "Ya existe el registro";
                            _logger.LogError(ex, ex.Message);
                        }
                        break;

                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogWarning(ex, ex.Message);
                        break;

                    case ValidatorException validatorException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        result = JsonConvert.SerializeObject(new CodeErrorExceptionDto(statusCode, "Se presentarón uno o mas errores de validación", ex.Message));
                        _logger.LogWarning(ex, ex.Message);
                        break;

                    case UserFriendlyException userFriendlyException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        message = ex.Message;
                        _logger.LogWarning(ex, ex.Message);
                        break;

                    case BusinessException businessException:
                        message = ex.Message;
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        _logger.LogError(ex, ex.Message);
                        break;

                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        message = "Ocurrio un error interno";
                        _logger.LogError(ex, ex.Message);
                        break;
                }

                if (string.IsNullOrEmpty(result))
                {
                    var details = ex.StackTrace;
                    result = JsonConvert.SerializeObject(new CodeErrorExceptionDto(statusCode, message, details));
                }

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(result);

            }

        }

    }
}
