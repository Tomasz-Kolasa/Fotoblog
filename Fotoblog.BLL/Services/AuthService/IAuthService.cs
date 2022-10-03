﻿using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.Utils.ViewModels.Auth;

namespace Fotoblog.BLL.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResult> ResendEmail(string email);
        Task<ServiceResult> ConfirmEmail(ConfirmEmailVm confirmEmailVm);
        Task<ServiceResult<string>> Login(LoginUserVm loginUserVm);
        Task<ServiceResult> RegisterAdmin(RegisterAdminVm registerAdminVm);
    }
}