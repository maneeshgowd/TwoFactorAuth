namespace TwoFactorAuth.Interfaces
{
    public interface IEmailService
    {
        Task<string> VerifyEmail(string name, string email);
    }
}
