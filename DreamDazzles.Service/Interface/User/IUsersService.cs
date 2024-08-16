using DreamDazzle.Model.Data;
using DreamDazzles.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamDazzle.Model;


namespace DreamDazzles.Service.Interface
{
    public interface IUsersService
    {
        Task<ClientResponse> UserAddAsync(string Password, string traceid, CancellationToken token = default);

        Task<ClientResponse> UserLogin(Login login, string traceid, CancellationToken token = default);
        Task<ClientResponse> ResetPassword(ResetPassword reset, string traceid, CancellationToken token = default);


        Task<string> SendForgotPasswordEmail(User user, string traceid, CancellationToken token = default);


    }
}
