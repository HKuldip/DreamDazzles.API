using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Emails
{
    public interface IEmailSender
    {
        public Task SendEmail(object jobDetails);
        public Task SendBulkEmailAsync<T>(
            List<EmailRequestDTO> requestDTOs,
            T dynamicTemplateData,
            string templateId = null,
            Attachment attachment = null);

        public Task SendEmailAsync<T>(
            string email,
            string subject,
            string message,
            T dynamicTemplateData,
            string templateId = null,
            Attachment attachment = null);
    }
    public class EmailSender : IEmailSender
    {
        public Task SendBulkEmailAsync<T>(List<EmailRequestDTO> requestDTOs, T dynamicTemplateData, string templateId = null, Attachment attachment = null)
        {
            throw new NotImplementedException();
        }

        public Task SendEmail(object jobDetails)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailAsync<T>(string email, string subject, string message, T dynamicTemplateData, string templateId = null, Attachment attachment = null)
        {
            throw new NotImplementedException();
        }
    }
}
