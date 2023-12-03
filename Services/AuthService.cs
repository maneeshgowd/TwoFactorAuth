using AutoMapper;
using TwoFactorAuth.DataModels;
using TwoFactorAuth.DBContext;
using TwoFactorAuth.Interfaces;
using TwoFactorAuth.TwoFactorAuthDto;

namespace TwoFactorAuth.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _dataContext;
    private readonly IEmailService _emailService;
    private readonly IJwtService _jwtService;
    private readonly IMapper _autoMapper;
    public AuthService(DataContext context, IEmailService emailService, IJwtService jwtService, IMapper mapper)
    {
        _dataContext = context;
        _emailService = emailService;
        _jwtService = jwtService;
        _autoMapper = mapper;
    }
    public async Task<string> Login(UserLoginDto userLoginData)
    {
        var user = await UserExists(userLoginData.Email) ??
            throw new Exception("Invalid Email or Password");

        var isPasswordValid = _jwtService.VerifyPasswordHash(userLoginData.Password, user.PasswordHash, user.PasswordSalt);

        if (!isPasswordValid)
            throw new Exception("Invalid Email or Password");

        return _jwtService.IssueJwtToken(user);
    }

    public async Task<string> Register(UserRegisterDto userRegisterData)
    {
        var user = await UserExists(userRegisterData.Email);

        if (user is not null)
            throw new Exception("User with the given email already exists.");

        var newUser = _autoMapper.Map<RegisterModel>(userRegisterData);

        _jwtService.CreatePasswordHash(userRegisterData.Password, out byte[] passwordHash, out byte[] passwordSalt);
        newUser.PasswordHash = passwordHash;
        newUser.PasswordSalt = passwordSalt;


        await _dataContext.AddAsync(newUser);
        await _dataContext.SaveChangesAsync();

        return _jwtService.IssueJwtToken(userRegisterData);
    }

    public async Task<string> VerifyOtp(int otp)
    {
        var generatedOtp = await _dataContext.UsersOtp.FindAsync(otp).ConfigureAwait(false);

        if (generatedOtp == null)
            throw new Exception("Something went wrong");

        if (generatedOtp.Otp != otp)
            throw new Exception("Invalid otp");

        if (generatedOtp.TimeStamp > DateTime.UtcNow.AddMinutes(-5) || generatedOtp.TimeStamp < DateTime.UtcNow.AddMinutes(-5))
            throw new Exception("Invalid otp");

        return "success";
    }

    private async Task<RegisterModel> UserExists(string email)
    {
        return (await _dataContext.Users.FindAsync(email))!;
    }
}
