using TwoFactorAuth.DataModels;
namespace TwoFactorAuth.TwoFactorAuthDto;
public record UserRegisterDto
{
  public string FirstName = string.Empty;
  public string LastName = string.Empty;
  public string Email = string.Empty;
  public string PhoneNumber = string.Empty;
  public DateOnly Dob;
  public string Password = string.Empty;
  public Gender Gender;
};

