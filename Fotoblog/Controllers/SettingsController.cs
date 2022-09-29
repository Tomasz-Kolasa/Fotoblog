using Fotoblog.BLL.Services.SettingsService;
using Fotoblog.Utils.ViewModels.Settings.AccountSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fotoblog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : BaseController
    {
        private readonly ISettingsService _settingsService;
        
        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePwdVm changePwdVm)
        {
            var result = await _settingsService.ChangePassword(changePwdVm);
            return FromResult(result);
        }
    }
}