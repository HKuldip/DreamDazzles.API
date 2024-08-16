using DreamDazzle.Model.Data;
using DreamDazzles.DTO.User;
using DreamDazzles.Repository.Interface;
 
using DreamDazzles.Service.Emails;
using DreamDazzles.Service.Interface;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
 
using MimeKit;
 





namespace DreamDazzles.Service.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfigration _emailConfig;
        private readonly UserManager<DreamDazzle.Model.User> _userManager;
        private readonly IEmailRepository _emailRepo;

        public EmailService(EmailConfigration emailConfig, UserManager<DreamDazzle.Model.User> userManager)
        {
            _emailConfig = emailConfig;
            _userManager = userManager;
        }


        public void SendEmail(Messages messages, Dictionary<string, string> placeholder)
        {
            var emailMessage = CreateEmailMessages(messages, placeholder);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessages(Messages messages, Dictionary<string, string> placeholder)
        {
            try
            {
                var emailMessage = new MimeMessage();
                ChangePlaceHolde(messages, placeholder);
                emailMessage.From.Add(new MailboxAddress("DreamDazzle", _emailConfig.From));
                emailMessage.To.AddRange(messages.To);
                emailMessage.Subject = messages.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = messages.Content };

                return emailMessage;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ChangePlaceHolde(Messages messages, Dictionary<string, string> placeholder)
        {
            try
            {
                if (!string.IsNullOrEmpty(messages.Content) && placeholder != null)
                {
                    foreach (var item in placeholder)
                    {
                        if (messages.Content.Contains(item.Key))
                        {
                            messages.Content = messages.Content.Replace(item.Key, item.Value);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                client.Send(mailMessage);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                client.Disconnect(true);
                client.Dispose();
            }
        }


        public async Task<bool> IsEmailExist(string email)
        {


            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<ClientResponse> SendForgotPasswordEmail(Forgotmail user)
        {
            try
            {
                return await _emailRepo.GenerateForgotPasswordTokenAsync(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
