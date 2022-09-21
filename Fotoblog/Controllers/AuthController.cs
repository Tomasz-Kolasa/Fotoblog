using Fotoblog.BLL.Services.AuthService;
using Fotoblog.Utils.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Fotoblog.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVm loginUserVm)
        {
            var result = await _authService.Login(loginUserVm);
            return FromResult(result);
        }
    }
}
