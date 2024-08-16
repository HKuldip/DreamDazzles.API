using DreamDazzle.Model.Data;
using DreamDazzles.DTO.User;
using DreamDazzles.Repository.Interface;
using DreamDazzles.Service.Interface.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<ClientResponse> RegisterUser(Register register)
        {
            try
            {
                return await _userRepo.RegisterUser(register);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
