using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        await _emailService.VerifyEmail(
            string.Concat(userRegisterData.FirstName, userRegisterData.LastName),
            userRegisterData.Email);

        await StoreUserData(userRegisterData);

        return _jwtService.IssueJwtToken(userRegisterData);
    }

    private async Task StoreUserData(UserRegisterDto userData)
    {
        var newUser = _autoMapper.Map<RegisterModel>(userData);
        var otpModels = await _dataContext.UsersOtp.Where(user => user.UserEmail == userData.Email)
            .ToListAsync();

        _jwtService.CreatePasswordHash(userData.Password, out byte[] passwordHash, out byte[] passwordSalt);
        newUser.PasswordHash = passwordHash;
        newUser.PasswordSalt = passwordSalt;
        newUser.OtpModels = otpModels;

        await _dataContext.AddAsync(newUser);
        await _dataContext.SaveChangesAsync();
    }

    private async Task<RegisterModel> UserExists(string email)
    {
        return (await _dataContext.Users.FindAsync(email))!;
    }
}
