namespace TwoFactorAuth.Interfaces
{
    public interface IJwtService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string IssueJwtToken(dynamic user);
    }
}
