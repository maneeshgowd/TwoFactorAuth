using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TwoFactorAuth.Interfaces;

namespace TwoFactorAuth.Services
{
    public class EmailService : IEmailService
    {
        private readonly dynamic _options;
        private readonly static string _fromMail = "twofactor-auth@mail.com";
        private readonly static string _subject = "Email verification";
        public EmailService(IOptionsSnapshot<dynamic> options)
        {
            _options = options.Value;
        }
        public async Task<string> VerifyEmail(string name, string email)
        {
            int randomNumber = new Random().Next(100000, 999999);

            var mailMessage = ConstructMail(randomNumber, name, email);

            var SmtpClient = RegisterMailClient();

            await SmtpClient.SendMailAsync(mailMessage).ConfigureAwait(false);

            return randomNumber.ToString();
        }

        private SmtpClient RegisterMailClient()
        {
            SmtpClient smtpClient = new()
            {
                Host = _options.Host!,
                Port = Convert.ToInt32(_options.Port)!,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_options.Username!, _options.Password!),
                EnableSsl = true
            };

            return smtpClient;
        }

        private static MailMessage ConstructMail(int otp, string name, string toMail)
        {

            var file = File.ReadAllTextAsync(@"email.html").GetAwaiter().GetResult();

            MailMessage mailMessage = new()
            {
                From = new MailAddress(_fromMail),
                Subject = _subject,
                Body = file.Replace("{{OTP}}", $"{otp}").Replace("{{NAME}}", name),
                IsBodyHtml = true
            };

            mailMessage.To.Add(toMail);

            return mailMessage;
        }
    }
}
