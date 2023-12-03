using TwoFactorAuth.TwoFactorAuthDto;

namespace TwoFactorAuth.Interfaces
{
    public interface IUserService
    {
        Task<bool> UpdatePassword(PasswordUpdateDto passwordData);
        Task<string> ForgotPassword(ForgotPasswordDto passwordData);
    }
}
