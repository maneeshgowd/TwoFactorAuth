using Microsoft.AspNetCore.Mvc;
using TwoFactorAuth.Interfaces;
using TwoFactorAuth.TwoFactorAuthDto;

namespace TwoFactorAuth.Controllers;

[Route("api/[controller]")]
public class AuthController
{
    public static void Map(WebApplication app)
    {
        app.MapPost("login", async 
        (
            [FromBody] UserLoginDto userLoginData,
            IAuthService authService,
            HttpContext context) =>
        {
            await authService.Login(userLoginData);
        });

        app.MapPost("register", async
        (
            [FromBody] UserRegisterDto userRegisterData,
            IAuthService authService,
            HttpContext context) =>
        {
            await authService.Register(userRegisterData);
        });
    }
}