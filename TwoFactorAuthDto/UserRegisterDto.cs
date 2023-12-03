using TwoFactorAuth.DataModels;
namespace TwoFactorAuth.TwoFactorAuthDto;
public record class UserRegisterDto
(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateOnly Dob,
    string Password,
    Gender Gender
);

