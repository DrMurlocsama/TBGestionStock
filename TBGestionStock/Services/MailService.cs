using System.Net;
using System.Net.Mail;
using TBGestionStock.ASP.Models;

namespace TBGestionStock.ASP.Services

{
    public class MailService
    {
            private MailConfigurVM _config;
            private SmtpClient _client;
        
        public MailService(MailConfigurVM config, SmtpClient client)
        {
            _config = config;
            _client = client;
            _client.Host = config.Host;
            _client.Port = config.Port;
            _client.Credentials = new NetworkCredential(
                config.Email, config.Password
            );
            _client.EnableSsl = true;
        }

        public void EnvoyerMail(string subject, string content, params string[] to)
        {
            MailMessage message = new();
            message.From = new MailAddress(_config.Email);
            foreach (string email in to)
            {
                message.To.Add(email);
            }
            message.Subject = subject;
            message.Body = content;
            _client.Send(message);
        }
    }
}

