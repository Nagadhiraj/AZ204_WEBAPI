using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HFWEBAPI.Common
{
    public class EmailService
    {
        private IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Response> SendEmailAsync(string mailsubject, string to, string plainTextContent, string htmlContent, IEnumerable<Attachment> attachments)
        {
            var apiKey = _config.GetValue<string>("Values:SEND_GRID_API");  //GetEnvironmentVariable("SEND_GRID_API"); //insert your Sendgrid API Key
            var sgclient = new SendGridClient(apiKey);
            var from = new EmailAddress(_config.GetValue<string>("Values:EMAIL_ADD"), "Holistic Fitness");
            var msg = MailHelper.CreateSingleEmail(from, new EmailAddress(to, "Client"), mailsubject, plainTextContent, htmlContent);
            if (attachments != null && attachments.Count() > 0)
            {
                msg.AddAttachments(attachments);
            }
            var response = await sgclient.SendEmailAsync(msg);
            return response;
        }
    }
}
