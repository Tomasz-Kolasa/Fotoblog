using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.Utils.ViewModels.Settings.AccountSettings;

namespace Fotoblog.BLL.Services.SettingsService
{
    public interface ISettingsService
    {
        Task<ServiceResult> ChangePassword(ChangePwdVm changePwdVm);
    }
}