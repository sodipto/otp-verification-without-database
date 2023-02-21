namespace otp_verify_without_database.DTOs
{
    public class OTPDTO
    {
        public string PhoneNumber { get; set; }
        public string OTPToken { get; set; }
        public string Message { get; set; } = "OTP send successfully.";
    }

    public class TokenDTO
    {
        public string Type { get; } = "Bearer";
        public long ExpiredIn { get; set; }
        public string AccessToken { get; set; }

    }

    public class AuthDTO
    {
        public string Message { get; set; }
    }
}
