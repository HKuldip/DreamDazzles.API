using Asp.Versioning;
using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzles.Service;
using DreamDazzles.Service.Interface.Product;
using DreamDazzles.Service.Interface.User;
using DreamDazzles.Service.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DreamDazzles.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : BaseController<UserController>
    {
        private readonly IUsersService _usersService;
        private readonly UserManager<User> _userManager;

        public UserController(IUsersService usersService, UserManager<User> userManager, Serilog.ILogger slogger) : base(slogger)
        {
            _usersService = usersService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _user = user;
        }

        [HttpGet("GetAllUsers")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllUsers(CancellationToken token = default)
        {
            string methodName = "GetAllUsers";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);

                var password = Clscommon.GenerateRandomPassword();

                var user = new User
                {
                    UserName = "Test",
                    FirstName = "Test",
                    Lastname = "Test",
                    Email = "Test018@yopmail.com"
                };

                var result = await _userManager.CreateAsync(user, password);


                _logger.Information($"{methodName} - {httpMethod} Exit | trace: " + traceId);

                return returnAction(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }
    }
}
