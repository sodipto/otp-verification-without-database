using Newtonsoft.Json;

namespace otp_verify_without_database.Utils
{
    public static class ErrorHelper
    {
        public static ErrorDTO GetErrorResponse(int statusCode, string type, Dictionary<string, string> messages)
        {
            var dto = new ErrorDTO();

            dto.Error = new ErrorInfo
            {
                LogID = Guid.NewGuid().ToString(),
                StatusCode = statusCode,
                Type = type,
                Messages = messages
            };

            return dto;
        }

        public static ErrorDTO GetErrorResponse(int statusCode, string type, string message, string key = "Generic")
        {
            var dto = new ErrorDTO();

            dto.Error = new ErrorInfo
            {
                LogID = Guid.NewGuid().ToString(),
                StatusCode = statusCode,
                Type = type,
                Messages = new Dictionary<string, string>() { { key, message } }
            };

            return dto;
        }

        public static void ThrowValidatorException(string key, string errorMessage)
        {
            var errorDto = GetErrorResponse(400, "ValidatorException", errorMessage, key);
            string message = JsonConvert.SerializeObject(errorDto);

            throw new AppValidationException(message);
        }

        public static void Throw404Exception(string key, string errorMessage)
        {
            var errorDto = GetErrorResponse(404, "NotFoundException", errorMessage, key);
            string message = JsonConvert.SerializeObject(errorDto);

            throw new AppEntityNotFoundException(message);
        }

    }

    public class AppValidationException : Exception
    {
        public AppValidationException(string message) : base(message) { }
    }

    public class AppEntityNotFoundException : Exception
    {
        public AppEntityNotFoundException(string message) : base(message) { }
    }
}
