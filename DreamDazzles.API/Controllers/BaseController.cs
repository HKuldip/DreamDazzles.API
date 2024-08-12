using DreamDazzle.Model.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace DreamDazzles.API.Controllers
{
    public abstract class BaseController<T> : Controller
    {
        protected readonly Serilog.ILogger _logger;

        protected BaseController(Serilog.ILogger logger)
        {
            _logger = logger;
            //_logger.Information("NLog injected into HandleInternalRepositoryBase");
        }

        protected async Task<ClientResponse> AuthorizedLogRequestAsync(object request,
                                                            string methodName,
                                                            string httpMethod,
                                                            string traceId,
                                                            CancellationToken token = default)
        {
            var hpro = HttpContext.GetRequestedApiVersion().ToString();
            var userAgent = Request.Headers.UserAgent.ToString();
            token.Register(() => _logger.Information($"{methodName}-{httpMethod} is stopping."));

            if (token.IsCancellationRequested)
            {
                _logger.Information($"{methodName} - {httpMethod}: Request has been cancelled. | trace: {traceId}");
                throw new OperationCanceledException();
            }

            string requestPathWithQueryString = HttpContext.Request.Path + HttpContext.Request.QueryString;
            _logger.Information($"{methodName} - {httpMethod} ENTERED: Started => {httpMethod} {requestPathWithQueryString} | trace: {traceId}");


            ClientResponse response = new ClientResponse
            {
                Severity = SeverityType.status,
                MinorCode = CodeMinorValueType.authorizedrequest,
                HttpRequest = request,
                IsSuccess = true,
                StatusCode = HttpStatusCode.Continue,
                APIVersion = hpro,
                UserAgent = userAgent
            };

            string requestBodyJson = JsonConvert.SerializeObject(response);
            _logger.Debug($"{methodName} - {httpMethod} Started => Request Body: {requestBodyJson} | trace: {traceId}");

            return response;
        }

        protected IActionResult returnAction(ClientResponse objresp)
        {
            if (objresp.StatusCode == HttpStatusCode.OK || objresp.StatusCode == HttpStatusCode.NoContent)
            {
                return Ok(objresp);
            }
            else
            {
                return BadRequest(objresp);
            }
        }
    }
}
