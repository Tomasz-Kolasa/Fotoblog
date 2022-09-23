﻿using AutoMapper;
using DevOne.Security.Cryptography.BCrypt;
using Fotoblog.BLL.Services.ServiceResultNS;
using Fotoblog.DAL;
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
        public AuthService(ApplicationDbContext dbContext, IMapper mapper, IConfiguration config) : base(dbContext, mapper)
        {
            _config = config;
        }

        /// <summary>
        /// Login the admin (The admin is the only user who logins the fotoblog)
        /// </summary>
        /// <param name="loginUserVm"></param>
        /// <returns>Service Result with either JWT on successful login or error code if login fails</returns>
        public async Task<ServiceResult<string>> Login(LoginUserVm loginUserVm)
        {
            var user = await _dbContext.UserEntities.FirstOrDefaultAsync(
                x=>x.UserName.ToLower() == loginUserVm.UserName.ToLower());

            if (user == null || !BCryptHelper.CheckPassword(loginUserVm.Password, user.Hash))
                return ServiceResult<string>.Fail(ErrorCodes.BadCredentials);

            return ServiceResult<string>.Ok(GenerateToken(user.Id, user.UserName, "Admin"));
        }

        public string GenerateToken(int userId, string username, string role)
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
