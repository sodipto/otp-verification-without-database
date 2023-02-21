using otp_verify_without_database.DTOs;
using otp_verify_without_database.RequestPayloads;
using otp_verify_without_database.Utils;

namespace otp_verify_without_database.DTOBuilders.Auth
{
    public static class AuthDTOBuilder
    {
        #region Public Methods
        public static OTPDTO SendOTP(OTPPayload payload)
        {
            int otp = GenerateOTP();
            var expireTime = GetExpiredTime();

            var data = string.Format("{0}.{1}.{2}", payload.PhoneNumber, otp, expireTime);
            var hash = HashHelper.CreateHMACSHA256Hash(data, GlobalConfig.GetConfiguration("AuthKey"));

            var dto = new OTPDTO
            {
                PhoneNumber = payload.PhoneNumber,
                OTPToken = string.Format("{0}.{1}", hash, expireTime),
            };

            return dto;
        }

        public static AuthDTO Login(LoginPayload payload)
        {
            var isVerifiedOTP = IsVerifiedOTP(payload);
            if (!isVerifiedOTP)
                ErrorHelper.ThrowValidatorException("OTP", "OTP invalid or expired.");

            return new AuthDTO { Message = "Login Successful." };
        }

        #endregion

        #region Private methods
        private static bool IsVerifiedOTP(LoginPayload payload)
        {
            var hashedData = payload.OTPToken.Split('.');
            if (hashedData.Length != 2)
                return false;

            var expiredTime = DateTimeHelper.GetUnixTimeStampToDateTime(long.Parse(hashedData[1]));
            if (DateTime.UtcNow > expiredTime)
                return false;

            var data = string.Format("{0}.{1}.{2}", payload.PhoneNumber, payload.OTP, hashedData[1]);
            var requestedHash = HashHelper.CreateHMACSHA256Hash(data, GlobalConfig.GetConfiguration("AuthKey"));

            if (requestedHash == hashedData[0])
                return true;
            else
                return false;
        }

        private static long GetExpiredTime()
        {
            int minutes = int.Parse(GlobalConfig.GetConfiguration("SMS:OtpExpiredMinutes"));
            var otpExpiredTimeStamp = new DateTimeOffset(DateTime.UtcNow.AddMinutes(minutes)).ToUnixTimeMilliseconds();

            return otpExpiredTimeStamp;
        }

        private static int GenerateOTP() => new Random().Next(100000, 999999);

        #endregion
    }
}
