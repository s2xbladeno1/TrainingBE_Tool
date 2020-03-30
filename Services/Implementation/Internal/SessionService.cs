using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Services.Implementation.Internal
{
   public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User;
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var accountIdClaim = Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(accountIdClaim?.Value))
                {
                    throw new Exception("Bạn chưa đăng nhập");
                }

                int userId;
                if (!int.TryParse(accountIdClaim.Value, out userId))
                {
                    throw new Exception("Bạn chưa đăng nhập");
                }

                return userId;
            }
        }
    }
}
