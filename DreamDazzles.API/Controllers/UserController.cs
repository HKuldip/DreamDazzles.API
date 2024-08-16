using Asp.Versioning;
using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzles.DTO.User;
using DreamDazzles.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DreamDazzles.Service.Interface;
using System.Net;
using DreamDazzles.Service.Emails;
using static System.Net.WebRequestMethods;



namespace DreamDazzles.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : BaseController<UserController>
    {
        private readonly IUsersService _usersService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;



        public UserController(IUsersService usersService, UserManager<User> userManager, IEmailService emailService, IConfiguration configuration, Serilog.ILogger slogger) : base(slogger)
        {
            _usersService = usersService;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;

        }

        [HttpPost("AddUsers")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddUsers(string FirstName, string LastName, string Email, CancellationToken token = default)
        {
            string methodName = "AddUsers";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { FirstName, LastName, Email } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);



                if (await _emailService.IsEmailExist(Email))
                {
                    objresp.Message = AppConstant.EmailExist;
                    objresp.IsSuccess = false;
                }
                else
                {
                    var password = Clscommon.GenerateRandomPassword();

                    var fullname = FirstName + "" + LastName;
                    int cnt = _userManager.Users.Count();
                    var user = new User
                    {
                        UserName = fullname + "" + (cnt + 1),

                        FirstName = Clscommon.FirstLetterToUpper(FirstName),
                        Lastname = Clscommon.FirstLetterToUpper(LastName),
                        Email = Email,

                    };


                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        Dictionary<string, string> placeholder = new Dictionary<string, string>();
                        placeholder["Passworddigit"] = password.ToString();
                        HtmlTemplate htmlTemplate = new HtmlTemplate();
                        Messages message = new Messages();

                        message.To = new List<MimeKit.MailboxAddress>
                        {
                            new MimeKit.MailboxAddress("", Email)
                        };
                        message.Content = htmlTemplate.SentPassword;
                        message.Subject = "Password";
                        _emailService.SendEmail(message, placeholder);

                        objresp.Message = AppConstant.ProfileCreateSuccess;
                        objresp.IsSuccess = true;
                    }
                    else
                    {
                        objresp.Message = result.Errors.ToString();
                        objresp.IsSuccess = false;
                    }
                }
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
        public async Task<IActionResult> UserLogin([FromBody] Login login, CancellationToken token = default)
        {
            #region asdd
            string methodName = "AddUsers";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            #endregion
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);
            try
            {
                objresp = await _usersService.UserLogin(login, traceId, token);

                return Ok(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }


        [HttpGet("EmailVerify")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> EmailVerify(string Email)
        {
            string methodName = "EmailVerify";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId);
            try
            {

                var otp = 0;
                if (await _emailService.IsEmailExist(Email))
                {
                    objresp.Message = AppConstant.EmailExist;
                    objresp.IsSuccess = false;
                }
                else
                {
                    otp = Clscommon.GenerateOtp();

                    Dictionary<string, string> placeholder = new Dictionary<string, string>();
                    placeholder["otpdigit"] = otp.ToString();

                    Messages message = new Messages();
                    message.To = new List<MimeKit.MailboxAddress>
                    {
                        new MimeKit.MailboxAddress("", Email)
                    };

                    HtmlTemplate htmlTemplate = new HtmlTemplate();
                    message.Content = htmlTemplate.SentOtp;
                    message.Subject = "Email Verification";


                    _emailService.SendEmail(message, placeholder);
                }




                return Ok(otp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }


        [HttpGet("ForgotPasswordSentEmail")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ForgotPasswordSentEmail(string Email, CancellationToken token = default)
        {
            string methodName = "ForgotPasswordSentEmail";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);
            try
            {

                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null)
                {

                    var FoggrtPasswordLink = _usersService.SendForgotPasswordEmail(user, traceId, token);

                    Dictionary<string, string> placeholder = new Dictionary<string, string>();
                    placeholder["Link"] = FoggrtPasswordLink.Result;
                    placeholder["UserName"] = user.FirstName + " " + user.Lastname;

                    Messages message = new Messages();
                    message.To = new List<MimeKit.MailboxAddress>
                    {
                        new MimeKit.MailboxAddress("", Email)
                    };

                    HtmlTemplate htmlTemplate = new HtmlTemplate();
                    message.Content = htmlTemplate.ForgetPassword;
                    message.Subject = "Requset Forgot Password";
                    _logger.Error(message.Content, "");

                    _emailService.SendEmail(message, placeholder);

                }
                else
                {

                    objresp.Message = "User Not Found";
                    objresp.HttpResponse = null;
                    objresp.IsSuccess = false;
                    objresp.StatusCode = HttpStatusCode.OK;
                }

                return returnAction(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }



        [HttpPost("ResetPassword")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ResetPassword(ResetPassword reset,CancellationToken token = default)
        {
            string methodName = "ResetPassword";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);



                reset.Token = reset.Token.Replace(' ', '+');
                objresp = await _usersService.ResetPassword(reset, traceId, token);

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
