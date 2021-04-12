using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.CORE.Dtos
{
   public class ClientTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
    }
}
