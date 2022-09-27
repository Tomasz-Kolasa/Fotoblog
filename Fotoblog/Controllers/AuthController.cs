﻿using Fotoblog.BLL.Services.AuthService;
using Fotoblog.Utils.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "TempAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminVm registerAdminVm)
        {
            var result = await _authService.RegisterAdmin(registerAdminVm);
            return FromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVm loginUserVm)
        {
            var result = await _authService.Login(loginUserVm);
            return FromResult(result);
        }
    }
}
