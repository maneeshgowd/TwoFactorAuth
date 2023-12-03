namespace TwoFactorAuth.DataModels
{
    public class OtpModel
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Otp { get; set; }
        public RegisterModel RegisteringUser { get; set; }
    }
}
