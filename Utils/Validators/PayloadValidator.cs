using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace otp_verify_without_database.Utils
{
    public class PayloadValidator : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                   .Where(x => x.Value!.Errors.Count > 0)
                   .Select(x => new { x.Key, x.Value!.Errors[0].ErrorMessage })
                   .ToList();

                var messages = new Dictionary<string, string>();
                foreach (var error in errors)
                {
                    string key = string.IsNullOrEmpty(error.Key) ? "Generic" : error.Key;
                    if (key.Contains("$."))
                        key = key.Replace("$.", "");

                    messages.Add(key, error.ErrorMessage);
                }

                var errorDto = ErrorHelper.GetErrorResponse(400, "ValidatorException", messages);
                string message = JsonConvert.SerializeObject(errorDto);

                throw new AppValidationException(message);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
