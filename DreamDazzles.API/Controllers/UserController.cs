using Asp.Versioning;
using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzles.DTO.User;
using DreamDazzles.Service;
 
using DreamDazzles.Service.Interface.User;
 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Reflection;

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
        }

        [HttpPost("AddUsers")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddUsers(string FirstName, string LastName,string Email, CancellationToken token = default)
        {
            string methodName = "AddUsers";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { FirstName, LastName, Email } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);

                var password = Clscommon.GenerateRandomPassword();

                var fullname = FirstName + "" + LastName;

                var user = new User
                {
                    UserName = fullname,
                    FirstName = FirstName,
                    Lastname = LastName,
                    Email = Email,
                 


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


        [HttpPost("UserLogin")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UserLogin([FromBody] Login login)
        {
            string methodName = "AddUsers";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId);
            try
            {
                objresp = await _usersService.UserLogin(login);

                return Ok(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }


        [HttpPost("EmailVerify")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> EmailVerify(string Email)
        {
            string methodName = "AddUsers";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId);
            try
            {

                var otp = Clscommon.GenerateOtp();


                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient("smtp.yourserver.com");

                    mail.From = new MailAddress("mr.mavani2002@gmail.com");
                    mail.To.Add(Email);
                    mail.Subject = "opt details";
                    mail.Body = "your verification code is = "+otp;

                    smtpServer.Port = 587; // Adjust port according to your SMTP server settings
                    smtpServer.Credentials = new NetworkCredential("your-email@domain.com", "your-password");
                    smtpServer.EnableSsl = true;

                    smtpServer.Send(mail);
                    Console.WriteLine("Mail Sent Successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }


                return Ok(otp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }
    }
}
