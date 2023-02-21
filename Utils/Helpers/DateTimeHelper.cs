namespace otp_verify_without_database.Utils
{
    public static class DateTimeHelper
    {
        public static DateTime GetUnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(unixTimeStamp).ToUniversalTime();

            return dateTime;
        }
    }
}
