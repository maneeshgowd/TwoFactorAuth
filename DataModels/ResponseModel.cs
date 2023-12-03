namespace TwoFactorAuth.DataModels
{
    public record ResponseModel(int Status, string Message, dynamic[] Data);
}
