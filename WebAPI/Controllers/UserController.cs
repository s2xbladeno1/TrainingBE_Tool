using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity.Account;
using Microsoft.AspNetCore.Mvc;
using Services.Dto;
using Services.Interfaces.Account;

namespace WebAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
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
                return Ok(rtToken); 
            }
        }
    }
}