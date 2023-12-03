namespace TwoFactorAuth.Interfaces
{
    public interface IEmailService
    {
        Task VerifyEmail(string name, string email);
        Task VerifyOtp(int otp);
    }
}
