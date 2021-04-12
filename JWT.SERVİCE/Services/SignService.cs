using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.SERVİCE.Services
{
 public static class SignService
    {
        public static SecurityKey SimetrikAnahtar(string SecurityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
        }
    }
}
