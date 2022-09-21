using AutoMapper;
using DevOne.Security.Cryptography.BCrypt;
using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.DAL;
using Fotoblog.Utils.ViewModels.Auth;
using Microsoft.EntityFrameworkCore;

namespace Fotoblog.BLL.Services.AuthService
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public async Task<ServiceResult> Login(LoginUserVm loginUserVm)
        {
            var user = await _dbContext.UserEntities.FirstOrDefaultAsync(x=>x.UserName == loginUserVm.UserName);

            if (user == null || !BCryptHelper.CheckPassword(loginUserVm.Password, user.Hash))
                return ServiceResult.Fail(ErrorCodes.BadCredentials);

            return ServiceResult.Ok();
        }
    }
}
