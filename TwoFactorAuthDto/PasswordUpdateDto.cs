namespace TwoFactorAuth.TwoFactorAuthDto
{
    public record PasswordUpdateDto(string OldPassword, string NewPassword, string ConfirmPassword);

}