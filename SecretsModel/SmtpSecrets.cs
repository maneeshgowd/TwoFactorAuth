namespace TwoFactorAuth.SecretsModel
{
    public class SmtpSecrets
    {
        public const string Bravo = nameof(Bravo);
        public const string Mailtrap = nameof(Mailtrap);

        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public string? Port { get; set; }
    }
}
