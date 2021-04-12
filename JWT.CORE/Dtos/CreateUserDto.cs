using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.CORE.Dtos
{
   public class CreateUserDto
    {
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }

    }
}
