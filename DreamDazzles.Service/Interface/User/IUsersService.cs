using DreamDazzle.Model.Data;
using DreamDazzles.DTO.User;


namespace DreamDazzles.Service.Interface
{
    public interface IUsersService
    {
        Task<ClientResponse> UserAddAsync(string Password, string traceid, CancellationToken token = default);

        Task<ClientResponse> UserLogin(Login login, string traceid, CancellationToken token = default);
        Task<ClientResponse> ResetPassword(ResetPassword reset, string traceid, CancellationToken token = default);


        Task<string> SendForgotPasswordEmail(DreamDazzle.Model.User user, string traceid, CancellationToken token = default);


    }
}
