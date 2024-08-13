using DreamDazzle.Model.Data;
using DreamDazzle.Repository.Interface;
using DreamDazzles.DTO.User;
using DreamDazzles.Repository.Interface;
using DreamDazzles.Service.Interface.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
  


        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<ClientResponse> UserAddAsync(string Password, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _usersRepository.UserAddAsync(Password, traceid, token);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ClientResponse> UserLogin(Login login)
        {
            try
            {
                return await _usersRepository.UserLoginAsync(login);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
