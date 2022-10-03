using AutoMapper;
using Fotoblog.DAL;
using Fotoblog.Utils.ViewModels.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using System.ComponentModel.DataAnnotations;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Fotoblog.DAL.Migrations;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Fotoblog.BLL.Services.EmailService
{
    public class EmailService : BaseService, IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(ApplicationDbContext dbContext, IMapper mapper, IConfiguration configuration) : base(dbContext, mapper)
        {
            _configuration = configuration;
        }

        public async Task SendUserResetPasswordLink(string userName, string token, string userEmail)
        {
            var clientUrl = _configuration.GetSection("Client:Url").Value;

            var emailBodyTemplate = File.ReadAllText(
                Path.Combine(
                    Environment.CurrentDirectory,
                    @"..\Fotoblog.Utils\Templates\Auth\reset-password-email.html"
                    ));

            var emailBody = emailBodyTemplate.Replace("{{username}}", userName)
                .Replace("{{clientUrl}}", clientUrl)
                .Replace("{{email}}", userEmail)
                .Replace("{{token}}", token);

            await Send(userEmail, "Zmiana hasła w serwisie Fotoblog", emailBody);
        }

        public async Task SendUserActivationLink(string userName, string token, string userEmail)
        {
            var clientUrl = _configuration.GetSection("Client:Url").Value;

            var emailBodyTemplate = File.ReadAllText(
                Path.Combine(
                    Environment.CurrentDirectory,
                    @"..\Fotoblog.Utils\Templates\Auth\acc-confirmation-email.html"
                    ));

            var emailBody = emailBodyTemplate.Replace("{{username}}", userName)
                .Replace("{{clientUrl}}", clientUrl)
                .Replace("{{email}}", userEmail)
                .Replace("{{token}}", token);

            await Send(userEmail, "Potwierdź @ w serwisie Fotoblog", emailBody);
        }

        private async Task<Unit> Send([EmailAddress] string sendToAddress, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:MailboxAddress").Value));
            email.To.Add(MailboxAddress.Parse(sendToAddress));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration.GetSection("Email:SMTP").Value, 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(
                _configuration.GetSection("Email:Username").Value,
                _configuration.GetSection("Email:Password").Value);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return Unit.Value;
        }
    }
}
