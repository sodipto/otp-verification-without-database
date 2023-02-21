using Microsoft.AspNetCore.Mvc;

namespace otp_verify_without_database.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected BaseController() { }
    }
}
