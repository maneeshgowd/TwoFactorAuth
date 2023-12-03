using System.ComponentModel.DataAnnotations;

namespace TwoFactorAuth.DataModels;

public class RegisterModel
{
    [Key]
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateOnly Dob { get; set; }
    public Gender Gender { get; set; }
    public byte[] PasswordSalt { get; set; } = [];
    public byte[] PasswordHash { get; set; } = [];
    public bool IsEmailVerified { get; set; } = false;
    public bool IsTwoFactorAuthEnabled { get; set; } = false;
    public List<OtpModel> OtpModels { get; set; } = [];
};