using Microsoft.AspNetCore.Mvc;
using TwoFactorAuth.Interfaces;
using TwoFactorAuth.TwoFactorAuthDto;

namespace TwoFactorAuth.Controllers;

public class AuthController
{
    public static void Map(WebApplication app)
    {
        app.MapPost("api/auth/login", async (
            [FromBody] UserLoginDto userLoginData,
            IAuthService authService,
            HttpContext context) =>
        {
            try
            {
                string jwtToken = await authService.Login(userLoginData).ConfigureAwait(false);

                return Results.Ok(new
                {
                    StatusCode = 200,
                    Message = "success",
                    Data = jwtToken
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { StatusCode = 400, Message = ex.Message });
            }

        });

        app.MapPost("api/auth/register", async (
            [FromBody] UserRegisterDto userRegisterData,
            IAuthService authService,
            HttpContext context) =>
        {
            try
            {
                string jwtToken = await authService.Register(userRegisterData).ConfigureAwait(false);

                return Results.Ok(new
                {
                    StatusCode = 200,
                    Message = "success",
                    Data = jwtToken
                });

            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
        });

        app.MapGet("api/auth/verify/email",
            async ([FromQuery] string name, [FromQuery] string email,
            IEmailService service, HttpContext context) =>
        {
            try
            {
                await service.VerifyEmail(name, email).ConfigureAwait(false);

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        app.MapPost("api/auth/verify/otp", async ([FromBody] OtpDto userOtp, IEmailService service, HttpContext context) =>
        {
            try
            {
                await service.VerifyOtp(userOtp.Otp).ConfigureAwait(false);

                return Results.Ok(new { StatusCode = 200, Message = "Success" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }
}
