using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QuizProject.Services
{
    //TODO: Same it will be good to add logging
    //Interface should be in separated file
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string mail);
    }
    public class EmailService: IEmailService
    {
        private IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string mail)
        {
            MailAddress to = new MailAddress(toEmail);
            //TODO: configuration can be regitered for certain class and used in service constructor like IOptions<ClassName> option after option.Value
            // please check how it works and do it in this way
            MailAddress from = new MailAddress(_configuration["EmailSender:EmailFrom:From"], _configuration["EmailSender:EmailFrom:Name"]);
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = mail;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient(_configuration["EmailSender:SMTP:Client:Host"], Convert.ToInt32(_configuration["EmailSender:SMTP:Client:Port"]));
            smtpClient.Credentials = new NetworkCredential(_configuration["EmailSender:SMTP:Credentials:Name"], _configuration["EmailSender:SMTP:Credentials:Password"]);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mailMessage);

            //TODO: we dont know if email was sent or not. Use try catch and return bool

        }
    }
}
