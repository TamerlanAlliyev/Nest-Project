using Nest.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Nest.Services.Implements
{
    public class EmailService : IEmailService
    {
        readonly IConfiguration _configuration;

        public EmailService(IConfiguration config)
        {
            _configuration = config;
        }

        public void Send(string userEmail,string Subject,string Body,bool IsBody=true)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Port = Convert.ToInt32(_configuration["Email:Port"]);
            smtp.Host = _configuration["Email:Host"];
            smtp.EnableSsl = true;

            MailAddress from = new MailAddress(_configuration["Email:Username"],"Nest support");
            MailAddress to = new MailAddress(userEmail);

            NetworkCredential credential = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]);
            smtp.Credentials = credential;

            MailMessage message = new MailMessage(from,to);
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = IsBody;

            smtp.Send(message);
        }
    }
}
