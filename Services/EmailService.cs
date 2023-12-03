using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TwoFactorAuth.DataModels;
using TwoFactorAuth.DBContext;
using TwoFactorAuth.Interfaces;
using TwoFactorAuth.SecretsModel;

namespace TwoFactorAuth.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSecrets _options;
        private readonly static string _fromMail = "twofactor-auth@mail.com";
        private readonly static string _subject = "Email verification";
        private readonly DataContext _dataContext;
        public EmailService(DataContext context, IOptionsSnapshot<SmtpSecrets> options)
        {
            _options = options.Get(SmtpSecrets.Mailtrap);
            _dataContext = context;
        }
        public async Task VerifyEmail(string name, string email)
        {
            int randomNumber = new Random().Next(100000, 999999);

            var mailMessage = ConstructMail(randomNumber, name, email);

            var SmtpClient = RegisterMailClient();

            await SmtpClient.SendMailAsync(mailMessage).ConfigureAwait(false);

            await StoreOtp(randomNumber, email).ConfigureAwait(false);
        }

        public async Task VerifyOtp(int otp)
        {
            var generatedOtp = await _dataContext.UsersOtp.FirstOrDefaultAsync(o => o.Otp == otp).ConfigureAwait(false);

            // return if email is already verified

            if (generatedOtp == null)
                throw new Exception("Something went wrong");

            if (generatedOtp.Otp != otp)
                throw new Exception("Invalid otp");

            var currentTime = DateTime.Now;

            if (!(generatedOtp.TimeStamp.CompareTo(currentTime.AddMinutes(5)) < 0) ||
                !(generatedOtp.TimeStamp.CompareTo(currentTime.AddMinutes(-5)) > 0))
                throw new Exception("Oops otp is expired");

            await UpdateEmailVerificationStatus(generatedOtp.UserEmail!).ConfigureAwait(false);
        }

        private async Task UpdateEmailVerificationStatus(string email)
        {
            var user = await _dataContext.Users.FindAsync(email);

            if (!user.IsEmailVerified)
                user.IsEmailVerified = true;

            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync().ConfigureAwait(false);
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

        private async Task StoreOtp(int otp, string email)
        {
            var otpModel = new OtpModel
            {
                Otp = otp,
                TimeStamp = DateTime.Now,
                UserEmail = email
            };

            await _dataContext.UsersOtp.AddAsync(otpModel).ConfigureAwait(false);
            await _dataContext.SaveChangesAsync().ConfigureAwait(false);
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
