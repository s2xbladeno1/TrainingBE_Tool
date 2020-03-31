using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity.Account;
using Microsoft.AspNetCore.Mvc;
using Services.Dto;
using Services.Interfaces.Account;
using Services.Interfaces.Internal;

namespace WebAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Api login user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/user/login")]
        public async Task<IActionResult> Login([FromBody]LoginDto dto)
        {
            var user = await _userService.Login(dto);
            if(user == null)
            {
                return BadRequest("Sai tài khoản hoặc mật khẩu");
            }
            else
            {
                var rtToken = await _userService.LoginResultToken(user);
                var userSession = new UserSessionDto();
                userSession.ID = user.ID;
                userSession.UserName = user.UserName;
                userSession.Password = user.Password;
                userSession.RoleID = rtToken.RoleID;
                userSession.Token = rtToken.Token;
                return Ok(rtToken); 
            }
        }
    }
}