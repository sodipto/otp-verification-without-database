namespace otp_verify_without_database.Utils
{
    public static class GlobalConfig
    {
        private static IConfiguration _configuration { get; set; }
        public static IConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
        }

        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetConfiguration(string key)
        {
            return Configuration[key];
        }
    }
}
