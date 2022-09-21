using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.Utils.ViewModels.Auth;

namespace Fotoblog.BLL.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResult> Login(LoginUserVm loginUserVm);
    }
}