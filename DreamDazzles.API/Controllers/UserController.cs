using DreamDazzle.Model.Data;
using DreamDazzle.Model.User;
using DreamDazzles.DTO.User;
using DreamDazzles.Service.Interface.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DreamDazzles.API.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly RoleManager<AspNetRoles> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _user;

        public UserController(UserManager<AspNetUsers> userManager, RoleManager<AspNetRoles> roleManager, IConfiguration configuration, IUserService user)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _user = user;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Register register)
        {
            ClientResponse objresp = new ClientResponse();
            try
            {

                objresp = await _user.RegisterUser(register);

                return Ok(objresp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
