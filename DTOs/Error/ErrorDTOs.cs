namespace otp_verify_without_database.DTOs
{
    public class ErrorDTO
    {
        public ErrorInfo Error { get; set; }
    }

    public class ErrorInfo
    {
        public string LogID { get; set; }
        public int StatusCode { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Messages { get; set; }
    }
}
