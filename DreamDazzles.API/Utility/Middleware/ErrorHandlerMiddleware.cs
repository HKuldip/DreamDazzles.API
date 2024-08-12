using System.Text.Json;
using System.Net;
using DreamDazzle.Model.Helper;


namespace DreamDazzles.API.Utility.Middleware;
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        this._next = next;
        this._logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            _logger.LogInformation($"Before request {context.TraceIdentifier}");
            await _next(context);
            _logger.LogInformation($"After request {context.TraceIdentifier}");
        }
        catch (Exception error)
        {
            _logger.LogInformation($"Exception:  {context.TraceIdentifier}");
            //await HandleException(context, error);
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case AppException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case NullReferenceException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.SeeOther;
                    break;
                default:
                    // unhandled error
                    _logger.LogInformation(error, error.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }

    private Task HandleException(HttpContext context, Exception ex)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        switch (ex)
        {
            case AppException e:
                // custom application error
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case KeyNotFoundException e:
                // not found error
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case NullReferenceException e:
                // not found error
                response.StatusCode = (int)HttpStatusCode.SeeOther;
                break;
            default:
                // unhandled error
                _logger.LogInformation(ex, ex.Message);
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        //var result = JsonSerializer.Serialize(new { message = ex?.Message });
        //await response.WriteAsync(result);
        //_logger.LogError(ex.ToString());
        var errorMessageObject =
            new { Message = ex?.Message, Code = "system_error" };

        var errorMessage = JsonSerializer.Serialize(errorMessageObject);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(errorMessage);
    }
}
