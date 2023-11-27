using TwoFactorAuth.TwoFactorAuthDto;

namespace TwoFactorAuth.Interfaces;

public interface IAuthService
{
    Task Login(UserLoginDto userLoginData);
    Task Register(UserRegisterDto userRegisterData);
}