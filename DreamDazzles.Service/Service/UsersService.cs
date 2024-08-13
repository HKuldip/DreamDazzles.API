using DreamDazzle.Model.Data;
using DreamDazzle.Repository.Interface;
using DreamDazzles.Repository.Interface;
using DreamDazzles.Service.Interface.User;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
