using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuizProject.Models;
using QuizProject.Models.AppData;
using QuizProject.Models.ResponseModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QuizProject.Services.EmailService
{
    public interface IEmailService
    {
        Task<UserManagerResponse> SendEmailAsync(string toEmail, string subject, string mail);
    }

}
