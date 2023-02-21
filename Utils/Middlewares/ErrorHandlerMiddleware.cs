using Newtonsoft.Json;
using System.Net;

namespace otp_verify_without_database.Utils
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                string errorInfo = ex?.Message;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case AppValidationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case AppEntityNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case BadHttpRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case UnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorInfo = "Something went wrong. Internal server error.";
                        break;
                }

                if (!(ex is AppValidationException) && !(ex is AppEntityNotFoundException))
                {
                    var errorDto = ErrorHelper.GetErrorResponse(response.StatusCode, ex.GetType().ToString(), ex?.Message);
                    errorInfo = JsonConvert.SerializeObject(errorDto);
                }
                await response.WriteAsync(errorInfo);
            }
        }
    }
}
