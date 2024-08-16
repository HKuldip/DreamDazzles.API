using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzle.Repository.Interface;
using DreamDazzles.DTO.User;
using DreamDazzles.Repository.Interface;
using DreamDazzles.Service.Interface;
 

namespace DreamDazzles.Service.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
  


        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<string> SendForgotPasswordEmail(User user, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _usersRepository.SendForgotPasswordEmail(user, traceid, token);
            }
            catch (Exception)
            {

                throw;
            }
           
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

        public async Task<ClientResponse> UserLogin(Login login, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _usersRepository.UserLoginAsync(login, traceid,token);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<ClientResponse> ResetPassword(ResetPassword reset, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _usersRepository.ResetPassword(reset, traceid, token);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
