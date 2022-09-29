using AutoMapper;
using DevOne.Security.Cryptography.BCrypt;
using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.DAL;
using Fotoblog.Utils.ViewModels.Settings.AccountSettings;
using Microsoft.EntityFrameworkCore;

namespace Fotoblog.BLL.Services.SettingsService
{
    public class SettingsService : BaseService, ISettingsService
    {
        public SettingsService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public async Task<ServiceResult> ChangePassword(ChangePwdVm changePwdVm)
        {
            var user = await _dbContext.UserEntities.FirstOrDefaultAsync(u => u.Id == changePwdVm.UserId);

            if (user == null) return ServiceResult.Fail(ErrorCodes.UserNotExists);

            if( !BCryptHelper.CheckPassword(changePwdVm.OldPwd, user.Hash) )
                return ServiceResult.Fail(ErrorCodes.WrongPassword);

            user.Hash = BCryptHelper.HashPassword(changePwdVm.NewPwd, BCryptHelper.GenerateSalt());
            await _dbContext.SaveChangesAsync();

            return ServiceResult.Ok();
        }
    }
}
