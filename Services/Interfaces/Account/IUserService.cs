using Data.Entity.Account;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Account
{
    public interface IUserService
    {
        Task<Users> Login(LoginDto dto);
        Task<LoginResultDto> LoginResultToken(Users user);

    }
}
