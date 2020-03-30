using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dto
{
    public class LoginResultDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }

    }
}
