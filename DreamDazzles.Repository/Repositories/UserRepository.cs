using DreamDazzle.Model.Data;
using DreamDazzle.Model.User;
using DreamDazzles.DTO.User;
using DreamDazzles.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly UserManager<AspNetUsers> _userManager;
        private readonly RoleManager<AspNetRoles> _roleManager;

        public UserRepository(UserManager<AspNetUsers> userManager, RoleManager<AspNetRoles> roleManager, IUserRepository userRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ClientResponse> RegisterUser(Register register)
        {
            ClientResponse response = new ClientResponse();
            try
            {
                var userexist = await _userManager.FindByEmailAsync(register.Email);
                if (userexist != null)
                {
                    response.Message = "User Already exists";

                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.Forbidden;

                    return response;
                }

                AspNetUsers user = new()
                {
                    //Email = register.Email,
                    //SecurityStamp = Guid.NewGuid().ToString(),
                    //UserName = register.username,

                };
                if (await _roleManager.RoleExistsAsync(register.Role))
                {

                    var result = await _userManager.CreateAsync(user, register.password);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var emailConfirm = await _userManager.ConfirmEmailAsync(user, token);

                    if (!result.Succeeded)
                    {

                        response.Message = "User Create Failed";
                        response.HttpResponse = null;
                        response.IsSuccess = false;
                        response.StatusCode = HttpStatusCode.InternalServerError;

                        return response;
                    }
                    await _userManager.AddToRoleAsync(user, register.Role);
                    var userData = await _userManager.FindByEmailAsync(register.Email);
                    response.Message = "User Create Successfully";
                    response.HttpResponse = null;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;

                    return response;
                }
                else
                {

                    response.Message = "User role not existes";
                    response.HttpResponse = null;
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;

                    return response;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
