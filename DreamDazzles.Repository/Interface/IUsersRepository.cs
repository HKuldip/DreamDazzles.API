using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzles.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Repository.Interface
{
    public interface IUsersRepository
    {
        Task<ClientResponse> UserAddAsync(string Password, string traceid, CancellationToken token = default);
        Task<ClientResponse> UserLoginAsync(Login login, string traceid, CancellationToken token = default);
        Task<ClientResponse> ResetPassword(ResetPassword reset, string traceid, CancellationToken token = default);

        Task<string> SendForgotPasswordEmail(User user, string traceid, CancellationToken token = default);


    }
}
