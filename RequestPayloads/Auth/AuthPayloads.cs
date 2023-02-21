using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace otp_verify_without_database.RequestPayloads
{
    public class OTPPayload
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [MinLength(13, ErrorMessage = "Please enter a valid phone number.")]
        [MaxLength(13, ErrorMessage = "Please enter a valid phone number.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }
    }

    public class LoginPayload
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [MinLength(13, ErrorMessage = "Please enter a valid phone number.")]
        [MaxLength(13, ErrorMessage = "Please enter a valid phone number.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "OTP is required.")]
        [RegularExpression("^[0-9]{6}$")]
        public int OTP { get; set; }

        [Required(ErrorMessage = "OTP token is required.")]
        [RegularExpression(@".*\S.*", ErrorMessage = "Please provide valid OTP token.")]
        public string OTPToken { get; set; }
    }
}
