using TwoFactorAuth.TwoFactorAuthDto;

namespace TwoFactorAuth.Interfaces;

public interface IAuthService
{
    Task<string> Login(UserLoginDto userLoginData);
    Task<string> Register(UserRegisterDto userRegisterData);
}