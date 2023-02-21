using Microsoft.AspNetCore.Mvc;
using otp_verify_without_database.DTOBuilders.Auth;
using otp_verify_without_database.DTOs;
using otp_verify_without_database.RequestPayloads;
using otp_verify_without_database.Utils;

namespace otp_verify_without_database.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        public AuthController() { }

        #region End Points  
        [HttpPost, Route("send-otp")]
        [ProducesResponseType(typeof(OTPDTO), 200)]
        [PayloadValidator]
        public IActionResult SendOTP([FromBody] OTPPayload payload)
        {
            var dto = AuthDTOBuilder.SendOTP(payload);
            return Ok(dto);
        }

        [HttpPost, Route("login")]
        [ProducesResponseType(typeof(AuthDTO), 200)]
        [PayloadValidator]
        public IActionResult Login([FromBody] LoginPayload payload)
        {
            var dto = AuthDTOBuilder.Login(payload);
            return Ok(dto);
        }
        #endregion
    }
}