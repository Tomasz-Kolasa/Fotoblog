using Fotoblog.BLL.Services.ServiceResultNS;
using Microsoft.AspNetCore.Mvc;

namespace Fotoblog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
        protected IActionResult FromResult(ServiceResult result)
        {
            int code = GetHttpReturnCode(result.ErrorCode, result.Status);
            return StatusCode(code, result);
        }

        protected IActionResult FromResult<T>(ServiceResult<T> result)
        {
            int code = GetHttpReturnCode(result.ErrorCode, result.Status);
            return StatusCode(code, result);
        }

        private int GetHttpReturnCode(ErrorCodes? errorCode, bool result)
        {
            return (result) ? 200 : 400;
        }
    }
}
