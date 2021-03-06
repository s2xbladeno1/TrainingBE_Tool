﻿using Dapper.FastCrud;
using Data;
using Data.Entity.Account;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Dto;
using Services.Interfaces.Account;
using Services.Interfaces.Internal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation.Account
{
    public class UserService : BaseService, IUserService
    {
        private readonly ILogger<UserService> _logger;
        public UserService(DatabaseConnectService databaseConnectService, ILogger<UserService> logger) : base(databaseConnectService)
        {
            _databaseConnectService = databaseConnectService;
            _logger = logger;
        }

        public async Task<Users> Login(LoginDto dto)
        {
            var result = await _databaseConnectService.Connection.FindAsync<Users>(x => x
                                                           .Where($"{nameof(Users.UserName):C}=@username and {nameof(Users.Password):C}=@password")
                                                           .WithParameters(new { username = dto.UserName, password = dto.Password }));
            return result.FirstOrDefault();
        }
        public async Task<LoginResultDto> LoginResultToken(Users user)
        {
            try
            {
                _logger.LogInformation(user.FullName + " - Đăng nhập thành công");
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey010203"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    claims: new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.RoleID.ToString())
                    },
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return new LoginResultDto
                {
                    ID = user.ID,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    RoleID = user.RoleID,
                    Token = tokenString
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
