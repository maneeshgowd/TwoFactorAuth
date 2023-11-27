namespace TwoFactorAuth.DataModels;

public record RegisterModel
{
    public string FirstName = string.Empty;
    public string LastName = string.Empty;
    public string Email = string.Empty;
    public string PhoneNumber = string.Empty;
    public DateOnly Dob;
    public string Password = string.Empty;
    public Gender Gender;
    public byte[] PasswordSalt = Array.Empty<byte>();
    public byte[] PasswordHash = Array.Empty<byte>();
};