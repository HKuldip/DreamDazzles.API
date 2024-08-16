using DreamDazzle.Model.Data;
using DreamDazzles.DTO.User;
using DreamDazzles.Service.Emails;
 

namespace DreamDazzles.Service.Interface
{
    public interface IEmailService
    {

        void SendEmail(Messages messages, Dictionary<string, string> placeholder);
        Task<bool> IsEmailExist(string email);

    }
}
