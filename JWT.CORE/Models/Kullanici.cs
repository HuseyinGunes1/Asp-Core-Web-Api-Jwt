using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.CORE.Models
{
   public class Kullanici:IdentityUser
    {
        public string City { get; set; }
    }
}
