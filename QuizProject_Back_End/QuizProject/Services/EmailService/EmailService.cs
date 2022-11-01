using Microsoft.Extensions.Options;
using QuizProject.Models.AppData;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using QuizProject.Models.ResponseModels;
using QuizProject.Models;
using System.Collections.Generic;

namespace QuizProject.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly AppConf _appConf;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<AppConf> options, ILogger<EmailService> logger)
        {
            _logger = logger;
            _appConf = options.Value;
        }

        public async Task<UserManagerResponse> SendEmailAsync(string toEmail, string subject, string mail)
        {
            try
            {
                MailAddress to = new MailAddress(toEmail);
                MailAddress from = new MailAddress(_appConf.Email.Sender.From, _appConf.Email.Sender.SenderName);
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.Subject = subject;
                mailMessage.Body = mail;
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(_appConf.Email.SMTP.Client.Host, _appConf.Email.SMTP.Client.Port);
                smtpClient.Credentials = new NetworkCredential(_appConf.Email.SMTP.Credentials.Name, _appConf.Email.SMTP.Credentials.Password);
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
                _logger.LogError("Error during Sending Email!\n{0}", e.Message);
                return new UserManagerResponse
                {
                    Message = "Email sending failed",
                    Success = false,
                    Errors = new List<string> { e.Message }
                };
            }

            return new UserManagerResponse
            {
                Message = "Email sent successfully!",
                Success = true,
            };
        }
    }
}
