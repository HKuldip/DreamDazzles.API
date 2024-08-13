using DreamDazzle.DTO;
using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzle.Repository.Repositories;
using DreamDazzles.DTO.User;
using DreamDazzles.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
 

namespace DreamDazzles.Repository.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MainDBContext _context;
        private readonly ILogger<UsersRepository> _logger;
        private readonly UserManager<User> _userManager;
        //private readonly RoleManager<User> _roleManager;
        private readonly IConfiguration _configuration;

        public UsersRepository(MainDBContext context, ILogger<UsersRepository> logger, UserManager<User> userManager, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            //_roleManager = roleManager;
            _configuration = configuration;

        }

        public async Task<ClientResponse> UserAddAsync(string Password, string traceid, CancellationToken token = default)
        {
            ClientResponse<ProductDTO> response = new();
            string mname = "GetProductByIdAsync";
            response.IsSuccess = false;
            response.HttpRequest = "";
            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var userId = Guid.NewGuid();
                    var roleId = Guid.NewGuid();


                    _logger.LogInformation($"{mname}: Exit | trace: " + traceid);

                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{mname}: Error => {ex.Message} | trace: " + traceid);
                }
            }
            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{mname}: Request has cancelled.. | trace: " + traceid);
                response.Message = $"{mname}: Request has cancelled.. | trace: " + traceid;
            }
            return response;
        }

        public async Task<ClientResponse> UserLoginAsync(Login login)
        {
            ClientResponse response = new ClientResponse();
            try
            {
                LoginResponse resUser = new();
                var user = await _userManager.FindByEmailAsync(login.username);
                if (user != null)
                {
                    if (user != null && await _userManager.CheckPasswordAsync(user, login.password))
                    {
                        User role = new User();
                        var auth = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name, login.username),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                        };
                        var userRole = await _userManager.GetRolesAsync(user);
                        foreach (var roles in userRole)
                        {
                            auth.Add(new Claim(ClaimTypes.Role, roles));

                            resUser.Role = roles;
                            //role = await _roleManager.FindByNameAsync(roles);
                        }


                        var jwtToken = Genrate(auth);


                        resUser.RoleId = role.Id;
                        resUser.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                        resUser.Expiration = jwtToken.ValidTo;
                        resUser.Email = user.Email;
                        resUser.Id = new Guid(user.Id);
                        //resUser.PreferedLanguage = user.PreferredLanguage;
                        resUser.SignDate = DateTime.UtcNow;
                        resUser.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.UserName);


                        //resUser.PreferedLanguage = user.PreferredLanguage;

                        response.Message = AppConstant.LoginSucess;
                        response.HttpResponse = resUser;
                        response.IsSuccess = true;
                        response.StatusCode = HttpStatusCode.OK;
                        return response;

                    }
                    else
                    {

                        response.Message = AppConstant.LoginFailed;
                        response.HttpResponse = null;
                        response.IsSuccess = false;
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        return response;
                    }
                }
                else
                {

                    response.Message = "No User Found";
                    response.HttpResponse = null;
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    return response;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

            return response;

        }



        //JWT token Genarate
        JwtSecurityToken Genrate(List<Claim> claim)
        {
            try
            {
                var JwtKey = _configuration.GetSection("Jwt:secretKey").Value;
                var Jwtissuer = _configuration.GetSection("Jwt:issuer").Value;
                var JwtValidAudience = _configuration.GetSection("Jwt:ValidateAudience").Value;
                var expireTime = _configuration.GetSection("Jwt:expiryMinutes").Value;
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
                var token = new JwtSecurityToken(
                    issuer: Jwtissuer,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(expireTime)),
                    audience: JwtValidAudience,
                    claims: claim.ToArray(),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                var TokenJwt = new JwtSecurityTokenHandler().WriteToken(token);

                return token;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }


}
