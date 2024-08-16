using Castle.Core.Configuration;
using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzles.DTO.User;
using DreamDazzles.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;




namespace DreamDazzles.Repository.Repositories
{

    public class EmailRepository: IEmailRepository
    {

        private readonly UserManager<User> _userManager;
 

        public EmailRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
         
        }

        public async Task<ClientResponse> GenerateForgotPasswordTokenAsync(Forgotmail user)
        {
            ClientResponse response = new ClientResponse();
            try
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(await _userManager.FindByEmailAsync(user.Email));
                if (!string.IsNullOrEmpty(token))
                {
                   
                }
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }



       


    }
}
