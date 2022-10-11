using AutoMapper;
using DevOne.Security.Cryptography.BCrypt;
using Fotoblog.BLL.Services.EmailService;
using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.DAL;
using Fotoblog.DAL.Entities;
using Fotoblog.Utils.ViewModels.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fotoblog.BLL.Services.AuthService
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        public AuthService(
            ApplicationDbContext dbContext, IMapper mapper, IConfiguration config, IEmailService emailService
            ) : base(dbContext, mapper)
        {
            _config = config;
            _emailService = emailService;
        }

        /// <summary>
        /// Checkc if eamil & token exists. If so, sets new password.
        /// </summary>
        /// <param name="resetPasswordVm">Email&token, old password and new password to be set</param>
        /// <returns>ServiceResult.Ok() if password successfuly changed
        /// ServiceResult.Fail() with relevant error code on fail
        /// </returns>
        public async Task<ServiceResult> ResetPassword(ResetPasswordVm resetPasswordVm)
        {
            var user = await _dbContext.UserEntities.FirstOrDefaultAsync(
                u => u.Email==resetPasswordVm.Email && u.EmailVerificationCode==resetPasswordVm.Token);

            if (user == null)
            {
                return ServiceResult.Fail(ErrorCodes.InvalidResetPwdLink);
            }

            user.Hash = BCryptHelper.HashPassword(resetPasswordVm.NewPwd, BCryptHelper.GenerateSalt());
            user.EmailVerificationCode = Guid.NewGuid().ToString();
            await _dbContext.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        /// <summary>
        /// Send the adamin an email with a link so that they can reset the password
        /// </summary>
        /// <param name="email">admin's email to resend the link to</param>
        /// <returns>ServiceResult.Ok() if the email is send successfuly OR given email is not found.
        /// ServiceResult.Fail() if given email was found, but the try of sending email fails.
        public async Task<ServiceResult> SendResetPassword(string email)
        {
            var user = await _dbContext.UserEntities.FirstOrDefaultAsync(
                u => u.Email == email.ToLower()
                && u.IsEmailConfirmed);

            if (user != null)
            {
                user.EmailVerificationCode = Guid.NewGuid().ToString();
                await _dbContext.SaveChangesAsync();

                try
                {
                    await _emailService.SendUserResetPasswordLink(
                        user.UserName, user.EmailVerificationCode, user.Email);
                }
                catch (Exception)
                {
                    return ServiceResult.Fail(ErrorCodes.ResetPasswordLinkNotSent);
                }
            }

            return ServiceResult.Ok();
        }

        /// <summary>
        /// Resend the adamin an email with a link so that they can confirm that they are the owner of the mailbox
        /// Only send an email if given email exists
        /// </summary>
        /// <param name="email">email to resend the link to</param>
        /// <returns>ServiceResult.Ok() if the email is send successfuly OR given email is not found.
        /// ServiceResult.Fail() if given email was found, but the try of sending email fails.
        /// </returns>
        public async Task<ServiceResult<string>> ResendEmail(string email)
        {
            var user = await _dbContext.UserEntities.FirstOrDefaultAsync(
                u => u.Email == email.ToLower()
                && !u.IsEmailConfirmed);

            if (user != null)
            {
                try
                {
                    await _emailService.SendUserActivationLink(
                        user.UserName, user.EmailVerificationCode, user.Email);
                }
                catch (Exception ex)
                {
                    return ServiceResult<string>.Fail(ex.Message, ErrorCodes.EmailConfirmationLinkNotSent);
                }
            }

            return ServiceResult<string>.Ok("ok");
        }

        public async Task<ServiceResult> ConfirmEmail(ConfirmEmailVm confirmEmailVm)
        {
            var user = await _dbContext.UserEntities.FirstOrDefaultAsync(
                u => u.Email==confirmEmailVm.Email.ToLower()
                && u.EmailVerificationCode==confirmEmailVm.Token
                && u.IsEmailConfirmed==false);
            
            if (user==null)
            {
                return ServiceResult.Fail(ErrorCodes.EmailTokenNotFound);
            }

            user.IsEmailConfirmed = true;
            await _dbContext.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> RegisterAdmin(RegisterAdminVm registerAdminVm)
        {
            var usersQty = _dbContext.UserEntities.Count();

            if(usersQty > 0)
            {
                return ServiceResult.Fail(ErrorCodes.AdminAlreadyRegistered);
            }

            var userEntity = new UserEntity {
                UserName = registerAdminVm.UserName,
                Hash = BCryptHelper.HashPassword(registerAdminVm.Password, BCryptHelper.GenerateSalt()),
                Email = registerAdminVm.Email.ToLower(),
                IsEmailConfirmed = false,
                EmailVerificationCode = Guid.NewGuid().ToString()
            };

            await _dbContext.UserEntities.AddAsync(userEntity);
            _dbContext.SaveChanges();

            try
            {
                await _emailService.SendUserActivationLink(
                    userEntity.UserName, userEntity.EmailVerificationCode, userEntity.Email);
            }
            catch(Exception)
            {
                return ServiceResult.Fail(ErrorCodes.EmailConfirmationLinkNotSent);
            }

            return ServiceResult.Ok();
        }

        /// <summary>
        /// Login the admin (The admin is the only user who logins the fotoblog)
        /// </summary>
        /// <param name="loginUserVm"></param>
        /// <returns>Service Result with either JWT on successful login or error code if login fails</returns>
        public async Task<ServiceResult<string>> Login(LoginUserVm loginUserVm)
        {
            var usersQty = _dbContext.UserEntities.Count();

            if (usersQty == 0)
            {
                if (LoginWithFirstTimeLoginCreds(loginUserVm))
                {
                    return ServiceResult<string>.Ok(
                        GenerateToken(0, _config.GetSection("Auth:FirstTimeLoginCreds:Login").Value, "TempAdmin"));
                }

                return ServiceResult<string>.Fail(ErrorCodes.BadCredentials);
            }

            var user = await _dbContext.UserEntities.FirstOrDefaultAsync(
                x=>x.UserName.ToLower() == loginUserVm.UserName.Trim().ToLower());

            if (user == null ||!BCryptHelper.CheckPassword(loginUserVm.Password, user.Hash))
            {
                return ServiceResult<string>.Fail(ErrorCodes.BadCredentials);
            }
            else if(!user.IsEmailConfirmed)
            {                    
                return ServiceResult<string>.Fail(ErrorCodes.EmailNotConfirmed);
            }
            else
            {
                return ServiceResult<string>.Ok(GenerateToken(user.Id, user.UserName, "Admin"));
            }
        }

        private bool LoginWithFirstTimeLoginCreds(LoginUserVm loginUserVm)
        {
            var firstTimeLogin = _config.GetSection("Auth:FirstTimeLoginCreds:Login").Value;
            var firstTimePwd = _config.GetSection("Auth:FirstTimeLoginCreds:Password").Value;

            if (loginUserVm.UserName.Trim().ToLower() == firstTimeLogin
                && loginUserVm.Password.Trim() == firstTimePwd)
                return true;

            return false;
        }

        private string GenerateToken(int userId, string username, string role)
        {
            List<Claim> claims = new List<Claim>
                {
                     new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                     new Claim(ClaimTypes.Name, username),
                     new Claim(ClaimTypes.Role, role)
                 };

            var tokenSecretKey = _config.GetSection("Auth").GetSection("TokenSecretKey").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecretKey));

            var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCreds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
